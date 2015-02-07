﻿using System;

namespace OpenRasta.Sina.Rules
{
    public class ConcatConvertRule<TLeft, TRight, TResult> : Rule<TResult>
    {
        readonly Func<TLeft, TRight, TResult> _converter;
        readonly IParser<TLeft> _left;
        readonly IParser<TRight> _right;

        public ConcatConvertRule(Rule<TLeft> left, Rule<TRight> right, Func<TLeft, TRight, TResult> converter)
        {
            _left = left;
            _right = right;
            _converter = converter;
        }

        public override Match<TResult> Match(StringInput input)
        {
            return MatchFromLeft(input, _left.Match);
        }

        Match<TResult> MatchFromLeft(StringInput input, Func<StringInput, Match<TLeft>> leftParser)
        {
            if (leftParser == null) return Match<TResult>.None;

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
                       ? ReturnCombined(input, leftMatch, rightMatch)
                       : Match<TResult>.None;
        }

        Match<TResult> MatchFromRight(StringInput input, Match<TLeft> leftMatch, Func<StringInput, Match<TRight>> rightParser)
        {
            var rightMatch = rightParser(input);
            return rightMatch.IsMatch
                       ? ReturnCombined(input, leftMatch, rightMatch)
                       : MatchFromLeft(input, leftMatch.Backtrack);
        }

        Match<TResult> ReturnCombined(StringInput input, Match<TLeft> leftMatch, Match<TRight> rightMatch)
        {
            return new Match<TResult>(
                _converter(leftMatch.Value, rightMatch.Value),
                leftMatch.Position,
                input.Position - leftMatch.Position,
                PrepareBacktrack(leftMatch, rightMatch));
        }

        Func<StringInput, Match<TResult>> PrepareBacktrack(Match<TLeft> leftMatch, Match<TRight> rightMatch)
        {
            if (rightMatch.Backtrack == null && leftMatch.Backtrack == null)
                return null;
            if (rightMatch.Backtrack == null)
                return input =>
                {
                    input.Position = leftMatch.Position;
                    return MatchFromLeft(input, leftMatch.Backtrack);
                };
            return input =>
            {
                input.Position = rightMatch.Position;
                return MatchFromRight(input, leftMatch, rightMatch.Backtrack);
            };
        }

        public override string ToString()
        {
            return _left + " " + _right;
        }
    }
}
