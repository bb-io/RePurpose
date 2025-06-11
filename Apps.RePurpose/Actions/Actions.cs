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
using Blackbird.Xliff.Utils.Models.Content;
using Blackbird.Xliff.Utils.Constants;
using Apps.RePurpose.Utils;

namespace Apps.RePurpose.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Repurpose text", Description = "Repurpose content for different target audiences, languages, tone of voices and platforms")]
    public async Task<RepurposeResponse> RepurposeContent(        
        [ActionParameter][Display("Original content")] string content,        
        [ActionParameter] RepurposeRequest request)
    {
        if (request.Model == null)
        {
            request.Model = App == CredsNames.Anthropic ? "claude-3-5-sonnet-20241022" : "gpt-4.1";
        }

        var prompt = @$"Repurpose the content of the message of the user. Do not add any tags, html markdown or otherwise. You also need to consider the following instructions, style elements and guide: {request.StyleGuide}.";

        if (request.Tone != null)
        {
            prompt += request.Tone;
        }

        if (request.Touchpoint != null)
        {
            prompt += GetTouchpointPromptPart(request.Touchpoint);
        }

        if (request.Audience != null)
        {
            prompt += $" The audience of the content will be {request.Audience}.";
        }

        if (request.Purpose != null)
        {
            prompt += $" The new purpose of the content will be {request.Purpose}.";
        }

        if (request.Language != null)
        {
            prompt += $" The repurposed content (response) should be in {request.Language}. This is very important.";
        }

        if (request.Glossary != null)
        {
            var glossaryAddition =
                " Enhance the target text by incorporating relevant terms from our glossary where applicable. " +
                "Ensure that the translation aligns with the glossary entries for the respective languages. " +
                "If a term has variations or synonyms, consider them and choose the most appropriate " +
                "translation to maintain consistency and precision. ";

            var glossaryPromptPart = await GetGlossaryPromptPart(request.Glossary, content, true);
            if (glossaryPromptPart != null) prompt += (glossaryAddition + glossaryPromptPart);
        }

        var completion = await ErrorHandler.ExecuteWithErrorHandlingAsync(() => Client.ExecuteCompletion(request.Model, prompt, content));

        return new()
        {
            SystemPrompt = prompt,
            RepurposedText = completion,
            Language = request.Language,
            Tone = request.Tone,
            Touchpoint = request.Touchpoint,
            Audience = request.Audience,
            Purpose = request.Purpose,
        };
    }

    [Action("Repurpose", Description = "Repurpose content of a file for different target audiences, languages, tone of voices and platforms")]
    public async Task<RepurposeResponse> RepurposeFile(
    [ActionParameter] FileRequest file,
    [ActionParameter] RepurposeRequest request)
    {
        var fileStream = await ErrorHandler.ExecuteWithErrorHandlingAsync(() => fileManagementClient.DownloadAsync(file.File));

        var complexContent = await FileGroup.TryParse(fileStream);
        string? content;
        if (complexContent != null)
        {
            request.Language ??= complexContent.TargetLanguage;
            content = string.Join("\n",complexContent.IterateSegments().Select(x => x.Target == null ? x.GetSource(TagInclusion.Original) : x.GetTarget(TagInclusion.Ignore)));
        } else
        {
            fileStream.Position = 0;
            byte[] bytes = await fileStream.GetByteData();
            content = Encoding.UTF8.GetString(bytes);
        }

        return await RepurposeContent(content, request);
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

    private string GetTouchpointPromptPart(string touchpoint)
    {
        if (touchpoint == "Blog post") return Prompts.BlogPostPrompt;
        if (touchpoint == "LinkedIn post") return Prompts.LinkedInPrompt;
        if (touchpoint == "Tweet") return Prompts.TweetPrompt;
        return touchpoint;
    }

}