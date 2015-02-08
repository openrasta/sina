using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.conditional
{
    public class not_match : contexts.parsing_text_to<string>
    {
        public not_match()
        {
            given_rule(from any in AnyCharacter().Any().Select(_ => false)
                       where any
                       select "yay");
            when_matching("any old stuff");
        }

        [Fact]
        public void fails()
        {
            result.IsMatch.ShouldBeFalse();
            input.Position.ShouldEqual(0);
        }
    }
}