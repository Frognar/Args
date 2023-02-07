using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => argsMap.Keys.Count;
    readonly Dictionary<char, object> argsMap = new();

    public Args(string schema, string[] args)
    {
        ParseSchema(schema);
        if (schema.Length == 0 && args.Length > 0)
            throw new ArgsException(ErrorCode.UnexpectedArgument, args[0][1]);
    }

    void ParseSchema(string schema)
    {
        if (schema.Length > 0)
            ParseSchemaElement(schema);
    }

    void ParseSchemaElement(string element)
    {
        char elementId = element[0];
        string elementTail = element[1..];
        ValidateSchemaElementId(elementId);
        if (elementTail.Any())
            throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId);

        argsMap[elementId] = true;
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