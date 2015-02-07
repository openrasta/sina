using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class optional_group : parsing_text_to<string>
    {
        public optional_group()
        {
            given_rule(
                       ((Grammar.String("ab") / Grammar.String("a"))
                            .Optional() + Grammar.String("b")).End());
            when_matching("ab");
        }

        [Fact]
        public void backtracks()
        {
            results.First().IsMatch.ShouldBeTrue();
            results.First().Value.ShouldEqual("ab");
        }
    }
}