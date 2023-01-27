using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Lexer
{
     public class CsharpDictionary
    {
        private IDictionary<string, Token> Reserved { get; }

        public CsharpDictionary()
        {
            Reserved = new Dictionary<string, Token>();
            // keywords
            addKeyword(new Token(TokenType.Return, "return"));
            addKeyword(new Token(TokenType.If, "if"));
            addKeyword(new Token(TokenType.Else, "else"));
            addKeyword(new Token(TokenType.For, "for"));
            addKeyword(new Token(TokenType.While, "while"));
            addKeyword(new Token(TokenType.Do, "do"));
            //datatypes
            addKeyword(new Token(TokenType.DataType, "int"));
            addKeyword(new Token(TokenType.DataType, "bool"));
            addKeyword(new Token(TokenType.DataType, "float"));
            addKeyword(new Token(TokenType.DataType, "void"));
            addKeyword(new Token(TokenType.DataType, "string"));
            // symbols
            addKeyword(new Token(TokenType.Assign, "="));
            addKeyword(new Token(TokenType.Assignment, "+="));
            addKeyword(new Token(TokenType.Assignment, "-="));
            addKeyword(new Token(TokenType.Assignment, "*="));
            addKeyword(new Token(TokenType.Assignment, "/="));
            addKeyword(new Token(TokenType.BinaryOperator, "+"));
            addKeyword(new Token(TokenType.BinaryOperator, "-"));
            addKeyword(new Token(TokenType.BinaryOperator, "*"));
            addKeyword(new Token(TokenType.BinaryOperator, "/"));
            addKeyword(new Token(TokenType.Relation, "=="));
            addKeyword(new Token(TokenType.Relation, ">="));
            addKeyword(new Token(TokenType.Relation, "<="));
            addKeyword(new Token(TokenType.Relation, "!="));
            addKeyword(new Token(TokenType.Relation, ">"));
            addKeyword(new Token(TokenType.Relation, "<"));
            addKeyword(new Token(TokenType.OpenBrace, "{"));
            addKeyword(new Token(TokenType.ClosedBrace, "}"));
            addKeyword(new Token(TokenType.OpenBracket, "["));
            addKeyword(new Token(TokenType.ClosedBracket, "]"));
            addKeyword(new Token(TokenType.OpenParentheses, "("));
            addKeyword(new Token(TokenType.ClosedParentheses, ")"));
            addKeyword(new Token(TokenType.SemiColon, ";"));
            addKeyword(new Token(TokenType.Comma, ","));
            addKeyword(new Token(TokenType.SingleOperator, "++"));
            addKeyword(new Token(TokenType.SingleOperator, "--"));
            addKeyword(new Token(TokenType.SingleOperator, "!"));
            addKeyword(new Token(TokenType.Class, "class"));
            addKeyword(new Token(TokenType.IneritanceSymbole, ":"));
            addKeyword(new Token(TokenType.ClassIdentifires, "public"));
            addKeyword(new Token(TokenType.ClassIdentifires, "private"));
            addKeyword(new Token(TokenType.ClassIdentifires, "protected"));
            addKeyword(new Token(TokenType.Base, "base"));
            addKeyword(new Token(TokenType.Override, "override"));
            addKeyword(new Token(TokenType.Interface, "interface"));
            addKeyword(new Token(TokenType.New, "new"));
            addKeyword(new Token(TokenType.Dot, "."));
            addKeyword(new Token(TokenType.DoubleCotation, "\""));
            addKeyword(new Token(TokenType.tagCotation, "\'"));
            addKeyword(new Token(TokenType.Abstract, "abstract"));
            addKeyword(new Token(TokenType.OpenRecursionSymbole, "this"));
            addKeyword(new Token(TokenType.OpenRecursionSymbole, "self"));
            addKeyword(new Token(TokenType.Therad, "Therad"));
            addKeyword(new Token(TokenType.Underline, "_"));
            addKeyword(new Token(TokenType.AndOp, "&"));
            addKeyword(new Token(TokenType.OrOp, "|"));
            addKeyword(new Token(TokenType.ComentOp, "//"));
            addKeyword(new Token(TokenType.ComentOp, "\\"));
            addKeyword(new Token(TokenType.Using, "using"));
           
        }
       private  void addKeyword(Token token)
        {
            Reserved[token.Attribute] = token;
        }
        //توي لكسر استفاده كرديم كه مياد شناسايي ميكنه كه كلمه اي كه
        //بهش داديم عضو كي وورد هاي داخل دیکشنری هست يا نه
        public Token Identify(string s) {
            Reserved.TryGetValue(s, out Token t);
            return t;
        }
    }
}
