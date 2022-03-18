
using Scanner;

namespace Parser
{
    public class ConstantExpression : Expression
    {
        public ConstantExpression(Type type, Token token) : base(type, token)
        {
        }
        public override Type GetExpressionType()
        {
            return type;
        }
    }

}

