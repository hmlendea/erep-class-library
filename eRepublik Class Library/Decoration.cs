using System;
using System.Collections.Generic;
using System.Text;

namespace eRepublik.Citizen.Achievements
{
    public class Decoration
    {
        public string ImageLink { get; set; }
        public string Text { get; set; }
        public int Count { get; set; }

        public Decoration(string imageLink, string text, int count)
        {
            ImageLink = imageLink;
            Text = text;
            Count = count;
        }

        public Decoration(string imageLink, string text)
        {
            ImageLink = imageLink;
            Text = text;
            Count = 1;
        }

        public Decoration()
        {
            ImageLink = "";
            Text = "N/A";
            Count = 0;
        }
    }
}
