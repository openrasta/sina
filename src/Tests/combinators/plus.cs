using OpenRasta.Sina;
using Should;
using Xunit;

namespace Tests.combinators
{
    public class plus_right_member_backtracks : contexts.parsing_text_to<string>
    {
        public plus_right_member_backtracks()
        {
            given_rule((Character('a') + (Character('b') / Character('c'))).End());
            when_matching("abb");
        }

        [Fact]
        public void input_back_to_original_position()
        {
            input.Position.ShouldEqual(0);
        }
    }
    public class plus_left_member_backtracks : contexts.parsing_text_to<string>
    {
        public plus_left_member_backtracks()
        {
            given_rule((Character('a') / AnyCharacter() + Character('c')).End());
            when_matching("abb");
        }

        [Fact]
        public void input_back_to_original_position()
        {
            input.Position.ShouldEqual(0);
        }
    }
    public class plus_no_match : contexts.parsing_text_to<string>
    {
        public plus_no_match()
        {
            given_rule(Character('a') + Character('b'));
            when_matching("ayo");
        }

        [Fact]
        public void input_back_to_original_position()
        {
            input.Position.ShouldEqual(0);

        }
    }
    public class select_no_match : contexts.parsing_text_to<string>
    {
        public select_no_match()
        {
            given_rule(from a in Character('a') from b in  Character('b') select "true");
            when_matching("ayo");
        }

        [Fact]
        public void input_back_to_original_position()
        {
            input.Position.ShouldEqual(0);

        }
    }
}