using System.Globalization;

namespace Frognar.CliArgs.Marshalers;

public class DoubleArgumentMarshaler : ArgumentMarshaler
{
    double value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        currentArgument.MoveNext();
        value = double.Parse(currentArgument.Current, CultureInfo.InvariantCulture);
    }

    public static double GetValue(ArgumentMarshaler? am)
    {
        return (am as DoubleArgumentMarshaler)!.value;
    }
}