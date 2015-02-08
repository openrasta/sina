using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.non_capturing
{
    public class multi_fold : contexts.parsing_text_to<string>
    {
        public multi_fold()
        {
            CharacterRule dquote = '"';
            given_rule(-dquote + Character('a') + -dquote + Character('='));
            when_matching("\"a\"=");
        }

        [Fact]
        public void success()
        {
            result.ShouldMatch("a=", 0, 4);
        }
    }
}