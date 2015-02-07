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
            return new AlternateRule<T>(left, right);
        }



        public override Match<T> Match(StringInput input)
        {
            return Match(input, 0, input.Position);
        }

        Match<T> Match(StringInput input, int firstItemToTry, int inputPosition)
        {
            for (var i = firstItemToTry; i < _rules.Length; i++)
            {
                input.Position = inputPosition;
                var match = _rules[i].Match(input);

                if (match.IsMatch)
                    return new Match<T>(match.Value, inputPosition, match.Length)
                    {
                        Backtrack = PrepareBacktrack(i, match)
                    };
            }
            return Match<T>.None;
        }

        Func<StringInput, Match<T>> PrepareBacktrack(int currentItemPosition, Match<T> currentItemMatch)
        {
            if (currentItemMatch.Backtrack == null)
                return _ => Match(_, currentItemPosition + 1, currentItemMatch.Position);
            return input =>
            {
                input.Position = currentItemMatch.Position;
                var newMatch = currentItemMatch.Backtrack(input);
                return newMatch.IsMatch
                           ? new Match<T>(newMatch.Value, newMatch.Position, newMatch.Length, PrepareBacktrack(currentItemPosition, newMatch))
                           : Match(input, currentItemPosition + 1, input.Position);
            };
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
