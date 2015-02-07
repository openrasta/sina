using System;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals.textual
{
    public class matches_n_times : parsing_text_to<string>
    {
        public matches_n_times()
        {
            given_rule(Grammar.Character('a').Count(2));
            when_matching("aa", "aaa", "a");
        }

        [Fact]
        public void match_value_is_correct()
        {
            results[0].ShouldMatch("aa",0,2);
            inputs[0].Position.ShouldEqual(2);
            results[1].ShouldMatch("aa", 0, 2);
            inputs[1].Position.ShouldEqual(2);
        }

        [Fact]
        public void not_enough_doesnt_match()
        {
            results[2].ShouldNotMatch();
        }
    }
}
