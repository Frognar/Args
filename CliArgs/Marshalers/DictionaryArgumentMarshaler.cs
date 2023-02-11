using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class DictionaryArgumentMarshaler : ArgumentMarshaler
{
    readonly Dictionary<string, string> value = new();

    public void Set(IEnumerator<string> currentArgument)
    {
        if (currentArgument.MoveNext() == false)
            throw new ArgsException(ErrorCode.MissingEntry);
        
        foreach (string s in currentArgument.Current.Split(','))
        {
            string[] kv = s.Split(':');
            if (kv.Length < 2)
                throw new ArgsException(ErrorCode.MalformedEntry, s);
            
            value[kv[0]] = kv[1];
        }
    }

    public static Dictionary<string, string> GetValue(ArgumentMarshaler? am)
    {
        if (am is DictionaryArgumentMarshaler dam)
            return dam.value;
        
        return new Dictionary<string, string>();
    }
}