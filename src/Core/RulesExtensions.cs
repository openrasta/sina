using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public static class RulesExtensions
    {
        public static ListRuleBuilder<T> List<T>(this Rule<T> rule)
        {
            return new ListRuleBuilder<T>(rule);
        }
        public static Rule<IEnumerable<T>> List<T>(this Rule<T> rule, char separator)
        {
            return rule.List().Separator(Grammar.Character(separator));
        }

        public static Rule<IEnumerable<T>> List<T>(this Rule<T> rule, string separator)
        {
            return rule.List().Separator(Grammar.String(separator));
        }


        public static Rule<string> Min(this Rule<char> rule, int minimum)
        {
            return rule.Range(minimum,-1);
        }

        //public static Rule<string> Min(this Rule<string> rule, int minimum)
        //{
        //    return new CardinalToStringRule<string>(rule, minimum);
        //}

        public static Rule<IEnumerable<T>> Min<T>(this Rule<T> rule, int minimum)
        {
            return new CardinalRule<T>(rule, minimum);
        }

        public static Rule<IEnumerable<T>> Max<T>(this Rule<T> rule, int maximum)
        {
            return new CardinalRule<T>(rule, 0, maximum);
        }

        public static Rule<char?> Optional(this Rule<char> parser)
        {
            return new OptionalValueTypeRule<char>(parser);
        }
        public static Rule<T> Optional<T>(this Rule<T> parser) where T:class
        {
            return new OptionalReferenceTypeRule<T>(parser);
        }

        public static Rule<IEnumerable<T>> Range<T>(this Rule<T> rule, int minimum, int maximum)
        {
            return new CardinalRule<T>(rule, minimum, maximum);
        }

        public static Rule<string> Count(this Rule<char> parser, int count)
        {
            return parser.Range(count, count);
        }

        public static Rule<string> Range(this Rule<char> parser, int minimum, int maximum)
        {
            return from c in new CardinalRule<char>(parser, minimum, maximum)
                   select c.Aggregate(new StringBuilder(),(builder, c1) => builder.Append(c1)).ToString();
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


        public static Rule<string> Any(this Rule<string> rule)
        {
            return from values in rule.Min(0)
                   select values.Aggregate(new StringBuilder(), (builder, s) => builder.Append(s))
                                .ToString();
        }
        public static Rule<string> Any(this Rule<char> rule)
        {
            return rule.Min(0);
        }

        public static Rule<T> End<T>(this Rule<T> rule)
        {
            return rule + -(new EndRule<T>());
        }

        class EndRule<T> : Rule<T>
        {

            public override Match<T> Match(StringInput input)
            {
                return (input.Position >= input.Text.Length)
                           ? Match<T>.Empty(input.Position)
                           : Match<T>.None;
            }
            public override string ToString()
            {
                return "$";
            }
        }
    }
}
