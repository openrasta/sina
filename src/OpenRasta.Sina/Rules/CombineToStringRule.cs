using System;
using System.Text;

namespace OpenRasta.Sina.Rules
{
    public class CombineToStringRule<TLeft, TRight> : Rule<string>
    {
        readonly IParser<TLeft> _left;
        readonly IParser<TRight> _right;

        public CombineToStringRule(Rule<TLeft> left, Rule<TRight> right)
        {
            _left = left;
            _right = right;
        }


        public override Match<string> Match(StringInput input)
        {
            var matchLeft = _left.Match(input);
            if (!matchLeft.IsMatch) return Match<string>.None;
            var matchRight = _right.Match(input);
            if (!matchRight.IsMatch) return Match<string>.None;

            var sb = new StringBuilder(2);
            if (ReferenceEquals(matchLeft.Value, null) == false)
                sb.Append(matchLeft.Value);
            if (ReferenceEquals(matchRight.Value, null) == false)
                sb.Append(matchRight.Value);

            return new Match<string>(sb.ToString());
        }

        public override string ToString()
        {
            return _left + " " + _right;
        }
    }
}
