using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.RePurpose.Dtos.OpenAI;

public record ChatCompletionDto(IEnumerable<ChatCompletionChoiceDto> Choices, UsageDto Usage);
public record BaseChatMessageDto(string Role);
public record ChatMessageDto(string Role, string Content) : BaseChatMessageDto(Role);

public record ChatCompletionChoiceDto
{
    public ChatMessageDto Message { get; init; }

    [JsonProperty("finish_reason")]
    public string FinishReason { get; set; }
}
