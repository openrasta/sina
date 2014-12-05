using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.optionals
{
    public class absent : contexts.parsing_text_to<string>
    {
        public absent()
        {
            given_rule(Grammar.String("Sister").Optional());
            when_matching("Maria Rainer");
        }

        [Fact]
        public void is_match()
        {
            result.ShouldMatch(null);
        }

        [Fact]
        public void position_is_correct()
        {
            input.Position.ShouldEqual(0);
        }
    }
}
