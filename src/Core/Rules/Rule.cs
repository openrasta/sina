using System;

namespace OpenRasta.Sina.Rules
{
    public abstract class Rule<T> : IParser<T>
    {
        public static Rule<string> operator +(Rule<T> left, Rule<char> right)
        {
            return new CombineToStringRule<T, char>(left, right);
        }
        public static Rule<string> operator +(Rule<T> left, char right)
        {
            return new CombineToStringRule<T, char>(left, new CharacterRule(right));
        }
        public static Rule<string> operator +(Rule<T> left, Rule<char?> right)
        {
            return new CombineToStringRule<T, char?>(left, right);
        }

        public static Rule<string> operator +(Rule<T> left, Rule<string> right)
        {
            return new CombineToStringRule<T, string>(left, right);
        }

        public static Rule<string> operator +(Rule<T> left, Rule<T> right)
        {
            return new CombineToStringRule<T, T>(left, right);
        }


        public static AlternateRule<T> operator /(Rule<T> left, Rule<T> right)
        {
            return new AlternateRule<T>(new[] { left, right });
        }

        public static Rule<string> operator /(Rule<T> left, Rule<string> right)
        {
            return new AlternateRule<string>(new[] { left.Select(_ => _.ToString()), right });
        }

        public abstract Match<T> Match(StringInput input);
    }

}
