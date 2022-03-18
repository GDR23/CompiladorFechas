
using Scanner;

namespace Parser

{
    public class IdExpression : Expression
    {
        public IdExpression(Type type, Token token) : base(type, token)
        {
        }

        public override Type GetExpressionType()
        {
            return type;
        }
    }
}

