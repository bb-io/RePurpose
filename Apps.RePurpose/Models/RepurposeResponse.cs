using Apps.RePurpose.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;
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

        [Display("Language")]
        public string? Language { get; set; }

        [Display("Tone")]
        public string? Tone { get; set; }

        [Display("Touchpoint")]
        public string? Touchpoint { get; set; }

        [Display("Purpose")]
        public string? Purpose { get; set; }

        [Display("Audience")]
        public string? Audience { get; set; }
    }
}
