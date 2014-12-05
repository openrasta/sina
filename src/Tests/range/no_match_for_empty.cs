using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.range
{
    public class no_match_for_empty : contexts.parsing_text_to<char>
    {
        public no_match_for_empty()
        {
            given_rule(Grammar.Range('a', 'z'));
            when_matching(string.Empty);
        }

        [Fact]
        public void fails()
        {
            result.ShouldNotMatch();
            input.Position.ShouldEqual(0);
        }
    }
}
