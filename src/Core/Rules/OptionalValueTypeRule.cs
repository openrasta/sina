using System;
using System.Collections.Generic;

namespace OpenRasta.Sina.Rules
{
    public class OptionalValueTypeRule<T> : Rule<T?> where T : struct
    {
        readonly Rule<T> _rule;


        public OptionalValueTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T?> Match(StringInput input)
        {
            var originalPosition = input.Position;
            var match = _rule.Match(input);
            return match.IsMatch
                ? new Match<T?>(match.Value, originalPosition, input.Position-originalPosition)
                {
                    Backtrack = _ => Match<T?>.Empty(originalPosition)
                }
                : Match<T?>.Empty(originalPosition);
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}