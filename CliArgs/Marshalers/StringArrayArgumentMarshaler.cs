using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class StringArrayArgumentMarshaler : ArgumentMarshaler
{
    readonly List<string> values = new();
    
    public void Set(IEnumerator<string> currentArgument)
    {
        if (currentArgument.MoveNext() == false)
            throw new ArgsException(ErrorCode.MissingString);
        
        values.Add(currentArgument.Current);
    }

    public static string[] GetValue(ArgumentMarshaler? am)
    {
        return (am as StringArrayArgumentMarshaler)!.values.ToArray();
    }
}