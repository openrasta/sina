using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.alternates
{
    public class matching_one_char_or_another : contexts.parsing_text_to<char>
    {
        public matching_one_char_or_another()
        {
            given_rule(AbnfGrammar.Alternates(Grammar.Character('a'), 
                                              Grammar.Character('b')));
            when_matching("ba");
        }

        [Fact]
        public void input_position_is_set()
        {
            input.Position.ShouldEqual(1);
        }

        [Fact]
        public void matches()
        {
            result.ShouldMatch('b');
        }
    }
}
