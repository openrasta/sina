using System;

namespace OpenRasta.Sina.Rules
{
    public class CombineConvertRule<TLeft, TRight, TResult> : Rule<TResult>
    {
        readonly Func<TLeft, TRight, TResult> _converter;
        readonly IParser<TLeft> _left;
        readonly IParser<TRight> _right;

        public CombineConvertRule(Rule<TLeft> left, Rule<TRight> right, Func<TLeft, TRight, TResult> converter)
        {
            _left = left;
            _right = right;
            _converter = converter;
        }

        public override Match<TResult> Match(StringInput input)
        {
            Func<StringInput,Match<TLeft>> leftParser = _left.Match;
            Match<TRight> rightMatch;
            Match<TLeft> leftMatch;
            var originalPosition = input.Position;
            do
            {
                input.Position = originalPosition;
                leftMatch = leftParser(input);
                leftParser = leftMatch.Backtrack;

                if (!leftMatch.IsMatch) return Match<TResult>.None;

                rightMatch = _right.Match(input);
            }
            while (leftParser != null && rightMatch.IsMatch == false);

            return rightMatch.IsMatch 
                ? new Match<TResult>(_converter(leftMatch.Value, rightMatch.Value))
                : Match<TResult>.None;
        }

        public override string ToString()
        {
            return _left + " " + _right;
        }
    }
}
