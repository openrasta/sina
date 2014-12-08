namespace OpenRasta.Sina.Rules
{
    public class OptionalValueTypeRule<T> : Rule<T?> where T:struct
    {
        readonly Rule<T> _rule;


        public OptionalValueTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T?> Match(StringInput input)
        {
            var match = _rule.Match(input);
            return new Match<T?>(match.IsMatch ? (T?)match.Value : null);
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}