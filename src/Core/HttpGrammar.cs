using System;
using System.Diagnostics.CodeAnalysis;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
  /// <summary>
  /// A grammar implementing HTTP 7230
  /// </summary>
    public class HttpGrammar : Grammar
    {
        /// <summary>
        ///     OWS
        /// </summary>
        public static readonly Rule<string> OptionalWhiteSpace;

        public static readonly Rule<string> AbsolutePath;
        public static readonly CharAlternateRule Alpha = AbnfGrammar.Alpha;
        public static readonly Rule<string> AsteriskForm;
        public static readonly Rule<string> Attribute;
        public static readonly CharAlternateRule CTEXT;
        public static readonly CharacterRule CarriageReturn = AbnfGrammar.CarriageReturn;
        public static readonly Rule<string> Comment;
        public static readonly Rule<string> ConnectionOption;
        public static readonly Rule<long> ContentLength;
        public static readonly Rule<string> CrLf = AbnfGrammar.CrLf;
        public static readonly CharacterRangeRule Digit = AbnfGrammar.Digit;
        public static readonly CharacterRule DoubleQuote;
        public static readonly Rule<string> FieldContent;
        public static readonly Rule<string> FieldName;
        public static readonly Rule<string> FieldValue;
        public static readonly Rule<Tuple<string, string>> HeaderField;
        public static readonly CharAlternateRule HexDigit = AbnfGrammar.HexDigit;
        public static readonly CharacterRule HorizontalTab = AbnfGrammar.HorizontalTab;
        public static readonly StringRule HttpName;
        public static readonly Rule<Version> HttpVersion;
        public static readonly CharacterRule LineFeed = AbnfGrammar.LineFeed;
        public static readonly Rule<string> Method;
        public static readonly Rule<string> ObsoleteFold;
        public static readonly CharacterRangeRule ObsoleteText;
        public static readonly CharacterRangeRule Octet = AbnfGrammar.Octet;
        public static readonly Rule<string> Pseudonym;
        public static readonly CharAlternateRule QDTEXT;
        public static readonly CharAlternateRule QDTEXTNF;
        public static readonly Rule<string> QuotedCPpair;
        public static readonly Rule<string> QuotedPair;
        public static readonly Rule<string> QuotedString;
        public static readonly Rule<string> QuotedStringNF;
        public static readonly Rule<string> Rank;
        public static readonly Rule<string> ReasonPhrasae;
        public static readonly Rule<string> ReceivedBy;
        public static readonly Rule<string> ReceivedProtocol;
        public static readonly Rule<Tuple<string, string, Version>> RequestLine;
        public static readonly Rule<string> RequestTarget;

        /// <summary>
        ///     RWS
        /// </summary>
        public static readonly Rule<string> RequiredWhiteSpace;
        public static readonly CharacterRule Space = AbnfGrammar.Space;
        public static readonly CharAlternateRule Special;
        public static readonly Rule<string> StartLine;
        public static readonly Rule<string> StatusCode;
        public static readonly Rule<string> StatusLine;
        public static readonly Rule<object> TCodings;
        public static readonly Rule<string> Token;

        /// <summary>
        ///     TCHAR
        /// </summary>
        public static readonly CharAlternateRule TokenCharacter;
        public static readonly Rule<string> TransferCoding;
        public static readonly Rule<int> TransferExtension;
        public static readonly Rule<object> TransferParameter;
        public static readonly CharacterRule TransferRanking;
        public static readonly Rule<string> Value;
        public static readonly CharacterRangeRule VisibleCharacter = AbnfGrammar.VisibleCharacters;
        public static readonly Rule<string> Word;
        public static Rule<string> HttpMessage;
        public static CharacterRangeRule MessageBody;
        public static Rule<string> ReasonPhrase;


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
