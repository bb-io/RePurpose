using Apps.RePurpose.Constants;
using Apps.RePurpose.Dtos.Anthropic;
using Apps.RePurpose.Dtos.OpenAI;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Api
{
    public class AnthropicClient : BlackBirdRestClient, ICompletionClient
    {
        public AnthropicClient(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) :
            base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = new Uri("https://api.anthropic.com/v1") })
        {
            var key = authenticationCredentialsProviders.Get(CredsNames.ApiKey).Value;
            this.AddDefaultHeader("x-api-key", key);
            this.AddDefaultHeader("anthropic-version", "2023-06-01");
        }

        protected override Exception ConfigureErrorException(RestResponse response)
        {
            try
            {
                var json = response.Content!;
                var error = JsonConvert.DeserializeObject<ErrorResponse>(json)!;
                return new PluginApplicationException(error.ToString());
            }
            catch (Exception)
            {
                return new($"Failed to parse error response. Content: {response.Content}");
            }
        }

        public async Task<string> ExecuteCompletion(string model, string systemPrompt, string userPrompt, int? maxTokens = null)
        {
            var request = new RestRequest("/messages", Method.Post);

            request.AddJsonBody(new
            {
                system = systemPrompt,
                model = model,
                messages = new List<Message> { new Message { Role = "user", Content = userPrompt } },
                max_tokens = maxTokens ?? 4096,
                stop_sequences = new List<string>(),
                temperature = 1.0f,
                top_p = 1.0f,
                top_k = 1,
            });

            var response = await ExecuteWithErrorHandling<CompletionResponse>(request);

            var completion = response.Content.FirstOrDefault()?.Text;

            if (completion == null) throw new PluginApplicationException("OpenAI did not return any completion");

            return completion;
        }

        public async Task<IEnumerable<DataSourceItem>> SearchModels(string? searchString)
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem("claude-3-5-haiku-20241022", "Claude 3.5 Haiku"),
                new DataSourceItem("claude-3-5-sonnet-20241022", "Claude 3.5 Sonnet"),
                new DataSourceItem("claude-3-opus-20240229", "Claude 3 Opus"),
                new DataSourceItem("claude-3-sonnet-20240229", "Claude 3 Sonnet"),
                new DataSourceItem("claude-3-haiku-20240307", "Claude 3 Haiku"),
                new DataSourceItem("claude-2.1", "Claude 2.1"),
                new DataSourceItem("claude-2", "Claude 2"),
                new DataSourceItem("claude-instant-1", "Claude Instant"),
            };
        }
    }
}
