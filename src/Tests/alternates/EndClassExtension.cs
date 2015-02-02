using OpenRasta.Sina;
using OpenRasta.Sina.Rules;

namespace Tests.alternates
{
    public static class EndClassExtension
    {
        public static Rule<T> End<T>(this Rule<T> rule)
        {
            return rule + -(new EndRule<T>());
        }

        class EndRule<T> : Rule<T>
        {

            public override Match<T> Match(StringInput input)
            {
                return (input.Position >= input.Text.Length)
                           ? Match<T>.Empty
                           : Match<T>.None;
            }
        }
    }
}