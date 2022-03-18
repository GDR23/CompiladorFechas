using System;

using System.Collections.Generic;
using Parser;
using Scanner;

namespace MiniLanguageCompiler.Parser
{
    public class Environment
    {
        public string lexemM { get; set; }
        private readonly Dictionary<string, Symbol> _symbolTable;
        private TokenType nameEnvi { get; }
        protected Environment Previous;

        public Environment(Environment previous, TokenType n)
        {
            lexemM = "";
            Previous = previous;
            nameEnvi = n;
            _symbolTable = new Dictionary<string, Symbol>();
        }


        public void Put(string lexeme, IdExpression id)
        {
           
                if (_symbolTable.ContainsKey(lexeme))
                {
                    throw new ApplicationException($"Symbol {lexeme} was previously defined in this scope");
                }
                _symbolTable.Add(lexeme, new Symbol(id));
           
               }
          
        }
      
        public Symbol Get(string lexeme)
        {
            for (var currentEnv = this; currentEnv != null; currentEnv = currentEnv.Previous)
            {
                if (currentEnv._symbolTable.TryGetValue(lexeme, out var symbol))
                {
                    return symbol;
                }
            }

            throw new ApplicationException($"Symbol {lexeme} doesn't exist in current context.");
        }




    }
}
