using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OpenRasta.Sina.Rules
{
    public class CardinalRule<T> : Rule<IEnumerable<T>>
    {
        readonly int _maximum;
        readonly int _minimum;
        readonly Rule<T> _rule;

        public CardinalRule(Rule<T> rule, int minimum = -1, int maximum = -1)
        {
            _rule = rule;
            _minimum = minimum;
            _maximum = maximum;
        }

        public override Match<IEnumerable<T>> Match(StringInput input)
        {
            return MatchWithMax(input, _maximum);
        }

        Match<IEnumerable<T>> MatchWithMax(StringInput input, int maxCapacity)
        {
            var results = maxCapacity > 0 ? new List<T>(maxCapacity) : new List<T>();

            var originalPosition = input.Position;
            var i = 0;
            Stack<Func<StringInput, Match<IEnumerable<T>>>> retries =
                new Stack<Func<StringInput, Match<IEnumerable<T>>>>();

            do
            {
                var match = _rule.Match(input);
                if (match.IsMatch == false)
                {
                    if (maxCapacity == -1 || i < maxCapacity) break;
                    return Fail(input, originalPosition);
                }

                i++;

                if (i >= _minimum)
                {
                    if (match.Backtrack != null)
                        retries.Push(BacktrackFromMatch(match, results, i - 1));
                }
                if (i > _minimum)
                    retries.Push(BacktrackValuesBeforeMatch(originalPosition, match, results, i - 1));
                results.Add(match.Value);
            }
            while (maxCapacity == -1 || i < maxCapacity);


            var failed = _minimum != -1 && i < _minimum;

            return failed
                       ? Fail(input, originalPosition)
                       : new Match<IEnumerable<T>>(results, originalPosition, input.Position - originalPosition)
                       {
                           Backtrack = retries.AsBacktrack()
                       };
        }

        Func<StringInput, Match<IEnumerable<T>>> BacktrackValuesBeforeMatch(
            int originalPosition, 
            Match<T> match,
            List<T> values,
            int length)
        {
            return input =>
            {
                input.Position = match.Position;
                return new Match<IEnumerable<T>>(
                    Realize(values, 0, length),
                    originalPosition,
                    match.Position - originalPosition);
            };
        }

        Func<StringInput, Match<IEnumerable<T>>> BacktrackFromMatch(Match<T> lastMatch, List<T> values, int length)
        {
            return input =>
            {
                input.Position = lastMatch.Position;
                var newMatch = lastMatch.Backtrack(input);
                if (!newMatch.IsMatch) return Match<IEnumerable<T>>.None;

                values[length] = newMatch.Value;
                var newValues = Realize(values, 0, length+1);
                return new Match<IEnumerable<T>>(newValues,
                                                 lastMatch.Position,
                                                 input.Position,
                                                 BacktrackFromMatch(newMatch, values,length));
            };
        }

        static T[] Realize(List<T> values, int index, int length)
        {
            var result = new T[length];
            values.CopyTo(index, result, 0, length);
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}*{1}({2})", ToString(_minimum), ToString(_maximum), _rule);
        }

        static Match<IEnumerable<T>> Fail(StringInput input, int originalPosition)
        {
            input.Position = originalPosition;
            return Match<IEnumerable<T>>.None;
        }

        string ToString(int cardinality)
        {
            return cardinality == -1 ? string.Empty : cardinality.ToString(CultureInfo.InvariantCulture);
        }
    }
}