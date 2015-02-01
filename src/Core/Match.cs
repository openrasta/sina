using System;

namespace OpenRasta.Sina
{
    public class Match<T>
    {
        public static readonly Match<T> None = new Match<T> { IsMatch = false };
        public static readonly Match<T> Empty = new Match<T>(default(T));
        public Match(T value)
        {
            Value = value;
            IsMatch = true;
        }

        Match()
        {
        }

        public bool IsMatch { get; private set; }
        public T Value { get; private set; }
        public Func<StringInput, Match<T>> Backtrack { get; set; }
    }
}
