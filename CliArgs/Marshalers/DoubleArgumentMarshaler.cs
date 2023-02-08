using System.Globalization;
using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class DoubleArgumentMarshaler : ArgumentMarshaler
{
    double value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        currentArgument.MoveNext();
        if (double.TryParse(currentArgument.Current, CultureInfo.InvariantCulture, out value) == false)
            throw new ArgsException(ErrorCode.InvalidDouble);
    }

    public static double GetValue(ArgumentMarshaler? am)
    {
        return (am as DoubleArgumentMarshaler)!.value;
    }
}