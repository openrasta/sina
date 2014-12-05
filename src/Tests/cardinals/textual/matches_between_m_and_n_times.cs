using System;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals.textual
{
    public class matches_between_m_and_n_times : parsing_text_to<string>
    {
        public matches_between_m_and_n_times()
        {
            given_rule(Grammar.Character('z').Repeat(1, 2));
            when_matching("a", "z", "zz");
        }

        [Fact]
        public void doesnt_match_outside_of_range()
        {
            results[0].ShouldNotMatch();
        }

        [Fact]
        public void matches_maximum()
        {
            results[2].ShouldMatch("zz");
        }

        [Fact]
        public void matches_minimum()
        {
            results[1].ShouldMatch("z");
        }
    }
}
