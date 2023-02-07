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
            string elementTail = schema[1..];
            ValidateSchemaElementId(elementId);
            if (elementTail.Any())
                throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId);

            Count = 1;
        }
        else if (args.Length > 0)
            throw new ArgsException(ErrorCode.UnexpectedArgument, args[0][1]);
    }

    static void ValidateSchemaElementId(char elementId)
    {
        if (char.IsLetter(elementId) == false)
            throw new ArgsException(ErrorCode.InvalidArgumentName, elementId);
    }

    public bool GetBoolean(char elementId)
    {
        return true;
    }
}