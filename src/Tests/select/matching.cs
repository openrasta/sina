using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.select
{
    public class matching  : contexts.parsing_text_to<bool>
    {
        public matching()
        {
            given_rule(Grammar.Character('a').Select(_ => true));
            when_matching("a");
        }

        [Fact]
        public void is_successful()
        {
            result.ShouldMatch(true, 0, 1);
        }

        [Fact]
        public void positioning_correct()
        {
            input.Position.ShouldEqual(1);
        }
    }
}
