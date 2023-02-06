using Frognar.CliArgs;
using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace CliArgs.UnitTests;

public class ArgsTests
{
    [Fact]
    public void CanCreateArgs()
    {
        Args _ = new("", Array.Empty<string>());
    }

    [Fact]
    public void Create_WithNoSchemaOrArguments_ShouldHaveNoArguments()
    {
        Args args = new("", Array.Empty<string>());
        Assert.Equal(0, args.Count);
    }

    [Fact]
    public void Create_WithoutSchemaButWithOneArgument_ShouldHaveThrown()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("", new[] { "-x" }));
        
        Assert.Equal(ErrorCode.UnexpectedArgument, ex.ErrorCode);
    }
}