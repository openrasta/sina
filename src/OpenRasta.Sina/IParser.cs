namespace OpenRasta.Sina
{
    public interface IParser<T>
    {
        Match<T> Match(StringInput input);
    }
}
