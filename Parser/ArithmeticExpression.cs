using System.Collections.Generic;
using Scanner;

namespace Parser
{
    public class AritmeticExpression : BinaryExpression
    {
        private readonly Dictionary<(Type, Type, TokenType), Type> _typeRules;

        public AritmeticExpression(Token token, Expression leftExpression, Expression rightExpression) : base(token, leftExpression, rightExpression, null)
        {
            _typeRules = new Dictionary<(Type, Type, TokenType), Type>
            {
                { (Type.Date, Type.Minute, TokenType.plus), Type.Date },
                { (Type.Date, Type.Hour, TokenType.plus), Type.Date},
                { (Type.Date, Type.Day, TokenType.plus), Type.Date},
                { (Type.Date, Type.Month, TokenType.plus), Type.Date},
                { (Type.Date, Type.Year, TokenType.plus), Type.Date },
                { (Type.Date, Type.Minute, TokenType.minus), Type.Date },
                { (Type.Date, Type.Hour, TokenType.minus), Type.Date},
                { (Type.Date, Type.Day, TokenType.minus), Type.Date},
                { (Type.Date, Type.Month,TokenType.minus), Type.Date},
                { (Type.Date, Type.Year, TokenType.minus), Type.Date },


            };
        }
        public override Type GetExpressionType()
        {
            var leftType = LeftExpression.GetExpressionType();
            var rightType = RightExpression.GetExpressionType();
            if (_typeRules.TryGetValue((leftType, rightType, Token.TokenType), out var resultType))
            {
                return resultType;
            }

            throw new System.ApplicationException($"Cannot perform {Token.Lexeme} operation on types {leftType} and {rightType}");
        }
    }
}

