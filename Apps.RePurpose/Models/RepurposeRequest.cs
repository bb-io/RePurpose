using Apps.RePurpose.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.RePurpose.Models;
public class RepurposeRequest
{
    [Display("Instructions")]
    public string StyleGuide { get; set; }

    [Display("Model")]
    [DataSource(typeof(ModelHandler))]
    public string? Model { get; set; }

    [Display("Language")]
    [StaticDataSource(typeof(LanguageHandler))]
    public string? Language { get; set; }

    [Display("Glossary")]
    public FileReference? Glossary { get; set; }

    [Display("Tone")]
    [StaticDataSource(typeof(ToneHandler))]
    public string? Tone { get; set; }

    [Display("Touchpoint")]
    [StaticDataSource(typeof(TouchpointHandler))]
    public string? Touchpoint { get; set; }

    [Display("Purpose")]
    public string? Purpose { get; set; }

    [Display("Audience")]
    public string? Audience { get; set; }
}
