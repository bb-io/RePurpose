using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Dtos.OpenAI;

public class ModelDto
{
    public string Id { get; set; }
    public string Object { get; set; }
    public int Created { get; set; }

    [JsonProperty("owned_by")]
    public string OwnedBy { get; set; }
}

public record ModelsList(IEnumerable<ModelDto> Data);
