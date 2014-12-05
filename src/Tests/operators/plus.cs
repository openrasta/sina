using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.operators
{
    public class plus : parsing_text_to<string>
    {
        public plus()
        {
            given_rule(Grammar.Character('a') + Grammar.Character('b'));
            when_matching("ab");
        }

        [Fact]
        public void selects_combined_rules()
        {
            result.Value.ShouldEqual("ab");
        }
    }
}
