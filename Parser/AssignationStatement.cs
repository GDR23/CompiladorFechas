using System;

namespace Parser
{
    public class AssignationStatement : Statement
    {
        public IdExpression Id { get; }
        public Expression Expression { get; }
        public AssignationStatement(IdExpression id, Expression expression)
        {
            Id = id;
            Expression = expression;
            this.ValidateSemantic();
        }

        public override void ValidateSemantic()
        {
          if(Id.GetExpressionType() != Expression.GetExpressionType())
            {
                throw new ApplicationException($"Type {Id.GetExpressionType()} is not the same as {Expression.GetExpressionType()} ");
            }
        }
    }
}



