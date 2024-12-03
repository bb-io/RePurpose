using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Api;

public interface ICompletionClient
{
    public Task<string> ExecuteCompletion(string model, string systemPrompt, string userPrompt, int? maxTokens = null);

    public Task<IEnumerable<DataSourceItem>> SearchModels(string? searchString);
}
