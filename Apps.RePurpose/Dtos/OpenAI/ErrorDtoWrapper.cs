namespace Apps.RePurpose.Dtos.OpenAI;

public record ErrorDto(string Message, string Type, string? Code);

public record ErrorDtoWrapper(ErrorDto Error);
