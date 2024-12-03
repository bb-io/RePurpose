using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Models
{
    public class RepurposeFileResponse
    {
        [Display("Repurposed file")]
        public FileReference RepurposedFile { get; set; }

        [Display("System prompt")]
        public string SystemPrompt { get; set; }
    }
}
