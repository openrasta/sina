using System;
using System.Collections.Generic;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public static class RulesExtensions
    {
        public static Rule<IEnumerable<T>> List<T>(this Rule<T> rule, char separator)
        {
            return from firstRule in rule
                   from seconds in (from sep in Grammar.Character(separator)
                                    from second in rule
                                    select second).Repeat()
                   select NewList(firstRule, seconds);
        }

        static IEnumerable<T> NewList<T>(T firstRule, IEnumerable<T> seconds)
        {
            var list = new List<T> { firstRule };
            list.AddRange(seconds);
            return list;
        }

        public static Rule<string> AtLeast(this Rule<char> rule, int minimum)
        {
            return new CadinalToStringRule<char>(rule, minimum);
        }

        public static Rule<string> AtLeast(this Rule<string> rule, int minimum)
        {
            return new CadinalToStringRule<string>(rule, minimum);
        }

        public static Rule<IEnumerable<T>> AtLeast<T>(this Rule<T> rule, int minimum)
        {
            return new CardinalRule<T>(rule, minimum);
        }

        public static Rule<string> Character<T>(this Rule<T> parser, char character)
        {
            return new CombineToStringRule<T, char>(parser, new CharacterRule(character));
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

        public static Rule<string> RepeatExactly(this Rule<char> parser, int count)
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
            return new CombineConvertRule<T1, T2, TResult>(left, rightFactory(left), converter);
        }

        public static Rule<string> String<T>(this Rule<T> parser, string input)
        {
            return new CombineToStringRule<T, string>(parser, new StringRule(input));
        }

        public static Rule<string> ZeroOrMore(this Rule<string> rule)
        {
            return rule.AtLeast(0);
        }
        public static Rule<string> ZeroOrMore(this Rule<char> rule)
        {
            return rule.AtLeast(0);
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
