using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.joined
{
    public class character_separated
        : contexts.parsing_text_to<IEnumerable<string>>
    {
        public character_separated()
        {
            given_rule(
                (Grammar.Character('a') + 'b').List(' '));
            when_matching("ab", "ab ab ab");
        }

        [Fact]
        public void one_repetition()
        {
            results.ElementAt(0).IsMatch.ShouldBeTrue();
            results.ElementAt(0).Value.Single().ShouldEqual("ab");
        }

        [Fact]
        public void multiple()
        {
            results.ElementAt(1).IsMatch.ShouldBeTrue();
            var vals = results.ElementAt(1).Value.ToArray();
            vals.ShouldAllEqual("ab");

        }
    }
}