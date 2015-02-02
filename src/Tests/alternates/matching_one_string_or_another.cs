using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.alternates
{
    public class matching_one_string_or_another : contexts.parsing_text_to<string>
    {
        public matching_one_string_or_another()
        {
            var extRelType = Grammar.Not('"', '\'', ' ').Any();
            var regRelType = AbnfGrammar.LowercaseAlpha +
                             (AbnfGrammar.LowercaseAlpha / AbnfGrammar.Digit / '.' / '-').Any();
            given_rule((regRelType / extRelType).End());
            when_matching("http://google.com/stylesheet");
        }

        [Fact]
        public void matches()
        {
            result.IsMatch.ShouldBeTrue();
            result.Value.ShouldEqual("http://google.com/stylesheet");
        }
    }
}