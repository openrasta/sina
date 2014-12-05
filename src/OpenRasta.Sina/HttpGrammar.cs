using System;
using System.Diagnostics.CodeAnalysis;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public class HttpGrammar : Grammar
    {
        /// <summary>
        ///     OWS
        /// </summary>
        public static readonly Rule<string> OptionalWhiteSpace;

        static readonly Rule<string> AbsolutePath;
        static readonly CharAlternateRule Alpha = AbnfGrammar.Alpha;
        static readonly Rule<string> AsteriskForm;
        static readonly Rule<string> Attribute;
        static readonly CharAlternateRule CTEXT;
        static readonly CharacterRule CarriageReturn = AbnfGrammar.CarriageReturn;
        static readonly Rule<string> Comment;
        static readonly Rule<string> ConnectionOption;
        static readonly Rule<long> ContentLength;
        static readonly Rule<string> CrLf = AbnfGrammar.CrLf;
        static readonly CharacterRangeRule Digit = AbnfGrammar.Digit;
        static readonly CharacterRule DoubleQuote;

        static readonly Rule<string> FieldContent;
        static readonly Rule<string> FieldName;
        static readonly Rule<string> FieldValue;
        static readonly Rule<Tuple<string, string>> HeaderField;
        static readonly CharAlternateRule HexDigit = AbnfGrammar.HexDigit;
        static readonly CharacterRule HorizontalTab = AbnfGrammar.HorizontalTab;
        static readonly StringRule HttpName;
        static readonly Rule<Version> HttpVersion;
        static readonly CharacterRule LineFeed = AbnfGrammar.LineFeed;
        static readonly Rule<string> Method;
        static readonly Rule<string> ObsoleteFold;
        static readonly CharacterRangeRule ObsoleteText;
        static readonly CharacterRangeRule Octet = AbnfGrammar.Octet;
        static readonly Rule<string> Pseudonym;
        static readonly CharAlternateRule QDTEXT;
        static readonly CharAlternateRule QDTEXTNF;
        static readonly Rule<string> QuotedCPpair;
        static readonly Rule<string> QuotedPair;
        static readonly Rule<string> QuotedString;
        static readonly Rule<string> QuotedStringNF;
        static readonly Rule<string> Rank;
        static readonly Rule<string> ReasonPhrasae;
        static readonly Rule<string> ReceivedBy;
        static readonly Rule<string> ReceivedProtocol;
        static readonly Rule<Tuple<string, string, Version>> RequestLine;
        static readonly Rule<string> RequestTarget;

        /// <summary>
        ///     RWS
        /// </summary>
        static readonly Rule<string> RequiredWhiteSpace;

        static readonly CharacterRule Space = AbnfGrammar.Space;
        static readonly CharAlternateRule Special;

        static readonly Rule<string> StartLine;
        static readonly Rule<string> StatusCode;
        static readonly Rule<string> StatusLine;
        static readonly Rule<object> TCodings;
        static readonly Rule<string> Token;

        /// <summary>
        ///     TCHAR
        /// </summary>
        static readonly CharAlternateRule TokenCharacter;

        static readonly Rule<string> TransferCoding;
        static readonly Rule<int> TransferExtension;
        static readonly Rule<object> TransferParameter;
        static readonly CharacterRule TransferRanking;
        static readonly Rule<string> Value;
        static readonly CharacterRangeRule VisibleCharacter = AbnfGrammar.VisibleCharacters;
        static readonly Rule<string> Word;
        static Rule<string> HttpMessage;
        static CharacterRangeRule MessageBody;
        static Rule<string> ReasonPhrase;


        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:UseBuiltInTypeAlias", Justification = "StyleCopBug")]
        static HttpGrammar()
        {
            OptionalWhiteSpace = (Space / HorizontalTab).ZeroOrMore();
            RequiredWhiteSpace = (Space / HorizontalTab).AtLeast(1);
            DoubleQuote = ':';
            Special = DoubleQuote / '(' / ')' / '<' / '>' / '@' / ',' / ';' / ':' / '\\' / '/' / '[' / ']' / '?' / '=' /
                      '{' / '}';
            TokenCharacter = Alpha / Digit / '!' / '#' / '$' / '%' / '&' / '\'' / '*' / '+' / '-' / '.' /
                             '^' / '_' / '`' / '|' / '~';
            Token = TokenCharacter.AtLeast(1);
            Method = Token;
            Attribute = ConnectionOption = FieldName = Method = Pseudonym = Token;

            FieldContent = (HorizontalTab / Space / VisibleCharacter / ObsoleteText).ZeroOrMore();
            CTEXT = HorizontalTab / Space / Range(0x21, 0x27) /
                    Range(0x2A, 0x5B) /
                    Range(0x5D, 0x7E) /
                    ObsoleteText;
            QDTEXT = QDTEXTNF = HorizontalTab / Space / '!' / Range(0x23, 0x5B) /
                                Range(0x5D, 0x7E) /
                                ObsoleteText;
            QuotedCPpair = QuotedPair = Character('\'') + (HorizontalTab / Space / VisibleCharacter / ObsoleteText);
            QuotedString = QuotedStringNF = DoubleQuote + (QDTEXT / QuotedPair).ZeroOrMore() + DoubleQuote;
            Value = Word = Token / QuotedString;
            ContentLength = Digit.AtLeast(1).Select(long.Parse);
            HttpName = String("HTTP").CaseSensitive();
            MessageBody = Octet;


            HttpVersion = from prefix in HttpName + '/'
                          from major in Digit
                          from dot in Character('.')
                          from minor in Digit
                          select new Version(major, minor);

            RequestLine = from method in Method
                          from _ in Space
                          from requestTarget in RequestTarget
                          from __ in Space
                          from version in HttpVersion
                          from ___ in CrLf
                          select Tuple.Create(method, requestTarget, version);

            StartLine = RequestLine / StatusLine;
            StatusCode = Digit.Repeat(3, 3);
            ReasonPhrase = (HorizontalTab / Space / VisibleCharacter / ObsoleteText).ZeroOrMore();
            StatusLine = HttpVersion + Space + StatusCode + Space + ReasonPhrase + CrLf;
            HttpMessage = StartLine
                          + (HeaderField + CrLf).ZeroOrMore()
                          + CrLf
                          + MessageBody;
            ObsoleteFold = CrLf + (Space / HorizontalTab);

            FieldValue = FieldContent / ObsoleteFold;
            HeaderField = from name in FieldName
                          from nameSeparator in Character(':') + OptionalWhiteSpace
                          from value in FieldValue
                          from trailing in OptionalWhiteSpace
                          select Tuple.Create(name, value);
        }
    }
}
