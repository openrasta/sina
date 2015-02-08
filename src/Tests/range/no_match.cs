using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.range
{
    class no_match : contexts.parsing_text_to<char>
    {
        public no_match()
        {
            given_rule(Range('a', 'z'));
            when_matching("A");
        }

        [Fact]
        public void fails()
        {
            result.ShouldNotMatch();
            input.Position.ShouldEqual(0);
        }
    }
}
