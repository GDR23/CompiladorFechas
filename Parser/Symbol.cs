using System;

namespace Parser
{
    public class Symbol
    {
        public Symbol(IdExpression id)
        {
            Id = id;
        }

        public IdExpression Id { get; }
    }
}
