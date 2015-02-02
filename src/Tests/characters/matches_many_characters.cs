using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.characters
{
    public class matches_many_characters : parsing_text_to<string>
    {
        public matches_many_characters()
        {
            given_rule(Grammar.Character('a') + Grammar.Character('b'));
            when_matching("ab");
        }

        [Fact]
        public void matches()
        {
            result.ShouldMatch("ab");
        }

        [Fact]
        public void position_is_set()
        {
            input.Position.ShouldEqual(2);
        }
    }
}
