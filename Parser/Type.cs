using System;
using Scanner;

namespace Parser
{
    public class Type : IEquatable<Type>
    {
        public string Lexeme { get; private set; }

        public TokenType TokenType { get; set; }

        public Type(string lexeme, TokenType tokenType)
        {
            Lexeme = lexeme;
            TokenType = tokenType;
        }

        public static Type Date => new Type("date", TokenType.BasicType);

        public static Type String => new Type("string", TokenType.BasicType);

        public static Type Year => new Type("year", TokenType.BasicType);
        public static Type Month => new Type("month", TokenType.BasicType);
        public static Type Hour => new Type("hour", TokenType.BasicType);
        public static Type Minute => new Type("minute", TokenType.BasicType);
        public static Type Day => new Type("day", TokenType.BasicType);
        public bool Equals(Type other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Lexeme == other.Lexeme && TokenType == other.TokenType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Type)obj);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Lexeme, TokenType).GetHashCode();
        }

        public override string ToString()
        {
            return Lexeme;
        }

        public static bool operator ==(Type a, Type b) => a.Equals(b);

        public static bool operator !=(Type a, Type b) => !a.Equals(b);
    }
}

