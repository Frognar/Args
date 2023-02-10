using Frognar.CliArgs.Enums;

namespace Frognar.CliArgs.Exceptions;

public class ArgsException : Exception
{
    public ErrorCode ErrorCode { get; }
    public char ErrorArgumentId { get; private set; }
    
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
        _ => ""
    };
}