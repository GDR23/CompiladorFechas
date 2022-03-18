using System;
namespace Scanner
{
    public enum TokenType
    {
        hour,
        minute,
        month,
        year,
        day,
        plus,
        minus,
        equals,
        leftParens,
        rightParens,
        stringLiteral,
        comma,
        dot,
        letKeyword,
        nowKeyword,
        dateLiteral,
        printKeyword,
        Identifier,
        EOF,
        BasicType
    }
}