using System;
using System.Globalization;

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
        
        /// <summary>
        /// Universal decimal parser. Decimal delimiter can be both '.' and ','
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal? UniversalParseDecimal(string str)
        {
            str = str.Replace(",", ".");
            var style = NumberStyles.Number;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            if (Decimal.TryParse(str, style, culture, out var ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }
    }
}