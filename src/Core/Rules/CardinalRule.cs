using System;
using System.Collections.Generic;
using System.Globalization;

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

            do
            {
                var match = _rule.Match(input);
                if (match.IsMatch == false)
                {
                    if (maxCapacity == -1 || i < maxCapacity) break;
                    return Fail(input, originalPosition);
                }
                results.Add(match.Value);

                i++;
            }
            while (maxCapacity == -1 || i < maxCapacity);

            return (_minimum != -1 && i < _minimum)
                       ? Fail(input, originalPosition)
                       : new Match<IEnumerable<T>>(results)
                       {
                           Backtrack = PrepareBacktrackIfNeeded(i)
                       };
        }

        Func<StringInput, Match<IEnumerable<T>>> PrepareBacktrackIfNeeded(int i)
        {
            if (i == _minimum) return null;
            return _ => MatchWithMax(_, i - 1);
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