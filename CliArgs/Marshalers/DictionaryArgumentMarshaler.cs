namespace Frognar.CliArgs.Marshalers;

public class DictionaryArgumentMarshaler : ArgumentMarshaler
{
    Dictionary<string, string> value = new();

    public void Set(IEnumerator<string> currentArgument)
    {
        currentArgument.MoveNext();
        foreach (string s in currentArgument.Current.Split(','))
        {
            string[] kv = s.Split(':');
            value[kv[0]] = kv[1];
        }
    }

    public static Dictionary<string, string> GetValue(ArgumentMarshaler? am)
    {
        return (am as DictionaryArgumentMarshaler)!.value;
    }
}