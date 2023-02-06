using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => 0;
    
    public Args(string schema, string[] args)
    {
        if (args.Length > 0)
            throw new ArgsException(ErrorCode.UnexpectedArgument, 'x');
    }
}