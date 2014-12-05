using System;
using System.Linq;
using OpenRasta.Sina;

namespace Tests.abnf
{
    public class rules : AbnfGrammar
    {
        [Xunit.FactAttribute]
        public void match()
        {
            Alpha.ShouldMatch('a'.To('z').Concat('A'.To('Z')));
            Bit.ShouldMatch("0", "1");
            Char.ShouldMatch('\x01'.To('\x7f'));
            CarriageReturn.ShouldMatch('\r');
            LineFeed.ShouldMatch('\n');
            CrLf.ShouldMatch("\r\n");
            Control.ShouldMatch('\x0'.To('\x1f').Concat(new[] { "\x7f" }));
            Digit.ShouldMatch('0'.To('9'));
            DoubleQuote.ShouldMatch('\"');
            HexDigit.ShouldMatch('0'.To('9').Concat('A'.To('F')));
            HorizontalTab.ShouldMatch('\t');
            WhiteSpace.ShouldMatch(" ", "\t");
            LinearWhiteSpace.ShouldMatch("\t", "\r\n ", "\r\n\t", "\r\n     \r\n    ");
            VisibleCharacters.ShouldMatch('\x21'.To('\x7e'));
            Octet.ShouldMatch('\x0'.To('\xff'));
            Space.ShouldMatch(" ");
        }
    }
}
