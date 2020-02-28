using System;
using System.Collections.Generic;
using System.Text;

namespace eRepublik.BBCodes
{
    public class BBCodes
    {
        public static string Bold(string txt)
        { return "[B]" + txt + "[/B]"; }

        public static string Italic(string txt)
        { return "[I]" + txt + "[/I]"; }

        public static string Underline(string txt)
        { return "[U]" + txt + "[/U]"; }

        public static string Superscript(string txt)
        { return "[SUP]" + txt + "[/SUP]"; }

        public static string Subscript(string txt)
        { return "[SUB]" + txt + "[/SUB]"; }

        public static string Center(string txt)
        { return "[CENTER]" + txt + "[/CENTER]"; }

        public static string Image(string url)
        { return "[IMG]" + url + "[/IMG]"; }

        public static string Hyperlink(string url, string txt = "")
        {
            if (txt == "")
                return "[URL]" + url + "[URL]";
            else
                return "[URL=" + url + "]" + txt + "[/URL]";
        }
    }
}
