using System.Collections.Generic;
using OpenRasta.Sina.Rules;

namespace OpenRasta.Sina
{
    public class RuleBuilder<T>
    {
        readonly Rule<T> _rule;

        public RuleBuilder(Rule<T> rule)
        {
            _rule = rule;
        }

        public Rule<IEnumerable<T>> Separator<TSeparator>(Rule<TSeparator> separator)
        {
            return from first in _rule
                   from followups in (from sep in separator
                                      from followup in _rule
                                      select followup).Range()
                   select NewList(first, followups);
        }

        static IEnumerable<T> NewList(T firstRule, IEnumerable<T> seconds)
        {
            var list = new List<T> { firstRule };
            list.AddRange(seconds);
            return list;
        }
    }
}