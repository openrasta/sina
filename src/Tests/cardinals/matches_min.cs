using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals
{
    public class matches_min : parsing_text_to<IEnumerable<int>>
    {
        public matches_min()
        {
            given_rule(Range('0', '9').Select(_ => (int)_).Min(1));
            when_matching("1", "12");
        }

        [Fact]
        public void success()
        {
            results[0].Value.ElementAt(0).ShouldEqual('1');
            results[1].Value.ElementAt(0).ShouldEqual('1');
            results[1].Value.ElementAt(1).ShouldEqual('2');
        }
    }
}
