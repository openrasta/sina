using System;
using System.Collections.Generic;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public static class RulesExtensions
    {
        public static RuleBuilder<T> List<T>(this Rule<T> rule)
        {
            return new RuleBuilder<T>(rule);
        }
        public static Rule<IEnumerable<T>> List<T>(this Rule<T> rule, char separator)
        {
            return rule.List().Separator(Grammar.Character(separator));
        }


        public static Rule<string> Min(this Rule<char> rule, int minimum)
        {
            return new CadinalToStringRule<char>(rule, minimum);
        }

        public static Rule<string> Min(this Rule<string> rule, int minimum)
        {
            return new CadinalToStringRule<string>(rule, minimum);
        }

        public static Rule<IEnumerable<T>> Min<T>(this Rule<T> rule, int minimum)
        {
            return new CardinalRule<T>(rule, minimum);
        }

        public static Rule<string> Character<T>(this Rule<T> parser, char character)
        {
            return new ConcatToStringRule<T, char>(parser, new CharacterRule(character));
        }

        public static Rule<char?> Optional(this Rule<char> parser)
        {
            return new OptionalValueTypeRule<char>(parser);
        }
        public static Rule<T> Optional<T>(this Rule<T> parser) where T:class
        {
            return new OptionalReferenceTypeRule<T>(parser);
        }

        public static Rule<IEnumerable<T>> Repeat<T>(this Rule<T> rule, int minimum = 0, int maximum = -1)
        {
            return new CardinalRule<T>(rule, minimum, maximum);
        }

        public static Rule<string> Count(this Rule<char> parser, int count)
        {
            return new CadinalToStringRule<char>(parser, count, count);
        }

        public static Rule<string> Repeat(this Rule<char> parser, int minimum, int maximum)
        {
            return new CadinalToStringRule<char>(parser, minimum, maximum);
        }

        public static Rule<T> Where<T>(this Rule<T> parser, Func<T, bool> selector)
        {
            return new ConditionRule<T>(parser, selector);
        }

        public static Rule<TResult> Select<T, TResult>(this Rule<T> parser, Func<T, TResult> converter)
        {
            return new ReductionRule<T, TResult>(parser, converter);
        }

        public static Rule<TResult> SelectMany<T1, T2, TResult>(this Rule<T1> left,
                                                                Func<Rule<T1>, Rule<T2>> rightFactory,
                                                                Func<T1, T2, TResult> converter)
        {
            return new ConcatConvertRule<T1, T2, TResult>(left, rightFactory(left), converter);
        }

        public static Rule<string> String<T>(this Rule<T> parser, string input)
        {
            return new ConcatToStringRule<T, string>(parser, new StringRule(input));
        }

        public static Rule<string> Any(this Rule<string> rule)
        {
            return rule.Min(0);
        }
        public static Rule<string> Any(this Rule<char> rule)
        {
            return rule.Min(0);
        }
    }

    public class RuleBuilder<T>
    {
        readonly Rule<T> _rule;

        public RuleBuilder(Rule<T> rule)
        {
            _rule = rule;
        }

        public Rule<IEnumerable<T>> Separator<TSeparator>(Rule<TSeparator> separator)
        {
            return from first in _rule
                   from followups in (from sep in separator
                                      from followup in _rule
                                      select followup).Repeat()
                   select NewList(first, followups);
        }

        static IEnumerable<T> NewList(T firstRule, IEnumerable<T> seconds)
        {
            var list = new List<T> { firstRule };
            list.AddRange(seconds);
            return list;
        }
    }
    public class ConditionRule<T> : Rule<T>
    {
        readonly Rule<T> _parser;
        readonly Func<T, bool> _selector;

        public ConditionRule(Rule<T> parser, Func<T,bool> selector)
        {
            _parser = parser;
            _selector = selector;
        }

        public override Match<T> Match(StringInput input)
        {
            var result = _parser.Match(input);
            return result.IsMatch && _selector(result.Value)
                       ? result
                       : Match<T>.None;
        }
    }
}
