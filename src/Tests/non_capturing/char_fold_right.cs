using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.non_capturing
{
    public class char_fold_right : contexts.parsing_text_to<char>
    {
        public char_fold_right()
        {
            given_rule(-Character('a') + Character('b'));
            when_matching("ab");
        }

        [Fact]
        public void success()
        {
            result.ShouldMatch('b',0,2);
        }
    }
}