using System;
using System.Collections.Generic;
using System.Linq;
namespace GoogleHashCode2019.Helpers
{
    public static class Helper
    {
        public static int GetIntersectWeight(List<string> l1, List<string> l2)
        {
            int result = 0;
            result = l1.Intersect(l2).Count();
            return result;
        }
    }
}
