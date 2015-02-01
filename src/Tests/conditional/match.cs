using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.conditional
{
    public class match : contexts.parsing_text_to<string>
    {
        public match()
        {
            given_rule(from any in Grammar.Any().Select(_=>true)
                       where any
                       select "yay");
            when_matching("any old stuff");
        }

        [Fact]
        public void success()
        {
            result.IsMatch.ShouldBeTrue();
            result.Value.ShouldEqual("yay");
        }
    }
}