using System;

namespace OpenRasta.Sina.Rules
{
    public abstract class Rule<T> : IParser<T>
    {
        public static Rule<string> operator +(Rule<T> left, Rule<char> right)
        {
            return new ConcatToStringRule<T, char>(left, right);
        }
        public static Rule<string> operator +(Rule<T> left, char right)
        {
            return new ConcatToStringRule<T, char>(left, new CharacterRule(right));
        }
        public static Rule<string> operator +(Rule<T> left, Rule<char?> right)
        {
            return new ConcatToStringRule<T, char?>(left, right);
        }

        public static Rule<string> operator +(Rule<T> left, Rule<string> right)
        {
            return new ConcatToStringRule<T, string>(left, right);
        }

        public static Rule<string> operator +(Rule<T> left, Rule<T> right)
        {
            return new ConcatToStringRule<T, T>(left, right);
        }


        public static AlternateRule<T> operator /(Rule<T> left, Rule<T> right)
        {
            return new AlternateRule<T>(new[] { left, right });
        }

        public static Rule<string> operator /(Rule<T> left, Rule<string> right)
        {
            return new AlternateRule<string>(left.Select(_ => _.ToString()), right);
        }


        public abstract Match<T> Match(StringInput input);

        public static NonCapturingRule<T> operator -(Rule<T> rule)
        {
            return new NonCapturingRule<T>(rule);
        }
    }

    public class NonCapturingRule<T> : Rule<T>
    {
        readonly Rule<T> _rule;

        public NonCapturingRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T> Match(StringInput input)
        {
            var match = _rule.Match(input);
            return match.IsMatch ? new Match<T>(default(T)) { Backtrack = match.Backtrack } : match;
        }

        public static Rule<T> operator +(NonCapturingRule<T> left, Rule<T> right)
        {
            return new ConcatConvertRule<T,T,T>(left, right, (l,r)=>r);
        }

        public static Rule<T> operator +(Rule<T> left, NonCapturingRule<T> right)
        {
            return new ConcatConvertRule<T, T, T>(left, right, (l, r) => l);
        }
    }
}
