using OpenRasta.Sina;
using Should;
using Tests.alternates;

namespace Tests.backtracking
{
    public class alternate
    {
        public void backtracks_to_next_entry()
        {
            var rule = (Grammar.String("c") / Grammar.String("cc")).End();

            var m = rule.Match("cc");
            m.IsMatch.ShouldBeTrue();
            m.Value.ShouldEqual("cc");
        }
    }
}