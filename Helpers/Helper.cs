using System;
using System.Collections.Generic;
using System.Linq;
using GoogleHashCode2019.Classes;

namespace GoogleHashCode2019.Helpers
{
    public static class Helper
    {
        public static Stats GetIntersectWeight(Photo p1, Photo p2)
        {
            Stats stats = new Stats
            {
                Common = p1.Tags.Intersect(p2.Tags).Count()
            };
            stats.MinDiff = Math.Min(p1.Tags.Count - stats.Common, p2.Tags.Count - stats.Common);
            stats.MinDiff = Math.Min(stats.MinDiff, stats.Common);
            return stats;
        }
    }
}
