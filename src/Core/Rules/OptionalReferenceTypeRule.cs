using System;
using System.Collections.Generic;

namespace OpenRasta.Sina.Rules
{
    public class OptionalReferenceTypeRule<T> : Rule<T> where T : class
    {
        readonly IParser<T> _rule;

        public OptionalReferenceTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T> Match(StringInput input)
        {
            var originalPosition = input.Position;
            var match = _rule.Match(input);

            var stack = new Stack<Func<StringInput, Match<T>>>();
            stack.Push(_ => Match<T>.Empty(originalPosition));

            if (match.Backtrack != null)
                stack.Push(match.Backtrack);
            return new Match<T>(match.IsMatch ? match.Value : default(T),
                originalPosition, input.Position - originalPosition)
            {
                Backtrack = stack.AsBacktrack()
            };
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}
