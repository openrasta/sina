using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.conditional
{
    public class not_match_partial : contexts.parsing_text_to<string>
    {
        public not_match_partial()
        {
            given_rule(Grammar.Not(' ').Any());
            when_matching("abc ");
        }

        [Fact]
        public void matches_portion_not_in()
        {
            result.IsMatch.ShouldBeTrue();
            result.Value.ShouldEqual("abc");
        }
    }
}