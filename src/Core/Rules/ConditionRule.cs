using System;

namespace OpenRasta.Sina.Rules
{
    public class ConditionRule<T> : Rule<T>
    {
        readonly Rule<T> _parser;
        readonly Func<T, bool> _selector;

        public ConditionRule(Rule<T> parser, Func<T,bool> selector)
        {
            _parser = parser;
            _selector = selector;
        }

        public override Match<T> Match(StringInput input)
        {
            return Match(input, _parser.Match);
        }

        Match<T> Match(StringInput input, Func<StringInput, Match<T>> matcher)
        {
            var originalPosition = input.Position;
            do
            {
                input.Position = originalPosition;
                var result = matcher(input);
                if (!result.IsMatch) break;
                if (result.IsMatch && _selector(result.Value)) return result;
                matcher = result.Backtrack;
            }
            while (matcher != null);
            
            return Match<T>.None;
        }
    }
}