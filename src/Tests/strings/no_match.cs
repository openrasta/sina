using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.strings
{
    public class no_match : contexts.parsing_text_to<string>
    {
        public no_match()
        {
            given_rule(Grammar.String("test"));
            when_matching("something else");
        }

        [Fact]
        public void fails()
        {
            result.ShouldNotMatch();
            input.Position.ShouldEqual(0);
        }
    }
}
