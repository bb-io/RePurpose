using Apps.RePurpose.Constants;
using Apps.RePurpose.Dtos.OpenAI;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Net;
using System.Reflection;

namespace Apps.RePurpose.Api;

public class OpenAIClient : BlackBirdRestClient, ICompletionClient
{

    protected override JsonSerializerSettings JsonSettings =>
        new() { MissingMemberHandling = MissingMemberHandling.Ignore };

    public OpenAIClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(new RestClientOptions
    { ThrowOnAnyError = false, BaseUrl = new Uri("https://api.openai.com/v1"), MaxTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds })
    {
        var key = authenticationCredentialsProviders.Get(CredsNames.ApiKey).Value;
        this.AddDefaultHeader("Authorization", $"Bearer {key}");
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (response.Content == null)
            throw new Exception(response.ErrorMessage);

        var error = JsonConvert.DeserializeObject<ErrorDtoWrapper>(response.Content, JsonSettings);

        if (response.StatusCode == HttpStatusCode.NotFound && error.Error.Type == "invalid_request_error")
            throw new PluginMisconfigurationException("Model chosen is not suitable for this task. Please choose a compatible model.");

        return new(error?.Error?.Message ?? response.ErrorException.Message);
    }

    public async Task<string> ExecuteCompletion(string model, string systemPrompt, string userPrompt, int? maxTokens = null)
    {
        var jsonBody = new
        {
            model,
            Messages = new List<ChatMessageDto> { new("system", systemPrompt), new("user", userPrompt) },
            top_p = 1,
            presence_penalty = 0,
            frequency_penalty = 0,
            temperature = 1,
        };

        var jsonBodySerialized = JsonConvert.SerializeObject(jsonBody, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        });

        var request = new RestRequest("/chat/completions", Method.Post);
        request.AddJsonBody(jsonBodySerialized);

        var response = await ExecuteWithErrorHandling<ChatCompletionDto>(request);
        var completion = response.Choices.FirstOrDefault()?.Message?.Content;

        if (completion == null) throw new PluginApplicationException("OpenAI did not return any completion");

        return completion;
    }

    public async Task<IEnumerable<DataSourceItem>> SearchModels(string? searchString)
    {
        var request = new RestRequest("/models", Method.Get);
        var response = await ExecuteWithErrorHandling<ModelsList>(request);
        return response.Data
            .Where(x => (x.Id.Contains("gpt") || x.Id.Contains("o1")) && !x.Id.Contains("vision") && !x.Id.Contains("instruct"))
            .Where(model => searchString == null || model.Id.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Id, x.Id));
    }
}