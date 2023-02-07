using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => argsFound.Count;
    readonly IDictionary<char, object> argsMap;
    readonly ISet<char> argsFound;

    public Args(string schema, IEnumerable<string> args)
    {
        argsMap = new Dictionary<char, object>();
        argsFound = new HashSet<char>();
        
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
        else if (elementTail.Equals("*"))
            argsMap[elementId] = "";
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
        for(int i = 0; i < argsList.Count; i++)
        {
            string arg = argsList[i];
            if (arg.StartsWith("-") == false)
                break;
            
            if (argsMap.ContainsKey(arg[1]))
            {
                argsFound.Add(arg[1]);
                if (argsMap[arg[1]] is bool)
                    argsMap[arg[1]] = true;
                else if (i == argsList.Count - 1)
                    throw new ArgsException(ErrorCode.MissingString, arg[1]);
                else
                    argsMap[arg[1]] = argsList[++i];
            }
            else
            {
                throw new ArgsException(ErrorCode.UnexpectedArgument, arg[1]);
            }
        }
    }

    public bool GetBoolean(char elementId)
    {
        return (bool)argsMap[elementId];
    }

    public string GetString(char elementId)
    {
        return (string)argsMap[elementId];
    }
}