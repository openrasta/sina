using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRasta.Sina.Rules
{
    public class AlternateRule<T> : Rule<T>
    {
        readonly Rule<T>[] _rules;

        public AlternateRule(params Rule<T>[] rules)
            : this((IEnumerable<Rule<T>>)rules)
        {
        }

        public AlternateRule(IEnumerable<Rule<T>> rules)
        {
            _rules = rules.SelectMany(UnfoldAlternates).ToArray();
            if (_rules.Length < 2)
                throw new ArgumentOutOfRangeException("rules", 
                                                      "To choose between alternates requires at least two choices. Duh!");
        }

        public static AlternateRule<T> operator /(AlternateRule<T> left, Rule<T> right)
        {
            return new AlternateRule<T>(new[] { left, right });
        }



        public override Match<T> Match(StringInput input)
        {
            var oldPosition = input.Position;

            // ReSharper disable once ForCanBeConvertedToForeach, known optimization
            for (var i = 0; i < _rules.Length; i++)
            {
                var match = _rules[i].Match(input);

                if (match.IsMatch) return match;
                input.Position = oldPosition;
            }
            return Match<T>.None;
        }

        public override string ToString()
        {
            return _rules.Aggregate(string.Empty, (previous, rule) => previous + " / " + rule);
        }

        static IEnumerable<Rule<T>> UnfoldAlternates(Rule<T> rule)
        {
            var asAlt = rule as AlternateRule<T>;
            if (asAlt == null) yield return rule;
            else
                foreach (var subRule in asAlt._rules.SelectMany(UnfoldAlternates))
                    yield return subRule;
        }
    }
}
