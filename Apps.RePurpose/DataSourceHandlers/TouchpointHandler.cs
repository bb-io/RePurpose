using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.DataSourceHandlers;
public class TouchpointHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return new List<DataSourceItem>() 
        { 
            new ("Blog post", "Blog post"),
            new ("LinkedIn post", "LinkedIn post"),
            new ("Tweet", "Tweet"),
        };
    }
}
