namespace Frognar.CliArgs.Marshalers;

public class StringArrayArgumentMarshaler : ArgumentMarshaler
{
    readonly List<string> values = new();
    
    public void Set(IEnumerator<string> currentArgument)
    {
        while(currentArgument.MoveNext())
            values.Add(currentArgument.Current);
    }

    public static string[] GetValue(ArgumentMarshaler? am)
    {
        return (am as StringArrayArgumentMarshaler)!.values.ToArray();
    }
}