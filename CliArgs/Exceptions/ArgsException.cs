using Frognar.CliArgs.Enums;

namespace Frognar.CliArgs.Exceptions;

public class ArgsException : Exception
{
    public ErrorCode ErrorCode { get; }
    public char ErrorArgumentId { get; }
    
    public ArgsException(ErrorCode errorCode, char errorArgumentId)
    {
        ErrorCode = errorCode;
        ErrorArgumentId = errorArgumentId;
    }
}