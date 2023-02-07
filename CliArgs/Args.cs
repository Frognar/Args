using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;
using Frognar.CliArgs.Marshalers;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => argsFound.Count;
    readonly Dictionary<char, ArgumentMarshaler> argsMarshalers;
    readonly ISet<char> argsFound;
    List<string>.Enumerator currentArgument;

    public Args(string schema, IEnumerable<string> args)
    {
        argsMarshalers = new Dictionary<char, ArgumentMarshaler>();
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
            argsMarshalers[elementId] = new BooleanArgumentMarshaler();
        else if (elementTail.Equals("*"))
            argsMarshalers[elementId] = new StringArgumentMarshaler();
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
        for (currentArgument = argsList.GetEnumerator(); currentArgument.MoveNext();)
        {
            string arg = currentArgument.Current;
            if (arg.StartsWith("-") == false)
                break;

            ParseArgumentString(arg[1..]);
        }
    }

    void ParseArgumentString(string arg)
    {
        ParseArgumentCharacter(arg[0]);
    }

    void ParseArgumentCharacter(char argId)
    {
        if (argsMarshalers.ContainsKey(argId) == false)
            throw new ArgsException(ErrorCode.UnexpectedArgument, argId);
            
        argsFound.Add(argId);
        try
        {
            argsMarshalers[argId].Set(currentArgument);
        }
        catch (ArgsException ex)
        {
            ex.SetErrorArgumentId(argId);
            throw;
        }
    }

    public bool GetBoolean(char elementId)
    {
        return BooleanArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }

    public string GetString(char elementId)
    {
        return StringArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }
}