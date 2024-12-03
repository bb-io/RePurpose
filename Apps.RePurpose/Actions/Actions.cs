using Apps.RePurpose.Constants;
using Apps.RePurpose.DataSourceHandlers;
using Apps.RePurpose.Invocables;
using Apps.RePurpose.Models;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using System.Text.RegularExpressions;
using System.Text;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using System.Net.Mime;

namespace Apps.RePurpose.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Repurpose content", Description = "Repurpose content for different target audiences, languages, tone of voices and platforms")]
    public async Task<RepurposeResponse> RepurposeContent(        
        [ActionParameter][Display("Original content")] string content,        
        [ActionParameter][Display("Style guide")] string styleGuide,
        [ActionParameter][Display("Model")][DataSource(typeof(ModelHandler))] string? model,
        [ActionParameter][Display("Language")][StaticDataSource(typeof(LanguageHandler))] string? language,
        [ActionParameter] GlossaryRequest glossary)
    {
        if (model == null)
        {
            model = App == CredsNames.Anthropic ? "claude-3-5-sonnet-20241022" : "gpt-4o";
        }

        var prompt = 
@$"Repurpose the content of the message of the user. If the content contains any signs of file formatting, the file format and tags should be preserved. You also need to consider the following style elements and guides: 
{styleGuide}
{(language != null ? $" The response should be in {language}." : string.Empty)}
";

        if (glossary.Glossary != null)
        {
            var glossaryAddition =
                " Enhance the target text by incorporating relevant terms from our glossary where applicable. " +
                "Ensure that the translation aligns with the glossary entries for the respective languages. " +
                "If a term has variations or synonyms, consider them and choose the most appropriate " +
                "translation to maintain consistency and precision. ";

            var glossaryPromptPart = await GetGlossaryPromptPart(glossary.Glossary, content, true);
            if (glossaryPromptPart != null) prompt += (glossaryAddition + glossaryPromptPart);
        }

        var completion = await Client.ExecuteCompletion(model, prompt, content);

        return new()
        {
            SystemPrompt = prompt,
            RepurposedText = completion,
        };
    }

    [Action("Repurpose file", Description = "Repurpose content of a file for different target audiences, languages, tone of voices and platforms")]
    public async Task<RepurposeFileResponse> RepurposeFile(
    [ActionParameter][Display("Original file")] FileRequest file,
    [ActionParameter][Display("Style guide")] string styleGuide,
    [ActionParameter][Display("Model")][DataSource(typeof(ModelHandler))] string? model,
    [ActionParameter][Display("Language")][StaticDataSource(typeof(LanguageHandler))] string? language,
    [ActionParameter] GlossaryRequest glossary)
    {
        var fileStream = await fileManagementClient.DownloadAsync(file.File);
        byte[] bytes = await fileStream.GetByteData();
        string content = Encoding.UTF8.GetString(bytes);

        var response = await RepurposeContent(content, styleGuide, model, language, glossary);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(response.RepurposedText));
        var repurposedFile = await fileManagementClient.UploadAsync(stream, MimeTypes.GetMimeType(file.File.Name), file.File.Name);

        return new RepurposeFileResponse()
        {
            RepurposedFile = repurposedFile,
            SystemPrompt = response.SystemPrompt,
        };
    }

    private async Task<string?> GetGlossaryPromptPart(FileReference glossary, string sourceContent, bool filter)
    {
        var glossaryStream = await fileManagementClient.DownloadAsync(glossary);
        var blackbirdGlossary = await glossaryStream.ConvertFromTbx();

        var glossaryPromptPart = new StringBuilder();
        glossaryPromptPart.AppendLine();
        glossaryPromptPart.AppendLine();
        glossaryPromptPart.AppendLine("Glossary entries (each entry includes terms in different language. Each " +
                                      "language may have a few synonymous variations which are separated by ;;):");

        var entriesIncluded = false;
        foreach (var entry in blackbirdGlossary.ConceptEntries)
        {
            var allTerms = entry.LanguageSections.SelectMany(x => x.Terms.Select(y => y.Term));
            if (filter && !allTerms.Any(x => Regex.IsMatch(sourceContent, $@"\b{x}\b", RegexOptions.IgnoreCase))) continue;
            entriesIncluded = true;

            glossaryPromptPart.AppendLine();
            glossaryPromptPart.AppendLine("\tEntry:");

            foreach (var section in entry.LanguageSections)
            {
                glossaryPromptPart.AppendLine(
                    $"\t\t{section.LanguageCode}: {string.Join(";; ", section.Terms.Select(term => term.Term))}");
            }
        }

        return entriesIncluded ? glossaryPromptPart.ToString() : null;
    }

}