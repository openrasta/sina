using System;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public class ConditionRule<T> : Rule<T>
    {
        readonly Rule<T> _parser;
        readonly Func<T, bool> _selector;

        public ConditionRule(Rule<T> parser, Func<T,bool> selector)
        {
            _parser = parser;
            _selector = selector;
        }

        public override Match<T> Match(StringInput input)
        {
            var result = _parser.Match(input);
            return result.IsMatch && _selector(result.Value)
                       ? result
                       : Match<T>.None;
        }
    }
}