namespace Done.Domain.Errors;

public sealed class Error(string message, string code)
{
    public string Message { get; } = message;
    public string Code { get; } = code;

    internal static Error None => new Error(string.Empty, string.Empty);

    public static implicit operator string(Error error) => error.Code;
}