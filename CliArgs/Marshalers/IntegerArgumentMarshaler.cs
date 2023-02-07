namespace Frognar.CliArgs.Marshalers;

public class IntegerArgumentMarshaler : ArgumentMarshaler
{
    int value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        currentArgument.MoveNext();
        value = int.Parse(currentArgument.Current);
    }

    public static int GetValue(ArgumentMarshaler? am)
    {
        return (am as IntegerArgumentMarshaler)!.value;
    }
}