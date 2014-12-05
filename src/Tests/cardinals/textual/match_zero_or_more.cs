using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals.textual
{
    public class match_zero_or_more : parsing_text_to<string>
    {
        public match_zero_or_more()
        {
            given_rule(Grammar.Character('z').AtLeast(0));
            when_matching("a", "z", "zz");
        }

        [Fact]
        public void match_inside_range()
        {
            results[0].ShouldMatch(string.Empty);
            results[1].ShouldMatch("z");
            results[2].ShouldMatch("zz");
        }

        [Fact]
        public void positions_are_correct()
        {
            inputs[0].Position.ShouldEqual(0);
            inputs[1].Position.ShouldEqual(1);
            inputs[2].Position.ShouldEqual(2);
        }
    }
}
