using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.list
{
    public class character_separated
        : contexts.parsing_text_to<IEnumerable<string>>
    {
        public character_separated()
        {
            given_rule(
                (Character('a') + 'b').List(' '));
            when_matching("ab", "ab ab ab");
        }

        [Fact]
        public void one_repetition()
        {
            results[0].IsMatch.ShouldBeTrue();
            results[0].Value.Single().ShouldEqual("ab");
            results[0].Position.ShouldEqual(0);
            results[0].Length.ShouldEqual(2);
            inputs[0].Position.ShouldEqual(2);
        }

        [Fact]
        public void multiple()
        {
            results[1].IsMatch.ShouldBeTrue();
            var vals = results[1].Value.ToArray();
            vals.ShouldAllEqual("ab");
            results[1].Position.ShouldEqual(0);
            results[1].Length.ShouldEqual(8);
            inputs[1].Position.ShouldEqual(8);
        }
    }
}