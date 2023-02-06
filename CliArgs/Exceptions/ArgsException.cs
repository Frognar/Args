using Frognar.CliArgs.Enums;

namespace Frognar.CliArgs.Exceptions;

public class ArgsException : Exception
{
    public ErrorCode ErrorCode { get; }
    
    public ArgsException(ErrorCode errorCode)
    {
        ErrorCode = errorCode;
    }
}