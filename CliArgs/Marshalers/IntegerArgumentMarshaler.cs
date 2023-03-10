using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class IntegerArgumentMarshaler : ArgumentMarshaler
{
    int value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        if (currentArgument.MoveNext() == false)
            throw new ArgsException(ErrorCode.MissingInteger);
        
        if (int.TryParse(currentArgument.Current, out value) == false)
            throw new ArgsException(ErrorCode.InvalidInteger, currentArgument.Current);
    }

    public static int GetValue(ArgumentMarshaler? am)
    {
        if (am is IntegerArgumentMarshaler iam)
            return iam.value;

        return 0;
    }
}