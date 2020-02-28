using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace eRepublik
{
    public static class GameInfo
    {
        public static DateTime ReleaseDate { get { return new DateTime(2007, 11, 20); } }
        public static int eDay { get { return (int)(DateTime.Now - ReleaseDate).TotalDays; } }
        public static CultureInfo Culture
        {
            get
            {
                CultureInfo ci = new CultureInfo("en-US");

                ci.NumberFormat.NumberGroupSeparator = ",";
                ci.NumberFormat.NumberDecimalSeparator = ".";
                ci.DateTimeFormat.FullDateTimePattern = "MMMM dd, yyyy";

                return ci;
            }
        }
    }
}
