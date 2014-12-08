using System;

namespace OpenRasta.Sina.Rules
{
    public class StringRule : Rule<string>
    {
        readonly string _text;
        StringComparison _comparison;

        public StringRule(string text)
        {
            _text = text;
            _comparison = StringComparison.OrdinalIgnoreCase;
        }

        public StringRule CaseSensitive()
        {
            _comparison = StringComparison.Ordinal;
            return this;
        }

        public override Match<string> Match(StringInput input)
        {
            _comparison = StringComparison.OrdinalIgnoreCase;
            if (input.Text.IndexOf(_text, input.Position, _comparison) == -1)
                return Match<string>.None;
            var oldPosition = input.Position;
            input.Position += _text.Length;

            return new Match<string>(input.Text.Substring(oldPosition, _text.Length));
        }

        public override string ToString()
        {
            return string.Format("\"{0}\"", _text);
        }
    }
}
