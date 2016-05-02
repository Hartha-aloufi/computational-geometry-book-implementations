using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvexHull
{
    class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public static bool isCW(Point a, Point b, Point c){
            int area = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);

            if (area < 0)
                return true;
            return false;
        }
    }
}
