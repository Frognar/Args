using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => argsMap.Keys.Count;
    readonly Dictionary<char, object> argsMap = new();

    public Args(string schema, IEnumerable<string> args)
    {
        ParseSchema(schema);
        ParseArgumentStrings(args.ToList());
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
        if (elementTail.Length == 0)
            argsMap[elementId] = false;
        else
            throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId);
    }

    static void ValidateSchemaElementId(char elementId)
    {
        if (char.IsLetter(elementId) == false)
            throw new ArgsException(ErrorCode.InvalidArgumentName, elementId);
    }

    void ParseArgumentStrings(List<string> argsList)
    {
        foreach(string arg in argsList)
        {
            if (arg.StartsWith("-"))
            {
                if (argsMap.ContainsKey(arg[1]) == false)
                    throw new ArgsException(ErrorCode.UnexpectedArgument, arg[1]);
            }
        }
    }

    public bool GetBoolean(char elementId)
    {
        return true;
    }
}