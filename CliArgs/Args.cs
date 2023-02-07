using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count { get; }
    
    public Args(string schema, string[] args)
    {
        if (schema.Length > 0)
        {
            char elementId = schema[0];
            if (char.IsLetter(elementId) == false)
                throw new ArgsException(ErrorCode.InvalidArgumentName, elementId);
            if (schema.Skip(1).Any())
                throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId);

            Count = 1;
        }
        else if (args.Length > 0)
            throw new ArgsException(ErrorCode.UnexpectedArgument, args[0][1]);
    }

    public bool GetBoolean(char elementId)
    {
        return true;
    }
}