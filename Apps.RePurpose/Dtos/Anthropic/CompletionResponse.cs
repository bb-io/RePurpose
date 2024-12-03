using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Dtos.Anthropic
{
    public class Content
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }

    public class CompletionResponse
    {
        [JsonProperty("content")]
        public List<Content> Content { get; set; }

        [JsonProperty("stop_reason")]
        public string StopReason { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
    }
}
