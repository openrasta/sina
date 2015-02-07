using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.non_capturing
{
    public class char_fold_left : contexts.parsing_text_to<char>
    {
        public char_fold_left()
        {
            given_rule(Grammar.Character('a') + -Grammar.Character('b'));
            when_matching("ab");
        }

        [Fact]
        public void success()
        {
            result.ShouldMatch('a', 0, 2);
            input.Position.ShouldEqual(2);
        }
    }
}
