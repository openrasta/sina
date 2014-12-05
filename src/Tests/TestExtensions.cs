using System.Collections.Generic;
using OpenRasta.Sina;
using Should;

namespace Tests
{
    public static class TestExtensions
    {
        public static void ShouldAllEqual<T>(this IEnumerable<T> sources, T expectedValue)
        {
            foreach (var value in sources)
                value.ShouldEqual(expectedValue);
        }

        public static void ShouldMatch<T>(this Match<T> match, T expectedValue)
        {
            match.IsMatch.ShouldBeTrue("Rule should have matched but did not.");
            match.Value.ShouldEqual(expectedValue);
        }

        public static void ShouldNotMatch<T>(this Match<T> match)
        {
            match.IsMatch.ShouldBeFalse("Rule should not have matched.");
        }
    }
}
