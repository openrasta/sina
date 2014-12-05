using System;

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

        public override Match<TResult> Match(StringInput input)
        {
            var match = _rule.Match(input);
            return match.IsMatch
                       ? new Match<TResult>(_converter(match.Value))
                       : Match<TResult>.None;
        }

        public override string ToString()
        {
            return _rule.ToString();
        }
    }
}
