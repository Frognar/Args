using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class StringArgumentMarshaler : ArgumentMarshaler
{
    string value = "";

    public void Set(IEnumerator<string> currentArgument)
    {
        if (currentArgument.MoveNext() == false)
            throw new ArgsException(ErrorCode.MissingString);

        value = currentArgument.Current;
    }

    public static string GetValue(ArgumentMarshaler? am)
    {
        if (am is StringArgumentMarshaler sam)
            return sam.value;

        return "";
    }
}