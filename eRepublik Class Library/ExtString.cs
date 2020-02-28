using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}

public static class ExtString
{
    public static string RemoveNonDigits(this string s)
    { return RemoveNonDigits(s, new char[] { }); }
    public static string RemoveNonDigits(this string s, char[] exceptions)
    {
        for (int i = 0; i < s.Length; i++)
            if (!Char.IsDigit(s, i) && !(Array.IndexOf(exceptions, s[i]) > -1))
            {
                s = s.Remove(i, 1);
                i--;
            }

        return s;
    }
    public static string RemoveNewLines(this string s)
    {
        return s
            .Replace("\r\n", "")
            .Replace("\r", "")
            .Replace("\n", "");
    }

    public static string Reverse(this string s)
    {
        char[] c = s.ToCharArray();
        string rev = string.Empty;

        for (int i = s.Length - 1; i >= 0; i--)
            rev += c[i];

        return rev;
    }
}
