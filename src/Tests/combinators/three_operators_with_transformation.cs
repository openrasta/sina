using System;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.combinators
{
    public class three_operators_with_transformation : parsing_text_to<string>
    {
        public three_operators_with_transformation()
        {
            given_rule(from opening in Grammar.Character('(')
                       from text in Grammar.Character('a').RepeatExactly(2)
                       from closing in Grammar.Character(')')
                       select text);
            when_matching("(aa)");
        }

        [Fact]
        public void input_position_is_set()
        {
            input.Position.ShouldEqual(4);
        }

        [Fact]
        public void result_is_correct()
        {
            result.ShouldMatch("aa");
        }
    }
}
