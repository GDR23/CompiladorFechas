using System;

using System.Collections.Generic;
using System.Linq;

using Scanner;

namespace Parser
{
    public class Parser
    {
        private readonly Scanner.Scanner scanner;
        private readonly Logger logger;
        private Token lookAhead;
      //  public Environment topEnvironment;

        public Parser(Scanner.Scanner scanner, Logger logger)
        {
            this.scanner = scanner;
            this.logger = logger;
            this.Move();
        }

        public void Parse()
        {
            Code();
        }

        private Statement Code()
        {

            return Statements();
        }

        private Statement Statements()
        {
            if(this.lookAhead.TokenType== TokenType.EOF)
            {
                return null;
            }
            else
            {
               var x = StatementBlock();
               var y = Statements();
                return new SequenceStatement(x,y);
            }
            
        }

        private Statement StatementBlock()
        {
            if (this.lookAhead.TokenType == TokenType.printKeyword)
            {
               return  PrintBlock();
            }
            else if(this.lookAhead.TokenType == TokenType.letKeyword)
            {
               return  AssignationDeclare();
            }
            else 
            {
               return Assignation();
            }
           
        }

        private void Assignation()
        {
            Match(TokenType.Identifier);
            Match(TokenType.equals);
            ExpressionBlock();

        }

        private void AssignationDeclare()
        {
            Match(TokenType.letKeyword);
            Match(TokenType.Identifier);
            Match(TokenType.equals);
            ExpressionBlock();


        }

        private Statement PrintBlock()
        {
            Match(TokenType.printKeyword);
            Match(TokenType.leftParens);
            var expr = Arguments();
            Match(TokenType.rightParens);
            return new PrintStatement(expr) ;
        }

        private IEnumerable<Expression> Arguments()
        {
            var list = new List<Expression>();
           list.Add( ExpressionBlock());
           list.AddRange(ExpressionsBlock());
            return list;
        }

        private IEnumerable<Expression> ExpressionsBlock()
        {
            var list = new List<Expression>();
            if (this.lookAhead.TokenType == TokenType.comma)
            {
                Match(TokenType.comma);
               list.Add( ExpressionBlock());
               list.AddRange( ExpressionsBlock());

            }
            return list;
        }

        private Expression ExpressionBlock()
        {
            var x = Factor();
            return ExpressionPrime(x);
        }

        private Expression ExpressionPrime(Expression expr)
        {
            if(this.lookAhead.TokenType == TokenType.plus)
            {
                var tok = this.lookAhead;
                Match(TokenType.plus);
                var x = Factor();
                var exprR= ExpressionPrime(x);

                return new AritmeticExpression(tok, expr, exprR);
            }
            else if (this.lookAhead.TokenType == TokenType.minus)
            {
                var tok = this.lookAhead;
                Match(TokenType.minus);
                var x =Factor();
                var exprR= ExpressionPrime(x);
                return new AritmeticExpression(tok, expr, exprR);
            }
            return expr;
        }

        private Expression Factor()
        {
            switch (this.lookAhead.TokenType)
            {
                case TokenType.dateLiteral:
                    var tok = this.lookAhead;
                    Match(TokenType.dateLiteral);
                    return new ConstantExpression(Type.Date, tok);
                case TokenType.stringLiteral:
                    tok = this.lookAhead;
                    Match(TokenType.stringLiteral);
                    return new ConstantExpression(Type.String, tok);
                case TokenType.hour:
                    tok = this.lookAhead;
                    Match(TokenType.hour);
                    return new ConstantExpression(Type.Hour, tok);
                case TokenType.minute:
                    tok = this.lookAhead;
                    Match(TokenType.minute);
                    return new ConstantExpression(Type.Minute, tok);
                case TokenType.month:
                    tok = this.lookAhead;
                    Match(TokenType.month);
                    return new ConstantExpression(Type.Month, tok);
                case TokenType.year:
                    tok = this.lookAhead;
                    Match(TokenType.year);
                    return new ConstantExpression(Type.Year, tok);
                case TokenType.nowKeyword:
                    tok = this.lookAhead;
                    Match(TokenType.nowKeyword);
                    Match(TokenType.leftParens);
                    Match(TokenType.rightParens);
                    return new ConstantExpression(Type.Date, tok);
                case TokenType.Identifier:
                    tok = this.lookAhead;
                    Match(TokenType.Identifier);
                  return new IdExpression(Type.Hour, tok);
                case TokenType.day:
                    tok = this.lookAhead;
                    Match(TokenType.day);
                    return new ConstantExpression(Type.Day, tok);
                default:
                    tok = this.lookAhead;
                    Match(TokenType.dateLiteral);
                    return new ConstantExpression(Type.Date, tok);

            }
        }

        private void Move()
        {
            this.lookAhead = this.scanner.GetNextToken();
        }

        private void Match(TokenType expectedTokenType)
        {
            if (this.lookAhead.TokenType != expectedTokenType)
            {
                this.logger.Error($"Syntax Error! expected token {expectedTokenType} but found {this.lookAhead.TokenType} on line {this.lookAhead.Line} and column {this.lookAhead.Column}");
                throw new System.ApplicationException($"Syntax Error! expected token {expectedTokenType} but found {this.lookAhead.TokenType} on line {this.lookAhead.Line} and column {this.lookAhead.Column}");
            }
            this.Move();
        }
    }
}
