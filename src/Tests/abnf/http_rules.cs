using OpenRasta.Sina;

namespace Tests.abnf
{
    public class http_rules : HttpGrammar
    {
        [Xunit.FactAttribute]
        public void match()
        {
            OptionalWhiteSpace.ShouldMatch(string.Empty, string.Empty, "\t", "\t ");
        }
    }
}
