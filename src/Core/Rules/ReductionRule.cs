using System;
using System.Text;

namespace OpenRasta.Sina.Rules
{
    public class ReductionRule<T, TResult> : Rule<TResult>
    {
        readonly Func<T, TResult> _converter;
        readonly IParser<T> _rule;

        public ReductionRule(Rule<T> rule, Func<T, TResult> converter)
        {
            _rule = rule;
            _converter = converter;
        }

        protected override Match<TResult> MatchCore(StringInput input)
        {
            return Match(input.Position, input, _rule.Match);
        }

        Match<TResult> Match(int position, StringInput input, Func<StringInput, Match<T>> parser)
        {
            input.Position = position;
            var match = parser(input);
            return match.IsMatch
                       ? new Match<TResult>(
                             _converter(match.Value),
                             position,
                             input.Position - position,
                             PrepareBacktrack(position, match))
                       : Match<TResult>.None;
        }

        Func<StringInput, Match<TResult>> PrepareBacktrack(int position, Match<T> match)
        {
            if (match.Backtrack == null) return null;
            return _ => Match(position, _, match.Backtrack);
        }

        public override string ToString()
        {
            return _rule.ToString();
        }
    }
}
