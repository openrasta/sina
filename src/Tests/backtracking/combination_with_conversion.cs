using System;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;

namespace Tests.backtracking
{
    public class combination_with_conversion : parsing_text_to<Tuple<string, string>>
    {
        public combination_with_conversion()
        {
            given_rule(from onePlus in Grammar.Character('a').Min(1)
                       from one in Grammar.Character('a').Min(1)
                       select Tuple.Create(onePlus,one));
            when_matching("aa");
        }

        public void first_group_backtracked()
        {
            results.First().IsMatch.ShouldBeTrue();
            results.First().Value.ShouldEqual(Tuple.Create("a", "a"));
        }
    }
}