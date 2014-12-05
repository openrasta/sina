using System;
using System.Globalization;
using System.Text;

namespace OpenRasta.Sina.Rules
{
    public class RepeatToStringRule<T> : Rule<string>
    {
        readonly int _maximum;
        readonly int _minimum;
        readonly Rule<T> _rule;


        public RepeatToStringRule(Rule<T> rule, int minimum = -1, int maximum = -1)
        {
            _rule = rule;
            _minimum = minimum;
            _maximum = maximum;
        }

        public override Match<string> Match(StringInput input)
        {
            var originalPosition = input.Position;
            var builder = new StringBuilder();
            var i = 0;

            do
            {
                var match = _rule.Match(input);
                if (match.IsMatch == false)
                {
                    if (_maximum == -1 || i < _maximum) break;
                    return Fail(input, originalPosition);
                }

                i++;

                var resultingValue = match.Value.ToString();

                if (resultingValue.Length == 0) break;
                builder.Append(resultingValue);
            }
            while (_maximum == -1 || i < _maximum);

            return (_minimum != -1 && i < _minimum)
                       ? Fail(input, originalPosition)
                       : new Match<string>(builder.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0}*{1}({2})", ToString(_minimum), ToString(_maximum), _rule);
        }

        static Match<string> Fail(StringInput input, int originalPosition)
        {
            input.Position = originalPosition;
            return Match<string>.None;
        }

        string ToString(int cardinality)
        {
            return cardinality == -1 ? string.Empty : cardinality.ToString(CultureInfo.InvariantCulture);
        }
    }
}
