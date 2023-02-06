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
    public void Create_WithoutSchemaButWithOneArgument_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("", new[] { "-x" }));
        
        Assert.Equal(ErrorCode.UnexpectedArgument, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_WithoutSchemaButWithMultipleArguments_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("", new[] { "-y", "-x" }));
        
        Assert.Equal(ErrorCode.UnexpectedArgument, ex.ErrorCode);
        Assert.Equal('y', ex.ErrorArgumentId);
    }
}