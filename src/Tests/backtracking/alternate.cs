using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.alternates;
using Xunit;

namespace Tests.backtracking
{
    public class alternate
    {
        [Fact]
        public void backtrack_called_when_backtrack_supported()
        {
            ((Grammar.Character('a').Min(1) / Grammar.Character('b').Min(1)) + Grammar.Character('a'))
                .End()
                .Match("aa").ShouldMatch("aa");
        }
        [Fact]
        public void next_alternate_on_final()
        {
            var rule = (Grammar.String("c") / Grammar.String("cc")).End();

            rule.Match("cc").ShouldMatch("cc",0,2);

        }
    }
}