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

        public static NotCharacterRule operator !(CharacterRule rule)
        {
            return new NotCharacterRule(rule._character);
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

        protected override Match<char> MatchCore(StringInput input)
        {
            if (input.Position >= input.Text.Length ||
                input.Current != _character)
                return Match<char>.None;

            var match = new Match<char>(input.Current, input.Position, 1);
            input.Position++;
            return match;
        }

        public override string ToString()
        {
            return _character.ToString(CultureInfo.InvariantCulture);
        }
    }
}
