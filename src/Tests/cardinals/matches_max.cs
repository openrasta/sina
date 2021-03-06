﻿using System.Collections.Generic;
using System.Linq;
using OpenRasta.Sina;
using OpenRasta.Sina.Rules;
using Should;
using Tests.contexts;
using Xunit;

namespace Tests.cardinals
{
    public class matches_max : parsing_text_to<IEnumerable<int>>
    {
        public matches_max()
        {
            given_rule(Range('0', '9').Select(_ => (int)_).Max(1));
            when_matching(string.Empty, "1");
        }

        [Fact]
        public void success()
        {
            results[0].ShouldMatch(new int[0], 0, 0);
            results[1].Value.ElementAt(0).ShouldEqual('1');
            results[1].Position.ShouldEqual(0);
            results[1].Length.ShouldEqual(1);
            inputs[1].Position.ShouldEqual(1);

        }
    }
}
