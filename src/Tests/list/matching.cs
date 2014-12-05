using System;
using System.Collections.Generic;
using OpenRasta.Sina;

namespace Tests.list
{
    public class matching : contexts.parsing_text_to<IEnumerable<string>>
    {
        public matching()
        {
            given_rule((Grammar.String("a") / Grammar.String("b")).List());

            when_matching("a,b");
        }

        [Xunit.FactAttribute]
        public void matches()
        {
            result.ShouldMatch(new[] { "a", "b" });
        }
    }
}
