using System;
using System.Linq;

namespace OpenRasta.Sina.Rules
{
    public class NotCharacterRule : Rule<char>
    {
        readonly char[] _c;
        readonly Func<char, bool> _isForbisdden;

        public NotCharacterRule(params char[] c)
        {
            _c = c;
            Func<char, bool> selector = _ => false;
            _isForbisdden = c.Aggregate(
                        selector,
                        (previous, expected) =>
                        input => previous(input) || input == expected
                );
        }


        protected override Match<char> MatchCore(StringInput input)
        {
            if (input.Position >= input.Text.Length ||
                _isForbisdden(input.Current))
                return Match<char>.None;

            var match = new Match<char>(input.Current, input.Position, 1);
            input.Position++;
            return match;
        }
    }
}