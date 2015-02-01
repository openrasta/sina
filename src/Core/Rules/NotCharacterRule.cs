namespace OpenRasta.Sina.Rules
{
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