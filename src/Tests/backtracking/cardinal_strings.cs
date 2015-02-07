using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.backtracking
{
    public class cardinal_strings
        : contexts.parsing_text_to<string>
    {
        public cardinal_strings()
        {
            given_rule((Grammar.Character('a') / 'b').Range(2, 2).End());
            when_matching("bb");
        }

        [Fact]
        public void successful()
        {
            result.ShouldMatch("bb", 0, 2);
            input.Position.ShouldEqual(2);
        }}
}