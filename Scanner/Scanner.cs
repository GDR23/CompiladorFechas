using System;
using System.Collections.Generic;
using System.Text;

namespace Scanner
{
    public class Scanner
    {
        private Input input;
        private readonly Logger logger;

        private readonly Dictionary<string, TokenType> keywords;

        public Scanner(Input input, Logger logger)
        {
            this.input = input;
            this.logger = logger;
            this.keywords = new Dictionary<string, TokenType>
            {
                ["let"] = TokenType.letKeyword,
                ["print"] = TokenType.printKeyword,
                ["now"] = TokenType.nowKeyword,
            };
        }

        public Token GetNextToken()
        {
            var lexeme = new StringBuilder();
            var currentChar = this.GetNextChar();
            while (currentChar != '\0')
            {
                while (char.IsWhiteSpace(currentChar) || currentChar == '\n')
                {
                    currentChar = this.GetNextChar();
                }

                if (char.IsLetter(currentChar))
                {
                    lexeme.Append(currentChar);
                    currentChar = this.PeekNextChar();
                    while (char.IsLetterOrDigit(currentChar))
                    {
                        currentChar = this.GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = this.PeekNextChar();
                    }

                    var tokenLexeme = lexeme.ToString();
                    if (this.keywords.ContainsKey(tokenLexeme))
                    {
                        return BuildToken(tokenLexeme, this.keywords[tokenLexeme]);
                    }

                    return BuildToken(tokenLexeme, TokenType.Identifier);
                }
                else if (char.IsDigit(currentChar))
                {
                    lexeme.Append(currentChar);
                    currentChar = PeekNextChar();
                    while (char.IsDigit(currentChar))
                    {
                        currentChar = GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = PeekNextChar();
                    }

                    if (currentChar == '.')
                    {
                        currentChar = GetNextChar();
                        lexeme.Append(currentChar);
                        currentChar = PeekNextChar();
                        while (char.IsDigit(currentChar))
                        {
                            currentChar = GetNextChar();
                            lexeme.Append(currentChar);
                            currentChar = PeekNextChar();
                        }
                    }
                    currentChar = GetNextChar();
                    if (currentChar == 'd')
                    {
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.day);
                    }
                    else if (currentChar == 'h')
                    {
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.hour);
                    }
                    else if (currentChar == 'm')
                    {
                        lexeme.Append(currentChar);
                   
                        return BuildToken(lexeme.ToString(), TokenType.month);
                    }
                    else if (currentChar == 'M')
                    {
                        lexeme.Append(currentChar);
                       
                        return BuildToken(lexeme.ToString(), TokenType.minute);
                    }
                    else if (currentChar == 'y')
                    {
                        lexeme.Append(currentChar);
                       
                        return BuildToken(lexeme.ToString(), TokenType.year);
                    }
                    else if (currentChar == '/')
                    {
                        lexeme.Append(currentChar);
                        currentChar = GetNextChar();
                        if (char.IsDigit(currentChar))
                        {
                            lexeme.Append(currentChar);
                            currentChar = GetNextChar();
                            if (char.IsDigit(currentChar))
                            {
                                lexeme.Append(currentChar);
                                currentChar = GetNextChar();
                                if (currentChar == '/')
                                {
                                    lexeme.Append(currentChar);
                                    currentChar = GetNextChar();
                                    if (char.IsDigit(currentChar)){
                                        lexeme.Append(currentChar);
                                        currentChar = GetNextChar();

                                        if (char.IsDigit(currentChar))
                                        {
                                            lexeme.Append(currentChar);
                                            currentChar = GetNextChar();

                                            if (char.IsDigit(currentChar))
                                            {
                                                lexeme.Append(currentChar);
                                                currentChar = GetNextChar();
                                                if (char.IsDigit(currentChar))
                                                {
                                                    lexeme.Append(currentChar);
                                                    currentChar = GetNextChar();
                                                    while(currentChar==' ')
                                                    {
                                                        lexeme.Append(currentChar);
                                                        currentChar = GetNextChar();
                                                    }
                                                    if (char.IsDigit(currentChar))
                                                    {
                                                        lexeme.Append(currentChar);
                                                        currentChar = GetNextChar();

                                                        if (char.IsDigit(currentChar))
                                                        {
                                                            lexeme.Append(currentChar);
                                                            currentChar = GetNextChar();
                                                            if (currentChar ==':')
                                                            {
                                                                lexeme.Append(currentChar);
                                                                currentChar = GetNextChar();
                                                                if (char.IsDigit(currentChar))
                                                                {
                                                                    lexeme.Append(currentChar);
                                                                    currentChar = GetNextChar();

                                                                }
                                                                if (char.IsDigit(currentChar))
                                                                {
                                                                    lexeme.Append(currentChar);
                                                                    currentChar = GetNextChar();
                                                                   return BuildToken(lexeme.ToString(), TokenType.dateLiteral);
                                                                }
                                                            }
                                                        }

                                                    }
                                                }

                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                switch (currentChar)
                {
                    case '\0':
                        return BuildToken("\0", TokenType.EOF);
                    case '+':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.plus);
                    case '-':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.minus);
                    case '(':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.leftParens);
                    case ')':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.rightParens);
                    case ',':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.comma);
                    case '=':

                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.equals);
                    case '\"':
                        lexeme.Append(currentChar);
                        currentChar = GetNextChar();
                        while (currentChar != '\"' && currentChar != '\0')
                        {
                            lexeme.Append(currentChar);
                            currentChar = GetNextChar();
                        }
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.stringLiteral);
                    case '.':
                        lexeme.Append(currentChar);
                        return BuildToken(lexeme.ToString(), TokenType.dot);

                    default:
                        break;
                }

                logger.Error($"Invalid character {currentChar} in line: {this.input.Position.Line} and column: {this.input.Position.Column}");
                currentChar = this.GetNextChar();
            }
            return BuildToken("\0", TokenType.EOF);
        }

        private Token BuildToken(string lexeme, TokenType tokenType)
        {
            return new Token
            {
                Column = this.input.Position.Column,
                Line = this.input.Position.Line,
                Lexeme = lexeme,
                TokenType = tokenType,
            };
        }

        private char GetNextChar()
        {
            var next = input.NextChar();
            input = next.Reminder;
            return next.Value;
        }

        private void ReturnCharBack(int x)
        {
            var next = input.BackChar(x);
            input = next.Reminder;
        }

        private char PeekNextChar()
        {
            var next = input.NextChar();
            return next.Value;
        }
    }
}
