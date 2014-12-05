using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.@select
{
    public class non_matching
    {
        Match<bool> match;

        public non_matching()
        {
            match = Grammar.Character('a').Select(_ => true).Match("b");
        }

        [Fact]
        public void should_fail()
        {
            match.IsMatch.ShouldBeFalse();
        }
    }
}
