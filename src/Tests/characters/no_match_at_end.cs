using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.characters
{
    public class no_match_at_end : parsing_text_to<char>
    {
        public no_match_at_end()
        {
            given_rule(Grammar.Character('a'));
            when_matching(string.Empty);
        }

        [Fact]
        public void no_match()
        {
            result.ShouldNotMatch();
        }

        [Fact]
        public void position_is_set()
        {
            input.Position.ShouldEqual(0);
        }
    }
}
