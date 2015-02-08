using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals
{
    public class min : parsing_text_to<IEnumerable<int>>
    {
        public min()
        {
            given_rule(Range('0', '9').Select(_ => (int)_).Min(2));
            when_matching("1", "12", "123");
        }

        [Fact]
        public void success()
        {
            results[1].Value.ElementAt(0).ShouldEqual('1');
            results[1].Value.ElementAt(1).ShouldEqual('2');
            results[2].Value.ElementAt(0).ShouldEqual('1');
            results[2].Value.ElementAt(1).ShouldEqual('2');
            results[2].Value.ElementAt(2).ShouldEqual('3');
        }

        [Fact]
        public void fail()
        {
            inputs[0].Position.ShouldEqual(0);
        }
    }
}
