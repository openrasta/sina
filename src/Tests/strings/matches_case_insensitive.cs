using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.strings
{
    public class matches_case_insensitive : contexts.parsing_text_to<string>
    {
        public matches_case_insensitive()
        {
            given_rule(String("test"));
            when_matching("TEST");
        }

        [Fact]
        public void position_is_correct()
        {
            input.Position.ShouldEqual(4);
        }

        [Fact]
        public void successful()
        {
            result.ShouldMatch("TEST");
        }
    }
}
