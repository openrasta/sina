namespace OpenRasta.Sina.Rules
{
    public class OptionalValueTypeRule<T> : Rule<T?> where T : struct
    {
        readonly Rule<T> _rule;


        public OptionalValueTypeRule(Rule<T> rule)
        {
            _rule = rule;
        }

        public override Match<T?> Match(StringInput input)
        {
            var match = _rule.Match(input);
            return match.IsMatch
                ? new Match<T?>(match.Value)
                {
                    Backtrack = _ => Match<T?>.Empty
                }
                : Match<T?>.Empty;
        }

        public override string ToString()
        {
            return string.Format("*({0})", _rule);
        }
    }
}