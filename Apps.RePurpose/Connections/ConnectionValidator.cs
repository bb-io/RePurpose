using Apps.RePurpose.Api;
using Apps.RePurpose.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using DocumentFormat.OpenXml.Drawing;
using RestSharp;

namespace Apps.RePurpose.Connections;

public class ConnectionValidator: IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        var app = authenticationCredentialsProviders.Get(CredsNames.App).Value;
        var key = authenticationCredentialsProviders.Get(CredsNames.ApiKey).Value;

        if (app == CredsNames.Anthropic)
        {
            var client = new AnthropicClient(authenticationCredentialsProviders);
            var request = new RestRequest("/complete", Method.Post);

            request.AddJsonBody(new
            {
                model = "claude-2",
                prompt = "\n\nHuman: hello \n\nAssistant:",
                max_tokens_to_sample = 20
            });

            try
            {
                await client.ExecuteWithErrorHandling(request);
                return new()
                {
                    IsValid = true
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsValid = false,
                    Message = ex.Message
                };
            }

        } else
        {
            var client = new OpenAIClient(authenticationCredentialsProviders);
            var request = new RestRequest("/models", Method.Get);

            try
            {
                await client.ExecuteWithErrorHandling(request);
                return new()
                {
                    IsValid = true
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsValid = false,
                    Message = ex.Message
                };
            }
        }
    }
}