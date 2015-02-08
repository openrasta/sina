using OpenRasta.Sina;
using Tests.contexts;
using Xunit;

namespace Tests.backtracking
{
    public class reduction : parsing_text_to<string>
    {
        public reduction()
        {
            given_rule((String("a") / String("aa"))
                           .Select(_ => "match").End());
            when_matching("aa");
        }

        [Fact]
        public void backtracks()
        {
            result.ShouldMatch("match");
        }
    }
}