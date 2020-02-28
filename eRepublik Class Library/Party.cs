using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Net;
using eRepublik;
using HtmlAgilityPack;

namespace eRepublik.Parties
{
    public class Party
    {
        #region Properties - General
        public int ID { get { return id; } } int id;
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Link { get { return "http://www.erepublik.com/en/party/" + ID; } }
        public string Wiki { get { return "http://wiki.erepublik.com/index.php/" + Name.Replace(" ", "_"); } }
        public string Country { get; set; }
        public int Members { get; set; }
        public string Orientation { get; set; }
        public string President { get; set; }
        public int PresidentID { get; set; }
        public int CongressMembers { get; set; }
        public float CongressOccupancy { get; set; }
        #endregion

        public Party(int id)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionFixNestedTags = true;
            
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                doc.LoadHtml(wc.DownloadString("http://www.erepublik.com/en/party/" + id));
            }

            this.id = id;
            Name = doc.DocumentNode.SelectSingleNode("//div[@id='profileholder']/p/h1").InnerText;
            Avatar = doc.DocumentNode.SelectSingleNode("//div[@id='filters']/div/img").Attributes["src"].Value.Replace("_55x55", "");
            Country = doc.DocumentNode.SelectSingleNode("//div[@id='profileholder']/p/a[4]").InnerText;
            Members = Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//div[@id='content']/div[4]/div[5]/div[1]/p[1]/span[2]").InnerText);
            Orientation = doc.DocumentNode.SelectSingleNode("//div[@id='content']/div[4]/div[5]/div[1]/p[2]/span[2]").InnerText;
            President = doc.DocumentNode.SelectSingleNode("//div[@id='profileholder']/p/a[2]").InnerText;
            PresidentID = Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//div[@id='profileholder']/p/a[1]").Attributes["href"].Value.RemoveNonDigits());
            CongressMembers = Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//div[@id='content']/div[5]/div/div[2]/div/div[1]/div/p/span").InnerText.Trim());
            CongressOccupancy = Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//div[@id='content']/div[5]/div/div[2]/div/div[1]/p/span").InnerText.RemoveNonDigits());
        }
    }
}
