﻿using Frognar.CliArgs.Enums;
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
    
    [Fact]
    public void Message_InvalidArgumentName()
    {
        ArgsException ex = new(ErrorCode.InvalidArgumentName, '#');
        
        Assert.Equal("'#' is not a valid argument name.", ex.Message);
    }
    
    [Fact]
    public void Message_InvalidArgumentFormat()
    {
        ArgsException ex = new(ErrorCode.InvalidArgumentFormat, 'x', "$");
        
        Assert.Equal("'$' is not a valid argument format.", ex.Message);
    }
}