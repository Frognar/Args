using System.Globalization;
using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace Frognar.CliArgs.Marshalers;

public class DoubleArgumentMarshaler : ArgumentMarshaler
{
    double value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        if (currentArgument.MoveNext() == false)
            throw new ArgsException(ErrorCode.MissingDouble);
        
        if (double.TryParse(currentArgument.Current, CultureInfo.InvariantCulture, out value) == false)
            throw new ArgsException(ErrorCode.InvalidDouble);
    }

    public static double GetValue(ArgumentMarshaler? am)
    {
        if (am is DoubleArgumentMarshaler dam)
            return dam.value;

        return 0.0;
    }
}