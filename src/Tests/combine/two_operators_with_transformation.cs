using System;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.combine
{
    public class two_operators_with_transformation : parsing_text_to<string>
    {
        public two_operators_with_transformation()
        {
            given_rule(from openParenthesis in Grammar.Character('(')
                                                      .Select(c => c + string.Empty)
                       from closeParenthesis in Grammar.Character(')')
                       select openParenthesis + closeParenthesis);

            when_matching("()");
        }

        [Fact]
        public void position_s_correct()
        {
            input.Position.ShouldEqual(2);
        }

        [Fact]
        public void result_is_correct()
        {
            result.ShouldMatch("()");
        }
    }
}
