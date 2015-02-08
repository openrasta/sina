using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class condition : parsing_text_to<string>
    {
        public condition()
        {
            given_rule((from val in String("seb") / String("sebastien")
                        where val == "sebastien"
                        select val));
            when_matching("sebastien");
        }

        [Fact]
        public void backtracks()
        {
            result.IsMatch.ShouldBeTrue();
            result.Value.ShouldEqual("sebastien");
        }
    }
}