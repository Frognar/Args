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

    [Fact]
    public void Create_NonLetterSchema_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("*", Array.Empty<string>()));
        
        Assert.Equal(ErrorCode.InvalidArgumentName, ex.ErrorCode);
        Assert.Equal('*', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_InvalidArgumentFormat_ShouldThrowException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("f~", Array.Empty<string>()));
        
        Assert.Equal(ErrorCode.InvalidArgumentFormat, ex.ErrorCode);
        Assert.Equal('f', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_SimpleBoolPreset_CanGetBoolValue()
    {
        Args args = new("x", new[] { "-x" });
        
        Assert.True(args.GetBoolean('x'));
        Assert.Equal(1, args.Count);
    }

    [Fact]
    public void Create_SimpleBoolPresetWithoutArguments_GetBooleanShouldReturnFalse()
    {
        Args args = new("x", Array.Empty<string>());
        
        Assert.False(args.GetBoolean('x'));
        Assert.Equal(0, args.Count);
    }

    [Fact]
    public void Create_SimpleStringPreset_CanGetStringValue()
    {
        Args args = new("x*", new[] { "-x", "param" });
        
        Assert.Equal("param", args.GetString('x'));
        Assert.Equal(1, args.Count);
    }
}