using System;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals.textual
{
    public class matches_n_times : parsing_text_to<string>
    {
        public matches_n_times()
        {
            given_rule(Grammar.Character('a').Repeat(2));
            when_matching("aa");
        }

        [Fact]
        public void match_value_is_correct()
        {
            result.ShouldMatch("aa");
        }
    }
}
