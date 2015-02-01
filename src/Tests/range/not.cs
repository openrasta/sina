using OpenRasta.Sina;

namespace Tests.range
{
    public class not : contexts.parsing_text_to<char>
    {
        public not()
        {
            given_rule(!Grammar.Character('a'));
        }
    }
}