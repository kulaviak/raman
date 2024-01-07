using System;

namespace Raman.Core
{
    public abstract class Util
    {
        public static float GetPixelDistance(System.Drawing.Point point1, System.Drawing.Point point2)
        {
            var x = Math.Abs(point1.X - point2.X);
            var y = Math.Abs(point1.Y - point2.Y);
            var ret = (float) Math.Sqrt(x*x + y*y);
            return ret;
        }
    }
}