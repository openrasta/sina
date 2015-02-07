using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
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
                .Range(1, 1)
                .End());
            when_matching("http://");
        }

        [Fact]
        public void successful()
        {
            result.Value.Single().ShouldEqual("http://");
            result.ShouldMatch(0, 7, "http://");
            input.Position.ShouldEqual(7);
        }
    }
}