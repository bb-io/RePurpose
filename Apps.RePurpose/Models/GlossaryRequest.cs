using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Models;
public class GlossaryRequest
{
    [Display("Glossary")]
    public FileReference? Glossary { get; set; }
}