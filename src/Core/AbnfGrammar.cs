using System;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{

  // A gramar implementing ABNF rules as per RFC 5234
  public class AbnfGrammar : Grammar
  {
    /// <summary>
    ///   ALPHA: Alphabetic characters (A-Z / a-z).
    /// </summary>
    public static readonly CharAlternateRule Alpha;

    /// <summary>
    ///   BIT: Bit of information ("0" / "1").
    /// </summary>
    public static readonly CharAlternateRule Bit;

    /// <summary>
    ///   CR: Carriage return (0x0D).
    /// </summary>
    public static readonly CharacterRule CarriageReturn;

    /// <summary>
    ///   CHAR: Any 7-bit character, excepting NUL.
    /// </summary>
    public static readonly CharacterRangeRule Char;

    /// <summary>
    ///   CTL: Control characters (%x00-1F / %x7F)
    /// </summary>
    public static readonly CharAlternateRule Control;

    /// <summary>
    ///   CRLF: Internet standard newline (%x0D %x0A)
    /// </summary>
    public static readonly Rule<string> CrLf;

    /// <summary>
    ///   DIGIT: Digits (0-9).
    /// </summary>
    public static readonly CharacterRangeRule Digit;

    /// <summary>
    ///   DQUOTE: Double quote (%x22).
    /// </summary>
    public static readonly CharacterRule DoubleQuote;

    /// <summary>
    ///   HEXDIG: Hexaadecimal digit (DIGIT / "A" / "B" / "C" / "D" / "E" / "F")
    /// </summary>
    public static readonly CharAlternateRule HexDigit;

    /// <summary>
    ///   HTAB: Horizontal tab (%x09).
    /// </summary>
    public static readonly CharacterRule HorizontalTab;

    /// <summary>
    ///   LF: Line feed (%x0A).
    /// </summary>
    public static readonly CharacterRule LineFeed;

    /// <summary>
    ///   LWSP: Linear whitepace (*(WSP / CRLF WSP)).
    /// </summary>
    public static readonly Rule<string> LinearWhiteSpace;

    /// <summary>
    ///   OCTET: 8 bits of data (%x00-FF).
    /// </summary>
    public static readonly CharacterRangeRule Octet;

    /// <summary>
    ///   SP: Space (%x20).
    /// </summary>
    public static readonly CharacterRule Space;

    /// <summary>
    ///   VCHAR: Visible printing characters (%x21-7E).
    /// </summary>
    public static readonly CharacterRangeRule VisibleCharacters;

    /// <summary>
    ///   WSP: White space (SP / HTAB).
    /// </summary>
    public static readonly CharAlternateRule WhiteSpace;

    public static readonly CharacterRangeRule UppercaseAlpha;
    public static readonly CharacterRangeRule LowercaseAlpha;

    static AbnfGrammar()
    {
      LowercaseAlpha = Range('a', 'z');
      UppercaseAlpha = Range('A', 'Z');
      Alpha = LowercaseAlpha / UppercaseAlpha;
      Bit = Character('0') / Character('1');
      Char = Range(0x01, 0x7f);
      CarriageReturn = '\r';
      LineFeed = '\n';
      CrLf = CarriageReturn + LineFeed;
      Control = Range(0x0, 0x1f) / Character('\x7f');
      Digit = Range('0', '9');
      DoubleQuote = '"';
      HexDigit = Digit / Range('A', 'F');
      HorizontalTab = Character('\x09');
      Octet = Range(0x00, 0xff);
      Space = ' ';
      WhiteSpace = Space / HorizontalTab;
      VisibleCharacters = Range(0x21, 0x7e);
      LinearWhiteSpace = (WhiteSpace / (CrLf + WhiteSpace)).Any();
    }

    public static Rule<T> Alternates<T>(params Rule<T>[] alternates)
    {
      return new AlternateRule<T>(alternates);
    }
  }
}
