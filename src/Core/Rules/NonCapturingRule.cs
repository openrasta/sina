namespace OpenRasta.Sina.Rules
{
    public class NonCapturingRule<T> : Rule<T>
    {
        readonly Rule<T> _rule;

        public NonCapturingRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T> Match(StringInput input)
        {
            var originalPosition = input.Position;
            var match = _rule.Match(input);
            return match.IsMatch ? new Match<T>(
                default(T),
                originalPosition,
                input.Position - originalPosition)
            {
                Backtrack = match.Backtrack
            } : match;
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