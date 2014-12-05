using System;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;

namespace Tests.contexts
{
    public abstract class parsing_text_to<T>
    {
        protected StringInput input;
        protected StringInput[] inputs;
        protected Match<T> result;
        protected Match<T>[] results;
        protected Rule<T> rule;

        protected void given_rule(Rule<T> rule)
        {
            this.rule = rule;
        }

        protected void when_matching(string input, params string[] additionalInputs)
        {
            inputs = new[] { input }.Concat(additionalInputs).Select(_ => new StringInput(_)).ToArray();
            this.input = inputs.First();
            result = rule.Match(this.input);
            results = new[] { result }.Concat(inputs.Skip(1).Select(_ => rule.Match(_))).ToArray();
        }
    }
}
