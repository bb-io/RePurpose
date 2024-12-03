using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.DataSourceHandlers
{
    public class LanguageHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(c => new DataSourceItem(c.DisplayName, c.DisplayName));
        }
    }
}
