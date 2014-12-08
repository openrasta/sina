using System.Collections.Generic;
using System.Globalization;

namespace OpenRasta.Sina.Rules
{
    public class RepeatRule<T> : Rule<IEnumerable<T>>
    {
        readonly int _maximum;
        readonly int _minimum;
        readonly Rule<T> _rule;

        public RepeatRule(Rule<T> rule, int minimum = -1, int maximum = -1)
        {
            _rule = rule;
            _minimum = minimum;
            _maximum = maximum;
        }

        public override Match<IEnumerable<T>> Match(StringInput input)
        {
            var results = _maximum > 0 ? new List<T>(_maximum) : new List<T>();

            var originalPosition = input.Position;
            var i = 0;

            do
            {
                var match = _rule.Match(input);
                if (match.IsMatch == false)
                {
                    if (_maximum == -1 || i < _maximum) break;
                    return Fail(input, originalPosition);
                }
                results.Add(match.Value);

                i++;
            }
            while (_maximum == -1 || i < _maximum);

            return (_minimum != -1 && i < _minimum)
                       ? Fail(input, originalPosition)
                       : new Match<IEnumerable<T>>(results);
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