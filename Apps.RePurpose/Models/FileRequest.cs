using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Models
{
    public class FileRequest
    {
        [Display("Plaintext file", Description = "Can be any plaintext file type like txt, html or csv")]
        public FileReference File { get; set; }
    }
}
