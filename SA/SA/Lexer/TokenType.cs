using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Lexer
{
      public enum TokenType
    {
        Id,
        Number,
        DataType,
        Relation,
        Assignment,
        If,
        Else,
        OpenBrace,
        ClosedBrace,
        OpenBracket,
        ClosedBracket,
        OpenParentheses,
        ClosedParentheses,
        SemiColon,
        PlusPlus,
        BinaryOperator,
        While,
        For,
        Do,
        Comma,
        SingleOperator,
        Assign,
        Return,
        Class,
        IneritanceSymbole,
        ClassIdentifires,
        Base,
        Override,
        Interface,
        New,
        Dot,
        DoubleCotation,
        tagCotation,
        Abstract,
        OpenRecursionSymbole,
        Therad,
        Underline,
        AndOp,
        OrOp,
        ComentOp,
        Using
    }
}
