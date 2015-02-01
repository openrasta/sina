using OpenRasta.Sina;

namespace Tests.range
{
    public class combine_matches : contexts.parsing_text_to<char>
    {
        public combine_matches()
        {
            given_rule(Grammar.Range('a', 'm') / Grammar.Range('n', 'z'));
        }
    }
}