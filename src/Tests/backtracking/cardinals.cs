using System.Collections.Generic;
using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.backtracking
{
    public class cardinals
        : contexts.parsing_text_to<IEnumerable<string>>
    {
        public cardinals()
        {
            given_rule(
                (Grammar.String("http") / Grammar.String("http://"))
                .Range(1,1)
                .End());
            when_matching("http://");
        }

        [Fact]
        public void successful()
        {
            result.IsMatch.ShouldBeTrue();
        }
    }
}