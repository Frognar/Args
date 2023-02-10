using Frognar.CliArgs.Enums;

namespace Frognar.CliArgs.Exceptions;

public class ArgsException : Exception
{
    public ErrorCode ErrorCode { get; }
    public char ErrorArgumentId { get; private set; }
    public string? ErrorParameter { get; }
    
    public ArgsException(ErrorCode errorCode, char errorArgumentId, string errorParameter)
    {
        ErrorCode = errorCode;
        ErrorArgumentId = errorArgumentId;
        ErrorParameter = errorParameter;
    }
    
    public ArgsException(ErrorCode errorCode, string errorParameter)
    {
        ErrorCode = errorCode;
        ErrorParameter = errorParameter;
    }
    
    public ArgsException(ErrorCode errorCode, char errorArgumentId)
    {
        ErrorCode = errorCode;
        ErrorArgumentId = errorArgumentId;
    }
    
    public ArgsException(ErrorCode errorCode)
    {
        ErrorCode = errorCode;
    }

    public void SetErrorArgumentId(char elementId)
    {
        ErrorArgumentId = elementId;
    }

    public override string Message => ErrorCode switch
    {
        ErrorCode.UnexpectedArgument =>  $"Argument -{ErrorArgumentId} unexpected.",
        ErrorCode.InvalidArgumentName => $"'{ErrorArgumentId}' is not a valid argument name.",
        ErrorCode.InvalidArgumentFormat => $"'{ErrorParameter}' is not a valid argument format.",
        ErrorCode.MissingString => $"Could not find string parameter for -{ErrorArgumentId}.",
        ErrorCode.InvalidInteger => $"Argument -{ErrorArgumentId} expects an integer but was '{ErrorParameter}'.",
        ErrorCode.MissingInteger => $"Could not find integer parameter for -{ErrorArgumentId}.",
        _ => ""
    };
}