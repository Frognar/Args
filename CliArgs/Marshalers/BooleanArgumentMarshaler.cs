namespace Frognar.CliArgs.Marshalers;

public class BooleanArgumentMarshaler : ArgumentMarshaler
{
    bool value;
    
    public void Set(IEnumerator<string> currentArgument)
    {
        value = true;
    }

    public static bool GetValue(ArgumentMarshaler? am)
    {
        if (am is BooleanArgumentMarshaler bam)
            return bam.value;

        return false;
    }
}