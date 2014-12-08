using System.Collections.Generic;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public static class HttpGrammarExtensions
    {
        public static Rule<IEnumerable<T>> List<T>(this Rule<T> rule, int min = 1, int max = -1)
        {
    
            var listRule = from comma in (HttpGrammar.OptionalWhiteSpace.ZeroOrMore() +
                                          Grammar.Character(',').Optional() +
                                          HttpGrammar.OptionalWhiteSpace.ZeroOrMore())
                           from entry in rule
                           select entry;

            return listRule.Repeat(min, max);
        }
    }
}