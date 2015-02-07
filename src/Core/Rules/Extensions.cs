using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRasta.Sina.Rules
{
    public static class Extensions
    {
        public static Func<StringInput, Match<T>> AsBacktrack<T>(this Stack<Func<StringInput, Match<T>>> tracks)
        {
            if (tracks.Any() == false) return null;
            return input =>
            {
                Match<T> next;
                do
                {
                    next = tracks.Pop()(input);
                }
                while (tracks.Count > 0 && next.IsMatch == false);

                if (next.Backtrack != null)
                    tracks.Push(next.Backtrack);
                return next.IsMatch
                           ? new Match<T>(next.Value, next.Position, next.Length, tracks.AsBacktrack())
                           : next;
            };
        }
    }
}