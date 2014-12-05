using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.select
{
    public class matching
    {
        Match<bool> match;

        public matching()
        {
            match = Grammar.Character('a').Select(_ => true).Match("a");
        }

        [Fact]
        public void is_successful()
        {
            match.IsMatch.ShouldBeTrue();
        }

        [Fact]
        public void value_is_correct()
        {
            match.Value.ShouldBeTrue();
        }
    }
}
