using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public class Grammar
    {
        public static CharacterRangeRule Any()
        {
            return new CharacterRangeRule((char)0x00, (char)0xff);
        }
        public static CharacterRule Character(char character)
        {
            return new CharacterRule(character);
        }

        public static CharacterRangeRule Range(byte from, byte to)
        {
            return new CharacterRangeRule((char)@from, (char)to);
        }

        public static CharacterRangeRule Range(char from, char to)
        {
            return new CharacterRangeRule(@from, to);
        }

        public static StringRule String(string text)
        {
            return new StringRule(text);
        }

        public static Rule<char> Not(char c)
        {
            return new NotCharacterRule(c);
        }
    }

    public class NotCharacterRule : Rule<char>
    {
        readonly char _c;

        public NotCharacterRule(char c)
        {
            _c = c;
        }


        public override Match<char> Match(StringInput input)
        {
            if (input.Position >= input.Text.Length ||
                input.Current == _c)
                return Match<char>.None;

            var match = new Match<char>(input.Current);
            input.Position++;
            return match;
        }
    }
}
