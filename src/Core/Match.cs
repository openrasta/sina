using System;

namespace OpenRasta.Sina
{
    public class Match<T>
    {
        public static readonly Match<T> None = new Match<T> { IsMatch = false };

        public static Match<T> Empty(int position)
        {
            return new Match<T>(default(T), position, 0);
        }

        public Match(T value, int position, int length, Func<StringInput,Match<T>> backtrack = null)
        {
            Value = value;
            Position = position;
            Length = length;
            IsMatch = true;
            Backtrack = backtrack;
        }

        Match()
        {
        }

        public bool IsMatch { get; private set; }
        public T Value { get; private set; }
        public int Position { get; set; }
        public int Length { get; set; }
        public Func<StringInput, Match<T>> Backtrack { get; set; }
    }
}
