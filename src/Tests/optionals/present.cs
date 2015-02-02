using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.optionals
{
    public class present : parsing_text_to<string>
    {
        public present()
        {
            given_rule(Grammar.Character('a').Optional() +
                       Grammar.Character('b'));
            when_matching("ab", "b");
        }

        [Fact]
        public void is_match()
        {
            result.ShouldMatch("ab");
            results[1].ShouldMatch("b");
        }

        [Fact]
        public void position_is_correct()
        {
            input.Position.ShouldEqual(2);
            inputs[1].Position.ShouldEqual(1);

        }
    }
}
