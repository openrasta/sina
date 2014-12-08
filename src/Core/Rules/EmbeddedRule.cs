using System;

namespace OpenRasta.Sina.Rules
{
    public class EmbeddedRule<TLeft, TRight, TResult> : Rule<TResult>
    {
        readonly Func<TLeft, TRight, TResult> _converter;
        readonly IParser<TLeft> _left;
        readonly IParser<TRight> _right;

        public EmbeddedRule(Rule<TLeft> left, Rule<TRight> right, Func<TLeft, TRight, TResult> converter)
        {
            _left = left;
            _right = right;
            _converter = converter;
        }

        public override Match<TResult> Match(StringInput input)
        {
            var matchLeft = _left.Match(input);
            if (!matchLeft.IsMatch) return Match<TResult>.None;
            var matchRight = _right.Match(input);
            if (!matchRight.IsMatch) return Match<TResult>.None;
            return new Match<TResult>(
                _converter(matchLeft.Value, matchRight.Value)
                );
        }

        public override string ToString()
        {
            return _left + " " + _right;
        }
    }
}
