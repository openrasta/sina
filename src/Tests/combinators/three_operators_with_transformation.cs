using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.combinators
{
    public class quoted_strings : parsing_text_to<string>
    {
        public quoted_strings()
        {

            var ExtRelType = Not('"', '\'', ' ').Min(1);
            var RegRelType = LowercaseAlpha + (LowercaseAlpha.Any() / Digit.Any());/// Digit;//+  (LowercaseAlpha / Digit).Any();// / '.' / '-');//.Any();
            var RelationType = RegRelType / ExtRelType;

            var QuotedRelationTypes = from startQuote in DoubleQuote
                                      from type in RelationType//.List(' ')
                                      from endQuote in DoubleQuote
                                      select type;
            StringInput inp = "http://";
            var match = RegRelType.End().Match(inp);
            match.ShouldNotMatch();
            inp.Position.ShouldEqual(0);
            
            given_rule(QuotedRelationTypes.End());
            when_matching("\"http://google.com\"");
        }

        [Fact]
        public void matches()
        {
            result.Value.ShouldEqual("http://google.com");
        }
    }
    public class three_operators_with_transformation : parsing_text_to<string>
    {
        public three_operators_with_transformation()
        {
            given_rule(from opening in Character('(')
                       from text in Character('a').Count(2)
                       from closing in Character(')')
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
            result.ShouldMatch("aa", 0, 4);
        }
    }
}
