using System.Collections.Generic;

namespace OpenRasta.Sina.Rules
{
    public class CharAlternateRule : AlternateRule<char>
    {
        public CharAlternateRule(params Rule<char>[] rules) : base(rules)
        {
        }

        public CharAlternateRule(IEnumerable<Rule<char>> rules) : base(rules)
        {
        }

        public static CharAlternateRule operator /(CharAlternateRule left, char right)
        {
            return new CharAlternateRule(left, new CharacterRule(right));
        }
        public static CharAlternateRule operator /(CharAlternateRule left, CharacterRangeRule right)
        {
            return new CharAlternateRule(left, right);
        }
        public static CharAlternateRule operator /(CharAlternateRule left, CharAlternateRule right)
        {
            return new CharAlternateRule(left, right);
        }
    }
}