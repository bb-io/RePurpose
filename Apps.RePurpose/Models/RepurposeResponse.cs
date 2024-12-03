using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Models
{
    public class RepurposeResponse
    {
        [Display("Repurposed text")]
        public string RepurposedText { get; set; }

        [Display("System prompt")]
        public string SystemPrompt { get; set; }
    }
}
