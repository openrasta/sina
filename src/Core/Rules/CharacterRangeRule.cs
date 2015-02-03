namespace OpenRasta.Sina.Rules
{
    public class CharacterRangeRule : Rule<char>
    {
        readonly char _from;
        readonly char _to;

        public CharacterRangeRule(char @from, char to)
        {
            _from = @from;
            _to = to;
        }

        public static CharAlternateRule operator /(CharacterRangeRule left, CharacterRangeRule right)
        {
            return new CharAlternateRule(left, right);
        }

        public static CharAlternateRule operator /(CharacterRangeRule left, CharacterRule right)
        {
            return new CharAlternateRule(left, right);
        }

        public static CharAlternateRule operator /(CharacterRangeRule range, char alternate)
        {
            return new CharAlternateRule(range, new CharacterRule(alternate));
        }

        public override Match<char> Match(StringInput input)
        {
            return (input.Position < input.Text.Length && input.Current <= _to && input.Current >= _from)
                       ? MovePositionAndReturn(input)
                       : Match<char>.None;
        }

        public override string ToString()
        {
            var charForm = char.IsLetterOrDigit(_from) && char.IsLetterOrDigit(_to);
            return string.Format(charForm ? "{0}-{1}" : "0x{0:X}-{1:X}", _from, _to);
        }

        static Match<char> MovePositionAndReturn(StringInput input)
        {
            var result = new Match<char>(input.Current, input.Position, 1);
            input.Position++;
            return result;
        }
    }
}
