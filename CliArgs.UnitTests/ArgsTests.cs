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
    public void Create_SimpleBoolPresent_CanGetBoolValue()
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
    public void Create_SimpleStringPresent_CanGetStringValue()
    {
        Args args = new("x*", new[] { "-x", "param" });
        
        Assert.Equal("param", args.GetString('x'));
        Assert.Equal(1, args.Count);
    }

    [Fact]
    public void Create_MissingStringArgument_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("x*", new[] { "-x" }));
        
        Assert.Equal(ErrorCode.MissingString, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_SchemaForMultipleArguments_ParseAllArgumentsFromSchema()
    {
        Args args = new("x,y,z*", new[] { "-xy", "-z", "param" });
        
        Assert.True(args.GetBoolean('x'));
        Assert.True(args.GetBoolean('y'));
        Assert.Equal("param", args.GetString('z'));
        Assert.Equal(3, args.Count);
    }

    [Fact]
    public void Create_WithSpacesInSchema_IgnoreSpaces()
    {
        Args args = new("x, y*", new[] { "-x", "-y", "param" });
        
        Assert.True(args.GetBoolean('x'));
        Assert.Equal("param", args.GetString('y'));
        Assert.Equal(2, args.Count);
    }

    [Fact]
    public void Create_SimpleIntPresent_CanGetIntValue()
    {
        Args args = new("x#", new[] { "-x", "42" });
        
        Assert.Equal(42, args.GetInteger('x'));
        Assert.Equal(1, args.Count);
    }

    [Fact]
    public void Create_InvalidInteger_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("x#", new[] { "-x", "Forty two" }));
        
        Assert.Equal(ErrorCode.InvalidInteger, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_MissingInteger_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("x#", new[] { "-x" }));
        
        Assert.Equal(ErrorCode.MissingInteger, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_SimpleDoublePresent_CanGetDoubleValue()
    {
        Args args = new("x##", new[] { "-x", "42.2" });
        
        Assert.Equal(42.2, args.GetDouble('x'), .001);
        Assert.Equal(1, args.Count);
    }

    [Fact]
    public void Create_InvalidDouble_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("x##", new[] { "-x", "Forty two" }));
        
        Assert.Equal(ErrorCode.InvalidDouble, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void Create_MissingDouble_ShouldThrowArgsException()
    {
        ArgsException ex = Assert.Throws<ArgsException>(
            () => new Args("x##", new[] { "-x" }));
        
        Assert.Equal(ErrorCode.MissingDouble, ex.ErrorCode);
        Assert.Equal('x', ex.ErrorArgumentId);
    }

    [Fact]
    public void GetBoolean_ForNotBoolArgument_ReturnsFalse()
    {
        Args args = new("x*", new[] { "-x", "param" });
        
        Assert.False(args.GetBoolean('x'));
    }

    [Fact]
    public void GetString_ForNotStringArgument_ReturnsEmptyString()
    {
        Args args = new("x", new[] { "-x" });
        
        Assert.Empty(args.GetString('x'));
    }

    [Fact]
    public void GetInteger_ForNotIntArgument_ReturnsZero()
    {
        Args args = new("x", new[] { "-x" });
        
        Assert.Equal(0, args.GetInteger('x'));
    }
}