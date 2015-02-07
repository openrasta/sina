using System.Collections.Generic;
using System.Linq;
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

        public static void ShouldMatch<T>(this Match<T> match, T expectedValue, int? expectedPosition = null, int? expectedLength = null)
        {
            match.IsMatch.ShouldBeTrue("Rule should have matched but did not.");
            match.Value.ShouldEqual(expectedValue);
            if (expectedPosition.HasValue)
                match.Position.ShouldEqual((int)expectedPosition);
            if (expectedLength.HasValue)
                match.Length.ShouldEqual((int)expectedLength);
        }

        public static void ShouldMatch<T>(this Match<IEnumerable<T>> match, int? expectedPosition = null, int? expectedLength = null, params T[] expectedValue)
        {
            match.IsMatch.ShouldBeTrue("Rule should have matched but did not.");
            match.Value.SequenceEqual(expectedValue).ShouldBeTrue();
            if (expectedPosition.HasValue)
                match.Position.ShouldEqual((int)expectedPosition);
            if (expectedLength.HasValue)
                match.Length.ShouldEqual((int)expectedLength);
        }

        public static void ShouldNotMatch<T>(this Match<T> match)
        {
            match.IsMatch.ShouldBeFalse("Rule should not have matched.");
        }
    }
}
