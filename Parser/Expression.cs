using System;
using Scanner;

namespace Parser
{
    public abstract class Expression : Node
    {
        public Type type { get; set; }
        public Token Token { get; }

        public Expression(Type type, Token token)
        {
            Token = token;
            this.type = type;
        }
        public abstract Type GetExpressionType();
    }
}
