using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.combinators
{
    public class plus_no_match : contexts.parsing_text_to<string>
    {
        public plus_no_match()
        {
            given_rule(Character('a') + Character('b'));
            when_matching("ayo");
        }

        [Fact]
        public void input_back_to_original_position()
        {
            input.Position.ShouldEqual(0);

        }
    }
}