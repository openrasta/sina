using System.Globalization;

namespace OpenRasta.Sina.Rules
{
    public class CharacterRule : Rule<char>
    {
        readonly char _character;

        public CharacterRule(char character)
        {
            _character = character;
        }
        public static CharAlternateRule operator /(CharacterRule left, CharacterRule right)
        {
            return new CharAlternateRule(left,right);
        }


        public static CharAlternateRule operator /(CharacterRule rule, char alternate)
        {
            return new CharAlternateRule(rule, new CharacterRule(alternate));
        }

        public static implicit operator CharacterRule(char character)
        {
            return new CharacterRule(character);
        }

        public override Match<char> Match(StringInput input)
        {
            if (input.Position >= input.Text.Length ||
                input.Current != _character)
                return Match<char>.None;

            var match = new Match<char>(input.Current);
            input.Position++;
            return match;
        }

        public override string ToString()
        {
            return _character.ToString(CultureInfo.InvariantCulture);
        }
    }
}
