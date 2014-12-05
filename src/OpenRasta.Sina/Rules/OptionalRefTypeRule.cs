namespace OpenRasta.Sina.Rules
{
    public class OptionalRefTypeRule<T> : Rule<T> where T:class
    {
        readonly IParser<T> _rule;

        public OptionalRefTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T> Match(StringInput input)
        {
            var match = _rule.Match(input);
            return new Match<T>(match.IsMatch ? match.Value : default(T));
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}
