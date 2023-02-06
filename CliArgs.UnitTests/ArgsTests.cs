using Frognar.CliArgs;

namespace CliArgs.UnitTests;

public class ArgsTests
{
    [Fact]
    public void CanCreateArgs()
    {
        Args _ = new("", Array.Empty<string>());
    }
}