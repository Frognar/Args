using Frognar.CliArgs.Enums;
using Frognar.CliArgs.Exceptions;

namespace CliArgs.UnitTests;

public class ArgsExceptionTests
{
    [Fact]
    public void Message_UnexpectedArgument()
    {
        ArgsException ex = new(ErrorCode.UnexpectedArgument, 'x');
        
        Assert.Equal("Argument -x unexpected.", ex.Message);
    }
}