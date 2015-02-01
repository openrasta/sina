using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.range
{
    public class not : contexts.parsing_text_to<char>
    {
        public not()
        {
            given_rule(!Grammar.Character('a'));
            when_matching("a","b");
        }

        [Fact]
        public void excluded_not_matched()
        {
            results[0].IsMatch.ShouldBeFalse();
        }

        [Fact]
        public void not_excluded_matched()
        {
            results[1].IsMatch.ShouldBeTrue();
        }
    }
}