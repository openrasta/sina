using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Xunit;

namespace Tests.conditional
{
    public class match : contexts.parsing_text_to<string>
    {
        public match()
        {
            given_rule(from any in Grammar.AnyCharacter().Any().Select(_=>true)
                       where any
                       select "yay");
            when_matching("any old stuff");
        }

        [Fact]
        public void success()
        {
            result.ShouldMatch("yay",0,13);
            input.Position.ShouldEqual(13);

        }
        
    }
}