using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public class Grammar
    {
        public static CharacterRangeRule AnyCharacter()
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

        public static Rule<char> Not(params char[] disallowed)
        {
            return new NotCharacterRule(disallowed);
        }
    }
}
