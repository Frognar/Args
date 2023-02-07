namespace Frognar.CliArgs.Marshalers;

public interface ArgumentMarshaler
{
    void Set(IEnumerator<string> currentArgument);
}