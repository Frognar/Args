using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;
using Frognar.CliArgs.Marshalers;

namespace Frognar.CliArgs;

public class Args
{
    public int Count => argsFound.Count;
    readonly Dictionary<char, ArgumentMarshaler> argsMarshalers;
    readonly ISet<char> argsFound;
    IEnumerator<string> currentArgument = default!;

    public Args(string schema, IEnumerable<string> args)
    {
        argsMarshalers = new Dictionary<char, ArgumentMarshaler>();
        argsFound = new HashSet<char>();
        
        ParseSchema(schema);
        ParseArgumentStrings(args.ToList());
    }

    void ParseSchema(string schema)
    {
        string[] elements = schema.Split(',', 
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        foreach (string element in elements)
            ParseSchemaElement(element);
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
        else if (elementTail.Equals("#"))
            argsMarshalers[elementId] = new IntegerArgumentMarshaler();
        else if (elementTail.Equals("##"))
            argsMarshalers[elementId] = new DoubleArgumentMarshaler();
        else if (elementTail.Equals("[*]"))
            argsMarshalers[elementId] = new StringArrayArgumentMarshaler();
        else if (elementTail.Equals("&"))
            argsMarshalers[elementId] = new DictionaryArgumentMarshaler();
        else
            throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId, elementTail);
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
                continue;

            ParseArgumentString(arg[1..]);
        }
    }

    void ParseArgumentString(string arg)
    {
        foreach (char c in arg)
            ParseArgumentCharacter(c);
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

    public int GetInteger(char elementId)
    {
        return IntegerArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }

    public double GetDouble(char elementId)
    {
        return DoubleArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }

    public string[] GetStringArray(char elementId)
    {
        return StringArrayArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }

    public Dictionary<string,string> GetDictionary(char elementId)
    {
        return DictionaryArgumentMarshaler.GetValue(argsMarshalers.GetValueOrDefault(elementId));
    }
}