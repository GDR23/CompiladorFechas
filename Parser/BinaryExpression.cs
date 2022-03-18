using System;
using Scanner;

namespace Parser
{
    public abstract class BinaryExpression : Expression
    {
        public Expression LeftExpression { get; }

        public Expression RightExpression { get; }

        public BinaryExpression(Token token, Expression leftExpression, Expression rightExpression, Type type) : base(type, token)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }
    }
}

