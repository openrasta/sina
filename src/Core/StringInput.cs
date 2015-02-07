namespace OpenRasta.Sina
{
    public class StringInput
    {
        int _position;

        public StringInput(string text)
        {
            Text = text;
        }

        public char Current
        {
            get { return Text[Position]; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string Text { get; private set; }

        public static implicit operator StringInput(string text)
        {
            return new StringInput(text);
        }

        public override string ToString()
        {
            return string.Format("{0}˰{1}", Text.Substring(0, Position), Text.Substring(Position));
        }
    }
}
