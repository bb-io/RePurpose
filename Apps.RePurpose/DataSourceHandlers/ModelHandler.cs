using Apps.RePurpose.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.DataSourceHandlers
{
    public class ModelHandler(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        public Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            return Client.SearchModels(context.SearchString);
        }
    }
}
