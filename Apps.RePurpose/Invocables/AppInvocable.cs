using Apps.RePurpose.Api;
using Apps.RePurpose.Constants;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.RePurpose.Invocables;

public class AppInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected ICompletionClient Client { get; }

    public string App => Creds.Get(CredsNames.App).Value;

    public AppInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = App == CredsNames.Anthropic ? new AnthropicClient(Creds) : new OpenAIClient(Creds);  
    }
}