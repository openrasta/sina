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
            given_rule((Grammar.AnyCharacter().Min(1)
                        + Grammar.Character('a')).End());
            when_matching("aa");
        }

        [Fact]
        public void backtracks()
        {
            result.ShouldMatch("aa", 0, 2);
            input.Position.ShouldEqual(2);
        }
    }
}