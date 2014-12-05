using System;
using System.Collections.Generic;

namespace OpenRasta.Sina.Rules
{
    public static class RulesExtensions
    {
        public static Rule<string> AtLeast(this Rule<char> rule, int minimum)
        {
            return new RepeatToStringRule<char>(rule, minimum);
        }

        public static Rule<string> AtLeast(this Rule<string> rule, int minimum)
        {
            return new RepeatToStringRule<string>(rule, minimum);
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
            return new OptionalRefTypeRule<T>(parser);
        }

        public static Rule<IEnumerable<T>> Repeat<T>(this Rule<T> rule, int minimum = 0, int maximum = -1)
        {
            return new RepeatRule<T>(rule, minimum, maximum);
        }

        public static Rule<string> Repeat(this Rule<char> parser, int count)
        {
            return new RepeatToStringRule<char>(parser, count, count);
        }

        public static Rule<string> Repeat(this Rule<char> parser, int minimum, int maximum)
        {
            return new RepeatToStringRule<char>(parser, minimum, maximum);
        }


        public static Rule<TResult> Select<T, TResult>(this Rule<T> parser, Func<T, TResult> converter)
        {
            return new ReductionRule<T, TResult>(parser, converter);
        }

        public static Rule<TResult> SelectMany<T1, T2, TResult>(this Rule<T1> left,
                                                                Func<Rule<T1>, Rule<T2>> rightFactory,
                                                                Func<T1, T2, TResult> converter)
        {
            return new EmbeddedRule<T1, T2, TResult>(left, rightFactory(left), converter);
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
}
