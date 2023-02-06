using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => 0;
    
    public Args(string schema, string[] args)
    {
        if (schema.Length > 0)
        {
            char elementId = schema[0];
            if (char.IsLetter(elementId) == false)
                throw new ArgsException(ErrorCode.InvalidArgumentName, elementId);

            throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId);
        }
        if (args.Length > 0)
            throw new ArgsException(ErrorCode.UnexpectedArgument, args[0][1]);
    }
}