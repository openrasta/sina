using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.characters
{
    public class matches_single_character : parsing_text_to<char>
    {
        public matches_single_character()
        {
            given_rule(Character('a'));
            when_matching("a");
        }

        [Fact]
        public void is_successful()
        {
            result.ShouldMatch('a',0,1);
        }

        [Fact]
        public void position_input_is_set()
        {
            input.Position.ShouldEqual(1);
        }
    }
}
