using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Char;

namespace SA.Lexer
{
     public class Lexer_
    {

        private CsharpDictionary Dictionary = new CsharpDictionary();
        public List<Token> Tokenize(string source)
        {
            List<Token> tokens = new List<Token>();

            var reader = new StringReader(source);
            //تا وقتی به انتهای رشته ورودی نرسه ادامه میده
            while (reader.Peek() != -1)
            {

                var ch = (char)reader.Read();
                //اگه اینتر یا تب یا اینتر یا شروع خط بعد بود
                if (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r')
                {
                    continue;
                }
                //اگر اولین کارکتری که میخونه عدد بود
                if (IsDigit(ch))
                {
                    //قراره که داخل این کارکتر ها بهم بچسپن و یک استرینگ در نهایت ساخته بشه
                    StringBuilder stringBuilder = new StringBuilder();
                    //برای چک کردن عدد اعشاری
                    int dotCounter = 0;
                    do
                    {
                        stringBuilder.Append(ch);
                        //برای چک کردن عدد اعشاری
                        if ((char)reader.Peek() == '.' && dotCounter == 0)
                        {
                            ch = (char)reader.Read();
                            stringBuilder.Append(ch);
                            dotCounter++;
                        }
                        if (IsDigit((char)reader.Peek()))
                        {
                            ch = (char)reader.Read();
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                    tokens.Add(new Token(TokenType.Number, stringBuilder.ToString()));
                }
                //حروف الفبا
                else if (IsLetter(ch))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    //تا وقتی کارکتر ها حروف الفبا یا عدد هستن بخون اضافه کن
                    do
                    {
                        stringBuilder.Append(ch);
                        if (IsLetterOrDigit((char)reader.Peek()))
                        {
                            ch = (char)reader.Read();
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                    var word = stringBuilder.ToString();

                    var token = Dictionary.Identify(word);
                    //زمانی وارد ایف میشه که یک کلمه کیدی باشه
                    if (token != null)
                    {
                        tokens.Add(token);
                    }
                    //وقتی وارد الس میشه که اسم متغیر باشه
                    else
                    {
                        tokens.Add(new Token(TokenType.Id, word));
                    }
                }
                else
                { // symbols
                    char peek = (char)reader.Peek();
                    Token s1 = Dictionary.Identify(ch.ToString());
                    Token s2 = Dictionary.Identify($"{ch}{peek}");
                    //ینی جزع دوتا اوپه ریتوری ها بوده
                    if (s2 != null)
                    {
                        reader.Read();
                        tokens.Add(s2);
                    }
                    //ینی جزع یدونه ای ها بوده
                    else if (s1 != null)
                    {
                        tokens.Add(s1);
                    }
                    else if (ch == '_')
                    {
                        Token s3 = new Token(TokenType.Underline, "_");
                    }
                    else if (ch == '&')
                    {
                        Token s3 = new Token(TokenType.AndOp, "&");
                    }
                    else if (ch.ToString() == "//")
                    {
                        Token s3 = new Token(TokenType.ComentOp, ch.ToString());
                    }
                    else throw new Exception($"char({ch},peek:{peek}) was not expected!");
                }
            }
            return tokens;
        }
    }
}
