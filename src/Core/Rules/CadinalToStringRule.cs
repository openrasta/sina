using System;
using System.Globalization;
using System.Text;

namespace OpenRasta.Sina.Rules
{
    public class CadinalToStringRule<T> : Rule<string>
    {
        readonly int _maximum;
        readonly int _minimum;
        readonly Rule<T> _rule;


        public CadinalToStringRule(Rule<T> rule, int minimum = -1, int maximum = -1)
        {
            _rule = rule;
            _minimum = minimum;
            _maximum = maximum;
        }

        public override Match<string> Match(StringInput input)
        {
            var maximum = _maximum;
            return MatchWithMax(input, maximum);
        }

        Match<string> MatchWithMax(StringInput input, int maximum)
        {
            var originalPosition = input.Position;
            var builder = new StringBuilder();
            var i = 0;

            do
            {
                var match = _rule.Match(input);
                if (match.IsMatch == false)
                {
                    if (maximum == -1 || i < maximum) break;
                    return Fail(input, originalPosition);
                }

                i++;

                var resultingValue = match.Value.ToString();

                if (resultingValue.Length == 0) break;
                builder.Append(resultingValue);
            }
            while (maximum == -1 || i < maximum);

            return (_minimum != -1 && i < _minimum)
                       ? Fail(input, originalPosition)
                       : new Match<string>(builder.ToString(), originalPosition, input.Position)
                       {
                           Backtrack = PrepareBacktrackIfPossible(i)
                       };
        }

        Func<StringInput, Match<string>> PrepareBacktrackIfPossible(int i)
        {
            if (i == _minimum) return null;
            return _ => MatchWithMax(_, i - 1);
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
