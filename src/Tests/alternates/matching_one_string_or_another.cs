using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.alternates
{
    public class not_matching : contexts.parsing_text_to<string>
    {
        public not_matching()
        {
            given_rule((Character('a') + 'b') / (Character('a')  + 'c'));
            when_matching("ad");
        }

        [Fact]
        public void input_position_back()
        {
            input.Position.ShouldEqual(0);
        }
    }
    public class matching_one_string_or_another : contexts.parsing_text_to<string>
    {
        public matching_one_string_or_another()
        {
            var extRelType = Not('"', '\'', ' ').Any();
            var regRelType = (LowercaseAlpha / Digit ).Any();
            given_rule((regRelType / extRelType).End());
            when_matching("http://google.com/stylesheet");
        }

        [Fact]
        public void matches()
        {
            result.ShouldMatch("http://google.com/stylesheet", 0, input.Text.Length);
        }
    }
}