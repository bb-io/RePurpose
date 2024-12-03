using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Dtos.Anthropic;

public class ErrorResponse
{
    [JsonProperty("error")]
    public Error Error { get; set; }

    public override string ToString() => $"{Error.Type}: {Error.Message}";
}

public class Error
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}