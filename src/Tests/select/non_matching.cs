using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.@select
{
    public class non_matching : contexts.parsing_text_to<bool>
    {
        public non_matching()
        {
            given_rule(Grammar.Character('a').Select(_ => true));
            when_matching("b");
        }

        [Fact]
        public void should_fail()
        {
            result.ShouldNotMatch();
            input.Position.ShouldEqual(0);
        }
    }
}
