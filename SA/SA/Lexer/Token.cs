using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Lexer
{
      public class Token
    {
        public TokenType TokenType { get; set; }
       
        public string Attribute { get; set; }

        public Token(TokenType tokenType, String attribute)
        {
            this.TokenType = tokenType;
            this.Attribute = attribute;
        }
        //برای چاپ کردن مقادیر توکن ها
        public override string ToString()
        {
            return $"{TokenType}({Attribute})";
        }

        //میاد چک میکنه دوتا توکن باهم برابر هستن یانه
        public  static bool Equals(Token lookahead , Token token) {

            return (lookahead.TokenType == token.TokenType && lookahead.Attribute == token.Attribute);

        }

    }
}
