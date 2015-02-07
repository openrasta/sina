using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.range
{
    public class not_multiple : contexts.parsing_text_to<char>
    {
        public not_multiple()
        {
            given_rule(Grammar.Not('a','b'));
            when_matching("a", "b", "c");
        }

        [Fact]
        public void excluded_not_matched()
        {
            results[0].IsMatch.ShouldBeFalse();
            inputs[0].Position.ShouldEqual(0);

            results[1].IsMatch.ShouldBeFalse();
            inputs[1].Position.ShouldEqual(0);
        }

        [Fact]
        public void not_excluded_matched()
        {
            results[2].ShouldMatch('c', 0, 1);
            inputs[2].Position.ShouldEqual(1);

        }
    }
}