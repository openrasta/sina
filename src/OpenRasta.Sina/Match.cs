namespace OpenRasta.Sina
{
    public class Match<T>
    {
        public static readonly Match<T> None = new Match<T> { IsMatch = false };

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
    }
}
