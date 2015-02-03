namespace OpenRasta.Sina.Rules
{
    public class OptionalReferenceTypeRule<T> : Rule<T> where T:class
    {
        readonly IParser<T> _rule;

        public OptionalReferenceTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T> Match(StringInput input)
        {
            var originalPosition = input.Position;
            var match = _rule.Match(input);
            return new Match<T>(match.IsMatch ? match.Value : default(T),
                originalPosition, input.Position-originalPosition)
            {
                Backtrack = _ => Match<T>.Empty(originalPosition)
            };
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}
