using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class optional : parsing_text_to<string>
    {
        public optional()
        {
            given_rule((Grammar.Character('a').Optional() + Grammar.Character('a')).End());
            when_matching("a");
        }

        [Fact]
        public void backtracks()
        {
            results.First().IsMatch.ShouldBeTrue();
            results.First().Value.ShouldEqual("a");
        }
    }
}