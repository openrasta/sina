using System;
using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.strings
{
    public class matches : contexts.parsing_text_to<string>
    {
        public matches()
        {
            given_rule(String("test"));
            when_matching("test");
        }

        [Fact]
        public void is_successful()
        {
            result.ShouldMatch("test");
        }

        [Fact]
        public void position_is_correct()
        {
            input.Position.ShouldEqual(4);
        }
    }
}
