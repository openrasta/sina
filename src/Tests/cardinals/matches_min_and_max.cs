using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals
{
    public class matches_min_and_max : parsing_text_to<IEnumerable<int>>
    {
        public matches_min_and_max()
        {
            given_rule(Range('0', '9')
                              .Select(_ => int.Parse(_ + string.Empty))
                              .Range(1, 4));

            when_matching("0123");
        }

        [Fact]
        public void matched()
        {
            result.Value.ElementAt(0).ShouldEqual(0);
            result.Value.ElementAt(1).ShouldEqual(1);
            result.Value.ElementAt(2).ShouldEqual(2);
            result.Value.ElementAt(3).ShouldEqual(3);
        }

        [Fact]
        public void position_is_set()
        {
            input.Position.ShouldEqual(4);
        }
    }
}
