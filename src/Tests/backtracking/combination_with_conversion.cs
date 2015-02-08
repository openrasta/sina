using System;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class combination_with_conversion : parsing_text_to<Tuple<string, string>>
    {
        public combination_with_conversion()
        {
            given_rule(from onePlus in Character('a').Min(1)
                       from one in Character('a').Min(1)
                       select Tuple.Create(onePlus,one));
            when_matching("aa");
        }
        
        [Fact]
        public void first_group_backtracked()
        {
            result.ShouldMatch(Tuple.Create("a", "a"), 0, 2);
        }

    }
}