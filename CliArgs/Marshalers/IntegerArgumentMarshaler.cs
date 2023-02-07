using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class IntegerArgumentMarshaler : ArgumentMarshaler
{
    int value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        currentArgument.MoveNext();
        if (int.TryParse(currentArgument.Current, out value) == false)
            throw new ArgsException(ErrorCode.InvalidInteger);
    }

    public static int GetValue(ArgumentMarshaler? am)
    {
        return (am as IntegerArgumentMarshaler)!.value;
    }
}