using System;
using System.Linq;
using System.Collections.Generic;


namespace Parser
{
        public class PrintStatement : Statement
        {
            public PrintStatement(IEnumerable<Expression> parameters)
            {
                Parameters = parameters;
                this.ValidateSemantic();
            }

            public IEnumerable<Expression> Parameters { get; }

           
            public override void ValidateSemantic()
            {
               
               
                
            }
        }
    }

