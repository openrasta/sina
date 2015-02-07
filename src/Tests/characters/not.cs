using System;
using System.Linq;
using System.Text;
using OpenRasta.Sina;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.characters
{
    public class not : parsing_text_to<char>
    {
        public not()
        {
            given_rule(Grammar.Not('a'));
            when_matching("a", "b", "c", "d", "e", "f", "g",
                          "h", "i", "j", "k", "l", "m", "n",
                          "o", "p", "q", "r", "s", "t", "u",
                          "v", "w", "x", "y", "z");
        }

        [Fact]
        public void doesnt_match_excluded_character()
        {
            results[0].ShouldNotMatch();
        }

        [Fact]
        public void matches_other_characters()
        {
            results.Skip(1).Select(_ => _.IsMatch).ShouldAllEqual(true);
            results.Skip(1)
                   .Aggregate(new StringBuilder(), (builder, match) => builder.Append(match.Value))
                   .ToString()
                   .ShouldEqual("bcdefghijklmnopqrstuvwxyz");
        }
    }
}