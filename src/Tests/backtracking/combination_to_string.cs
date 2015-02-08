using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class combination_to_string : parsing_text_to<string>
    {
        public combination_to_string()
        {
            given_rule((AnyCharacter().Min(1)
                        + Character('a')).End());
            when_matching("aa", "aab");
        }

        [Fact]
        public void backtracks()
        {
            result.ShouldMatch("aa", 0, 2);
            input.Position.ShouldEqual(2);
        }

        [Fact]
        public void fails_restore_inputs()
        {
            inputs[1].Position.ShouldEqual(0);
        }
    }
}