using System;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.range
{
    public class matches : contexts.parsing_text_to<char>
    {
        public matches()
        {
            given_rule(Range('a', 'z'));

            when_matching("a", "x", "z");
        }

        [Fact]
        public void input_position_is_correct()
        {
            inputs.Select(_ => _.Position).ShouldAllEqual(1);
        }

        [Fact]
        public void result_is_a_match()
        {
            results.Select(_ => _.IsMatch).ShouldAllEqual(true);
            results[0].ShouldMatch('a', 0, 1);
            results[1].ShouldMatch('x', 0, 1);
            results[2].ShouldMatch('z', 0, 1);
        }
    }
}
