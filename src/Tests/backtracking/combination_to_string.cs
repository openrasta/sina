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
            given_rule(Grammar.Any().Min(1) +
                       Grammar.Character('a'));
            when_matching("aa");
        }

        [Fact]
        public void backtracks()
        {
            results.First().IsMatch.ShouldBeTrue();
            results.First().Value.ShouldEqual("aa");
        }
    }
}