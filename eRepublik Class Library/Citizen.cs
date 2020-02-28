using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Net;
using eRepublik;
using eRepublik.Citizen.Achievements;
using HtmlAgilityPack;

namespace eRepublik.Citizens
{
    #region SubClasses
    public class CitizenResidence
    {
        public string Country { get; set; }
        public string Region { get; set; }

        public CitizenResidence(string country, string region)
        {
            Country = country;
            Region = region;
        }
    }
    public class CitizenBirthDay
    {
        public DateTime Date { get; set; }
        public int Age { get { return (int)(DateTime.Now - Date).TotalDays; } }
        public int eDay { get { return GameInfo.eDay - Age; } }

        public CitizenBirthDay(DateTime date)
        { Date = date; }
    }
    public class CitizenDamage
    {
        public long Damage { get; set; }
        public string Country { get; set; }

        public CitizenDamage(long damage, string country)
        {
            Damage = damage;
            Country = country;
        }
    }
    public class CitizenGroup
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Position { get; set; }
        public int Members
        {
            get;
            set;
        }
        public CitizenGroup(string name, int id, string position, int members)
        {
            Name = name;
            ID = id;
            Position = position;
            Members = members;
        }
    }
    public class CitizenBombsUsed
    {
        public int Small { get; set; }
        public int Big { get; set; }
        public int Total { get { return Small + Big; } }
        public string LastBombUsed { get; set; }

        public CitizenBombsUsed(int small, int big, string lastBombUsed)
        {
            Small = small;
            Big = big;
            LastBombUsed = lastBombUsed;
        }
        public CitizenBombsUsed(int small, int big)
        {
            Small = small;
            Big = big;
            LastBombUsed = "Unknown";
        }
    }
    public class CitizenRanking
    {
        public int Experience { get; set; }
        public int Strength { get; set; }
        public int Rank { get; set; }
        public int Hit { get; set; }
        public int Medals { get; set; }

        public CitizenRanking(int experience, int strength, int rank, int hit, int medals)
        {
            Experience = experience;
            Strength = strength;
            Rank = rank;
            Hit = hit;
            Medals = medals;
        }
    }
    public class GuerillaScore
    {
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Total { get { return Won + Lost; } }

        public GuerillaScore(int won, int lost)
        {
            Won = won;
            Lost = lost;
        }
    }
    #endregion

    public class Citizen
    {
        #region Properties - General
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfileLink { get { return "http://www.erepublik.com/en/citizen/profile/" + ID; } }
        public string AvatarLink { get; set; }
        public int NationalRank { get; set; }
        public int Friends { get; set; }
        public string Citizenship { get; set; }
        public string FirstFriendName { get; set; }
        public CitizenResidence Residence { get; set; }
        public Medals Medals { get; set; }
        public Medals MedalsGained { get; set; }
        public Decoration[] Decoration
        {
            get { return decoration; }
            set { decoration = value; }
        } Decoration[] decoration;
        public int DecorationsTotal
        {
            get
            {
                int nr = 0;

                for (int i = 0; i < Decoration.Length; i++)
                    nr += Decoration[i].Count;

                return nr;
            }
        }
        public CitizenBirthDay BirthDay { get; set; }
        public string CitizenStatus
        {
            get
            {
                if (Strength >= 50)
                    return "Adult";
                else
                    return "Young";
            }
        }
        public bool EliteCitizen { get { return Level >= 101; } }
        public bool Ambassador { get; set; }
        public bool Moderator { get; set; }
        public CitizenGroup PoliticalParty { get; set; }
        public CitizenGroup Newspaper { get; set; }
        #endregion
        #region Properties - Military
        public int Division
        {
            get
            {
                if (Level <= 34)
                    return 1;
                else if (Level <= 49)
                    return 2;
                else if (Level <= 69)
                    return 3;
                else
                    return 4;
            }
        }
        public string Rank { get; set; }
        public int RankValue
        {
            get
            {
                switch (Rank)
                {
                    case "Recruit ":
                        return 1;
                    case "Private ":
                        return 2;
                    case "Private *":
                        return 3;
                    case "Private **":
                        return 4;
                    case "Private ***":
                        return 5;
                    case "Corporal ":
                        return 6;
                    case "Corporal *":
                        return 7;
                    case "Corporal **":
                        return 8;
                    case "Corporal ***":
                        return 9;
                    case "Sergeant ":
                        return 10;
                    case "Sergeant *":
                        return 11;
                    case "Sergeant **":
                        return 12;
                    case "Sergeant ***":
                        return 13;
                    case "Lieutenant ":
                        return 14;
                    case "Lieutenant *":
                        return 15;
                    case "Lieutenant **":
                        return 16;
                    case "Lieutenant ***":
                        return 17;
                    case "Captain ":
                        return 18;
                    case "Captain *":
                        return 19;
                    case "Captain **":
                        return 20;
                    case "Captain ***":
                        return 21;
                    case "Major ":
                        return 22;
                    case "Major *":
                        return 23;
                    case "Major **":
                        return 24;
                    case "Major ***":
                        return 25;
                    case "Commander ":
                        return 26;
                    case "Commander *":
                        return 27;
                    case "Commander **":
                        return 28;
                    case "Commander ***":
                        return 29;
                    case "Lt. Colonel ":
                        return 30;
                    case "Lt. Colonel *":
                        return 31;
                    case "Lt. Colonel **":
                        return 32;
                    case "Lt. Colonel ***":
                        return 33;
                    case "Colonel ":
                        return 34;
                    case "Colonel *":
                        return 35;
                    case "Colonel **":
                        return 36;
                    case "Colonel ***":
                        return 37;
                    case "General ":
                        return 38;
                    case "General *":
                        return 39;
                    case "General **":
                        return 40;
                    case "General ***":
                        return 41;
                    case "Field Marshal ":
                        return 42;
                    case "Field Marshal *":
                        return 43;
                    case "Field Marshal **":
                        return 44;
                    case "Field Marshal ***":
                        return 45;
                    case "Supreme Marshal ":
                        return 46;
                    case "Supreme Marshal *":
                        return 47;
                    case "Supreme Marshal **":
                        return 48;
                    case "Supreme Marshal ***":
                        return 49;
                    case "National Force ":
                        return 50;
                    case "National Force *":
                        return 51;
                    case "National Force **":
                        return 52;
                    case "National Force ***":
                        return 53;
                    case "World Class Force ":
                        return 54;
                    case "World Class Force *":
                        return 55;
                    case "World Class Force **":
                        return 56;
                    case "World Class Force ***":
                        return 57;
                    case "Legendary Force ":
                        return 58;
                    case "Legendary Force *":
                        return 59;
                    case "Legendary Force **":
                        return 60;
                    case "Legendary Force ***":
                        return 61;
                    case "God of War ":
                        return 62;
                    case "God of War *":
                        return 63;
                    case "God of War **":
                        return 64;
                    case "God of War ***":
                        return 65;
                    case "Titan ":
                        return 66;
                    case "Titan *":
                        return 67;
                    case "Titan **":
                        return 68;
                    case "Titan ***":
                        return 69;
                    default:
                        return 0;
                }
            }
        }
        public string RankUp { get; set; }
        public double RankPoints { get; set; }
        public int RankPointsGained { get; set; }
        public long Influence { get; set; }
        public CitizenDamage TruePatriotDamage { get; set; }
        public CitizenDamage TopCampaignDamage { get; set; }
        public long MonthlyDamage { get; set; }
        public CitizenRanking WorldRanking { get; set; }
        public CitizenRanking CountryRanking { get; set; }
        public CitizenRanking UnitRanking { get; set; }
        public int HitQ0 { get { return GetHitDamage(0); } }
        public int HitQ1 { get { return GetHitDamage(1); } }
        public int HitQ2 { get { return GetHitDamage(2); } }
        public int HitQ3 { get { return GetHitDamage(3); } }
        public int HitQ4 { get { return GetHitDamage(4); } }
        public int HitQ5 { get { return GetHitDamage(5); } }
        public int HitQ6 { get { return GetHitDamage(6); } }
        public int HitQ7 { get { return GetHitDamage(7); } }
        public int Experience { get; set; }
        public int ExperienceGained { get; set; }
        public int Level { get; set; }
        public string LevelUp { get; set; }
        public int Strength { get; set; }
        public int StrengthGained { get; set; }
        public CitizenBombsUsed BombsUsed { get; set; }
        public CitizenGroup MilitaryUnit { get; set; }
        public GuerillaScore GuerillaScore { get; set; }
        #endregion

        public Citizen(bool isOldStatistics = false)
        {
            Medals = new Medals();
            MedalsGained = new Medals();
            Decoration = new Decoration[0];
        }

        public void Load(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);

                XmlNode root = xml.SelectSingleNode("Citizen");

                //ID = Convert.ToInt32(root.SelectSingleNode("ID").InnerText);
                Name = root.SelectSingleNode("Name").InnerText;
                Strength = Convert.ToInt32(root.SelectSingleNode("Strength").InnerText);
                Rank = root.SelectSingleNode("Rank").InnerText;
                RankPoints = Convert.ToDouble(root.SelectSingleNode("RankPoints").InnerText);
                Influence = Convert.ToInt32(root.SelectSingleNode("Influence").InnerText);
                Experience = Convert.ToInt32(root.SelectSingleNode("Experience").InnerText);
                Level = Convert.ToInt32(root.SelectSingleNode("Level").InnerText);
                NationalRank = Convert.ToInt32(root.SelectSingleNode("NationalRank").InnerText);
                Friends = Convert.ToInt16(root.SelectSingleNode("Friends").InnerText);
                Citizenship = root.SelectSingleNode("Citizenship").InnerText;
                BirthDay = new CitizenBirthDay(Convert.ToDateTime(root.SelectSingleNode("BirthDay").InnerText, GameInfo.Culture));

                Residence = new CitizenResidence(
                    root.SelectSingleNode("Residence/Country").InnerText,
                    root.SelectSingleNode("Residence/Region").InnerText);

                PoliticalParty = new CitizenGroup(
                    root.SelectSingleNode("PoliticalParty/Name").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("PoliticalParty/ID").InnerText),
                    root.SelectSingleNode("PoliticalParty/Position").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("PoliticalParty/Members").InnerText));

                Newspaper = new CitizenGroup(
                    root.SelectSingleNode("Newspaper/Name").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("Newspaper/ID").InnerText),
                    root.SelectSingleNode("Newspaper/Position").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("Newspaper/Members").InnerText));

                MilitaryUnit = new CitizenGroup(
                    root.SelectSingleNode("MilitaryUnit/Name").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("MilitaryUnit/ID").InnerText),
                    root.SelectSingleNode("MilitaryUnit/Position").InnerText,
                    Convert.ToInt32(root.SelectSingleNode("MilitaryUnit/Members").InnerText));

                TruePatriotDamage = new CitizenDamage(
                    Convert.ToInt64(root.SelectSingleNode("TruePatriotDamage/Damage").InnerText),
                    root.SelectSingleNode("TruePatriotDamage/Country").InnerText);

                TopCampaignDamage = new CitizenDamage(
                    Convert.ToInt64(root.SelectSingleNode("TopCampaignDamage/Damage").InnerText),
                    root.SelectSingleNode("TopCampaignDamage/Country").InnerText);

                BombsUsed = new CitizenBombsUsed(
                    Convert.ToInt32(root.SelectSingleNode("BombsUsed/Small").InnerText),
                    Convert.ToInt32(root.SelectSingleNode("BombsUsed/Big").InnerText));

                Medals.FreedomFighter = Convert.ToInt16(root.SelectSingleNode("Medals/FreedomFighter").InnerText);
                Medals.HardWorker = Convert.ToInt16(root.SelectSingleNode("Medals/HardWorker").InnerText);
                Medals.CongressMember = Convert.ToInt16(root.SelectSingleNode("Medals/CongressMember").InnerText);
                Medals.CountryPresident = Convert.ToInt16(root.SelectSingleNode("Medals/CountryPresident").InnerText);
                Medals.MediaMogul = Convert.ToInt16(root.SelectSingleNode("Medals/MediaMogul").InnerText);
                Medals.BattleHero = Convert.ToInt16(root.SelectSingleNode("Medals/BattleHero").InnerText);
                Medals.CampaignHero = Convert.ToInt16(root.SelectSingleNode("Medals/CampaignHero").InnerText);
                Medals.ResistanceHero = Convert.ToInt16(root.SelectSingleNode("Medals/ResistanceHero").InnerText);
                Medals.SuperSoldier = Convert.ToInt16(root.SelectSingleNode("Medals/SuperSoldier").InnerText);
                Medals.SocietyBuilder = Convert.ToInt16(root.SelectSingleNode("Medals/SocietyBuilder").InnerText);
                Medals.Mercenary = Convert.ToInt16(root.SelectSingleNode("Medals/Mercenary").InnerText);
                Medals.TopFighter = Convert.ToInt16(root.SelectSingleNode("Medals/TopFighter").InnerText);
                Medals.TruePatriot = Convert.ToInt16(root.SelectSingleNode("Medals/TruePatriot").InnerText);
            }
            catch// (Exception ex)
            {
                //Log.AppendText(DateTime.Now.ToString(LogDateFormat) + "Failed to load old data for " +ID + ", attempting to scan him" + Environment.NewLine);
                //MessageBox.Show(ID + " " + ex.ToString());
                Scan();
            }
        }
        public void Save(string path)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = (XmlElement)xml.AppendChild(xml.CreateElement("Citizen"));

            root.AppendChild(xml.CreateElement("ID")).InnerText = ID.ToString();
            root.AppendChild(xml.CreateElement("Name")).InnerText = Name;
            root.AppendChild(xml.CreateElement("Strength")).InnerText = Strength.ToString();
            root.AppendChild(xml.CreateElement("Rank")).InnerText = Rank;
            root.AppendChild(xml.CreateElement("RankValue")).InnerText = RankValue.ToString();
            root.AppendChild(xml.CreateElement("RankPoints")).InnerText = RankPoints.ToString();
            root.AppendChild(xml.CreateElement("Influence")).InnerText = Influence.ToString();
            root.AppendChild(xml.CreateElement("Hit")).InnerText = HitQ7.ToString();
            root.AppendChild(xml.CreateElement("Experience")).InnerText = Experience.ToString();
            root.AppendChild(xml.CreateElement("Level")).InnerText = Level.ToString();
            root.AppendChild(xml.CreateElement("NationalRank")).InnerText = NationalRank.ToString();
            root.AppendChild(xml.CreateElement("Friends")).InnerText = Friends.ToString();
            root.AppendChild(xml.CreateElement("Citizenship")).InnerText = Citizenship;
            root.AppendChild(xml.CreateElement("BirthDay")).InnerText = BirthDay.Date.ToString(GameInfo.Culture.DateTimeFormat.FullDateTimePattern, GameInfo.Culture);

            XmlElement newspaper = (XmlElement)root.AppendChild(xml.CreateElement("Newspaper"));
            newspaper.AppendChild(xml.CreateElement("Name")).InnerText = Newspaper.Name;
            newspaper.AppendChild(xml.CreateElement("ID")).InnerText = Newspaper.ID.ToString();
            newspaper.AppendChild(xml.CreateElement("Position")).InnerText = Newspaper.Position;
            newspaper.AppendChild(xml.CreateElement("Members")).InnerText = Newspaper.ID.ToString();

            XmlElement politicalParty = (XmlElement)root.AppendChild(xml.CreateElement("PoliticalParty"));
            politicalParty.AppendChild(xml.CreateElement("Name")).InnerText = PoliticalParty.Name;
            politicalParty.AppendChild(xml.CreateElement("ID")).InnerText = PoliticalParty.ID.ToString();
            politicalParty.AppendChild(xml.CreateElement("Position")).InnerText = PoliticalParty.Position;
            politicalParty.AppendChild(xml.CreateElement("Members")).InnerText = PoliticalParty.ID.ToString();

            XmlElement militaryUnit = (XmlElement)root.AppendChild(xml.CreateElement("MilitaryUnit"));
            militaryUnit.AppendChild(xml.CreateElement("Name")).InnerText = MilitaryUnit.Name;
            militaryUnit.AppendChild(xml.CreateElement("ID")).InnerText = MilitaryUnit.ID.ToString();
            militaryUnit.AppendChild(xml.CreateElement("Position")).InnerText = MilitaryUnit.Position;
            militaryUnit.AppendChild(xml.CreateElement("Members")).InnerText = MilitaryUnit.ID.ToString();

            XmlElement residence = (XmlElement)root.AppendChild(xml.CreateElement("Residence"));
            residence.AppendChild(xml.CreateElement("Country")).InnerText = Residence.Country;
            residence.AppendChild(xml.CreateElement("Region")).InnerText = Residence.Region;

            XmlElement truePatriotDamage = (XmlElement)root.AppendChild(xml.CreateElement("TruePatriotDamage"));
            truePatriotDamage.AppendChild(xml.CreateElement("Damage")).InnerText = TruePatriotDamage.Damage.ToString();
            truePatriotDamage.AppendChild(xml.CreateElement("Country")).InnerText = TruePatriotDamage.Country;

            XmlElement topCampaignDamage = (XmlElement)root.AppendChild(xml.CreateElement("TopCampaignDamage"));
            topCampaignDamage.AppendChild(xml.CreateElement("Damage")).InnerText = TopCampaignDamage.Damage.ToString();
            topCampaignDamage.AppendChild(xml.CreateElement("Country")).InnerText = TopCampaignDamage.Country;

            XmlElement bombsUsed = (XmlElement)root.AppendChild(xml.CreateElement("BombsUsed"));
            bombsUsed.AppendChild(xml.CreateElement("Small")).InnerText = BombsUsed.Small.ToString();
            bombsUsed.AppendChild(xml.CreateElement("Big")).InnerText = BombsUsed.Big.ToString();

            XmlElement medals = (XmlElement)root.AppendChild(xml.CreateElement("Medals"));

            medals.AppendChild(xml.CreateElement("FreedomFighter")).InnerText = Medals.FreedomFighter.ToString();
            medals.AppendChild(xml.CreateElement("HardWorker")).InnerText = Medals.HardWorker.ToString();
            medals.AppendChild(xml.CreateElement("CongressMember")).InnerText = Medals.CongressMember.ToString();
            medals.AppendChild(xml.CreateElement("CountryPresident")).InnerText = Medals.CountryPresident.ToString();
            medals.AppendChild(xml.CreateElement("MediaMogul")).InnerText = Medals.MediaMogul.ToString();
            medals.AppendChild(xml.CreateElement("BattleHero")).InnerText = Medals.BattleHero.ToString();
            medals.AppendChild(xml.CreateElement("CampaignHero")).InnerText = Medals.CampaignHero.ToString();
            medals.AppendChild(xml.CreateElement("ResistanceHero")).InnerText = Medals.ResistanceHero.ToString();
            medals.AppendChild(xml.CreateElement("SuperSoldier")).InnerText = Medals.SuperSoldier.ToString();
            medals.AppendChild(xml.CreateElement("SocietyBuilder")).InnerText = Medals.SocietyBuilder.ToString();
            medals.AppendChild(xml.CreateElement("Mercenary")).InnerText = Medals.Mercenary.ToString();
            medals.AppendChild(xml.CreateElement("TopFighter")).InnerText = Medals.TopFighter.ToString();
            medals.AppendChild(xml.CreateElement("TruePatriot")).InnerText = Medals.TruePatriot.ToString();

            if (Directory.Exists("Data//Citizens") == false)
                Directory.CreateDirectory("Citizens");

            xml.Save(path);
        }
        public void Scan()
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlDocument egov = new HtmlAgilityPack.HtmlDocument();

            using (WebClient wc = new WebClient())
            {
                doc.LoadHtml(wc.DownloadString("http://www.erepublik.com/en/citizen/profile/" + ID));
                egov.LoadHtml(wc.DownloadString("http://www.egov4you.info/citizen/overview/" + ID));
            }

            Citizen OldStatistics = (Citizen)this.MemberwiseClone();
            OldStatistics.Medals = Medals.Clone(); // Needed!!!

            Name = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'citizen_profile_header')]/h2").InnerText.Trim();
            Strength = (int)Convert.ToDecimal(doc.DocumentNode.SelectSingleNode("//*[@class='citizen_military'][1]/h4").InnerText.Trim(), GameInfo.Culture);
            Rank = doc.DocumentNode.SelectSingleNode("//*[@class='citizen_military'][2]/h4/a").InnerHtml;
            RankPoints = Convert.ToDouble(doc.DocumentNode.SelectSingleNode("//div[@class='citizen_military'][2]/div/small[@style]").InnerText.Split('/')[0].RemoveNonDigits(), GameInfo.Culture);
            Level = Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//strong[@class='citizen_level']").InnerText);
            Experience = Convert.ToInt32(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_experience']/div/p").InnerText.Split('/')[0].RemoveNonDigits());
            NationalRank = Convert.ToInt16(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_second']/small[3]/strong").InnerText);
            Friends = Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//div[@class='citizen_activity']/h4[@class='friends_title']").InnerText.Split('(')[1].Split(')')[0]);
            FirstFriendName = doc.DocumentNode.SelectSingleNode("//div[@class='citizen_activity']/ul/li/a").Attributes["title"].Value;
            Citizenship = doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_info']/a[3]").InnerText.Trim();
            BirthDay = new CitizenBirthDay(Convert.ToDateTime(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_second']/p[2]").InnerText.Trim(), GameInfo.Culture));

            #region Avatar
            AvatarLink = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'citizen_profile_header')]/img").Attributes["style"].Value;
            AvatarLink = AvatarLink.Substring(AvatarLink.IndexOf('(') + 1);
            AvatarLink = AvatarLink.Substring(0, AvatarLink.Length - 2);
            AvatarLink = AvatarLink.Replace("_142x142", "");
            #endregion
            #region Residence
            Residence = new CitizenResidence(
                doc.DocumentNode.SelectSingleNode("//div[@class='citizen_info']/a[1]").InnerText.Trim(),
                doc.DocumentNode.SelectSingleNode("//div[@class='citizen_info']/a[2]").InnerText.Trim());
            #endregion
            #region PoliticalParty
            try
            {
                PoliticalParty = new CitizenGroup(
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][1]/div/span/a").InnerText.Trim(),
                    Convert.ToInt32(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][1]/div/span/a").Attributes["href"].Value.Split('/')[3].Reverse().Split('-')[0].Reverse()),
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][1]/h3").InnerText.Trim(), -1);

                HtmlAgilityPack.HtmlDocument docParty = new HtmlAgilityPack.HtmlDocument();
                docParty.LoadHtml(new WebClient().DownloadString("http://www.erepublik.com/en/party/" + PoliticalParty.ID));

                PoliticalParty.Members = Convert.ToInt16(docParty.DocumentNode.SelectSingleNode("//div[@class='indent']/div[@class='infoholder']/p/span[2]").InnerText.Split(' ')[0]);

                if (PoliticalParty.Position == "Vice President")
                    PoliticalParty.Position = "Vice Party President";
            }
            catch
            { PoliticalParty = new CitizenGroup("N/A", -1, "N/A", -1); }
            #endregion
            #region MilitaryUnit
            try
            {
                MilitaryUnit = new CitizenGroup(
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][2]/div/a/span").InnerText.Trim(),
                    Convert.ToInt32(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][2]/div/a").Attributes["href"].Value.Split('/')[4]),
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][2]/h3").InnerText.Trim(), -1);

                HtmlAgilityPack.HtmlDocument docMu = new HtmlAgilityPack.HtmlDocument();
                docMu.LoadHtml(new WebClient().DownloadString("http://www.erepublik.com/en/main/group-list/members/" + MilitaryUnit.ID));

                MilitaryUnit.Members = Convert.ToInt16(docMu.DocumentNode.SelectSingleNode("//div[@class='header_content']/h2/big").InnerText.Split(' ')[0]);
            }
            catch
            { MilitaryUnit = new CitizenGroup("N/A", -1, "N/A", -1); }
            #endregion
            #region Newspaper
            try
            {
                Newspaper = new CitizenGroup(
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][3]/div/a/span").InnerText.Trim(),
                    Convert.ToInt32(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][3]/div/a").Attributes["href"].Value.Split('/')[3].Reverse().Split('-')[0].Reverse()),
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_activity']/div[@class='place'][3]/h3").InnerText.Trim(), -1);

                //HtmlAgilityPack.HtmlDocument docNews = new HtmlAgilityPack.HtmlDocument();
                //docNews.LoadHtml(new WebClient().DownloadString("http://www.erepublik.com/en/newspaper/" + Newspaper.ID));

                //Newspaper.Members = Convert.ToInt16(docNews.DocumentNode.SelectSingleNode("//div[@class='header_content']/h2/big").InnerText.Split(' ')[0]);
            }
            catch
            { Newspaper = new CitizenGroup("N/A", -1, "N/A", -1); }
            #endregion
            #region TruePatriotDamage
            try
            {
                TruePatriotDamage = new CitizenDamage(
                    Convert.ToInt64(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_military'][3]/h4").InnerText.RemoveNonDigits()),
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_military'][3]/h4/img").Attributes["alt"].Value);
            }
            catch
            {
                TruePatriotDamage = new CitizenDamage(0, "None");
            }
            #endregion
            #region TopCampaignDamage
            try
            {
                TopCampaignDamage = new CitizenDamage(
                    Convert.ToInt64(doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_military'][4]/h4").InnerText.RemoveNonDigits()),
                    doc.DocumentNode.SelectSingleNode(".//div[@class='citizen_military'][4]/h4/img").Attributes["alt"].Value);
            }
            catch
            {
                TopCampaignDamage = new CitizenDamage(0, "None");
            }
            #endregion
            #region MonthlyDamage
            if (egov.DocumentNode.SelectSingleNode("//*[@id='eContainer']/section[4]/table/tr/td[2]/table[1]/tbody/tr/td") != null)
                MonthlyDamage = Convert.ToInt64(egov.DocumentNode.SelectSingleNode("//*[@id='eContainer']/section[4]/table/tr/td[2]/table[1]/tbody/tr/td").InnerText.Trim().Replace(",", ""));
            else
                MonthlyDamage = 0;
            #endregion
            #region BombsUsed
            try
            {
                BombsUsed = new CitizenBombsUsed(
                    Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//div[@class='citizen_mass_destruction']/strong[1]/b").InnerText),
                    Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//div[@class='citizen_mass_destruction']/strong[2]/b").InnerText),
                    doc.DocumentNode.SelectSingleNode("//div[@class='citizen_mass_destruction']/em").ChildNodes[2].InnerText.Trim());
            }
            catch
            {
                BombsUsed = new CitizenBombsUsed(0, 0);
            }
            #endregion
            #region Decorations
            //try
            //{
            for (int i = 0; i < doc.DocumentNode.SelectNodes("//ul[@id='new_achievements']/li").Count; i++)
                if (doc.DocumentNode.SelectSingleNode("//ul[@id='new_achievements']/li[" + (i + 1) + "]").Attributes["class"].Value != "unknown")
                {
                    Array.Resize(ref decoration, Decoration.Length + 1);
                    Decoration[i] = new Decoration(
                        doc.DocumentNode.SelectSingleNode("//ul[@id='new_achievements']/li[" + (i + 1) + "]/img").Attributes["src"].Value,
                        doc.DocumentNode.SelectSingleNode("//ul[@id='new_achievements']/li[" + (i + 1) + "]/div/span/p").InnerText.Trim());

                    if (doc.DocumentNode.SelectSingleNode("//ul[@id='new_achievements']/li[" + (i + 1) + "]/em") != null)
                        Decoration[i].Count = Convert.ToInt16(doc.DocumentNode.SelectSingleNode("//ul[@id='new_achievements']/li[" + (i + 1) + "]/em").InnerText);

                    //MessageBox.Show(
                    //    Decoration[i].ImageLink + Environment.NewLine +
                    //    Decoration[i].Text + Environment.NewLine +
                    //    Decoration[i].Count);
                }
            //}
            //catch
            //{
            //}
            #endregion
            #region Medals
            string[] medalCodeNames = new string[]
            {
                "Freedom Fighter", "hard worker", "congressman", "president",
                "media mogul", "battle hero", "campaign hero", "resistance hero",
                "super soldier", "society builder", "mercenary", "Top Fighter",
                "true patriot",
            };

            for (int i = 0; i < medalCodeNames.Length; i++)
                if (doc.DocumentNode.SelectSingleNode(".//ul[@id='achievment']/li[img[@alt='" + medalCodeNames[i] + "']]/div[@class='counter']") != null)
                    Medals.Medal[i] = Convert.ToInt16(doc.DocumentNode.SelectSingleNode(".//ul[@id='achievment']/li[img[@alt='" + medalCodeNames[i] + "']]/div[@class='counter']").InnerText);
            #endregion
            #region WorldRankings
            try
            {
                WorldRanking = new CitizenRanking(
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[1]/td[2]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[1]/td[3]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[1]/td[4]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[1]/td[5]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[1]/td[6]").InnerText.RemoveNonDigits()));
            }
            catch
            {
                WorldRanking = new CitizenRanking(0, 0, 0, 0, 0);
            }
            #endregion
            #region CountryRankings
            try
            {
            CountryRanking = new CitizenRanking(
                Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td[2]").InnerText.RemoveNonDigits()),
                Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td[3]").InnerText.RemoveNonDigits()),
                Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td[4]").InnerText.RemoveNonDigits()),
                Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td[5]").InnerText.RemoveNonDigits()),
                Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[2]/td[6]").InnerText.RemoveNonDigits()));
            }
            catch
            {
                CountryRanking = new CitizenRanking(0, 0, 0, 0, 0);
            }
            #endregion
            #region UnitRankings
            try
            {
                UnitRanking = new CitizenRanking(
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[3]/td[2]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[3]/td[3]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[3]/td[4]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[3]/td[5]").InnerText.RemoveNonDigits()),
                    Convert.ToInt32(egov.DocumentNode.SelectSingleNode("//section[@id='eContainer']/section[4]/table/tr/td[1]/table[1]/tbody/tr/td/table/tbody/tr[3]/td[6]").InnerText.RemoveNonDigits()));
            }
            catch
            {
                UnitRanking = new CitizenRanking(0, 0, 0, 0, 0);
            }
            #endregion
            #region GuerillaScore
            GuerillaScore = new GuerillaScore(
                Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//div[@class='guerilla_fights won']").InnerText),
                Convert.ToInt32(doc.DocumentNode.SelectSingleNode("//div[@class='guerilla_fights lost']").InnerText));
            #endregion

            Influence = (long)(RankPoints - OldStatistics.RankPoints) * 10;

            //if (OldStatistics.BombsUsed.Small < BombsUsed.Small)
            //    switch(Division)
            //    {
            //        case 1:
            //            Influence += 75000 * (BombsUsed.Small - OldStatistics.BombsUsed.Small);
            //            break;
            //        case 2:
            //            Influence += 375000 * (BombsUsed.Small - OldStatistics.BombsUsed.Small);
            //            break;
            //        case 3:
            //            Influence += 750000 * (BombsUsed.Small - OldStatistics.BombsUsed.Small);
            //            break;
            //        case 4:
            //            Influence += 1500000 * (BombsUsed.Small - OldStatistics.BombsUsed.Small);
            //            break;
            //    }

            //if (OldStatistics.BombsUsed.Big < BombsUsed.Big)
            //    Influence += (BombsUsed.Big - OldStatistics.BombsUsed.Big) * 5000000;

            StrengthGained = Strength - OldStatistics.Strength;
            RankPointsGained = (int)(RankPoints - OldStatistics.RankPoints);
            ExperienceGained = Experience - OldStatistics.Experience;

            if (Rank != OldStatistics.Rank)
                RankUp = OldStatistics.Rank + " → " + Rank;
            else
                RankUp = "";

            if (Level != OldStatistics.Level)
                LevelUp = OldStatistics.Level + " → " + Level;
            else
                LevelUp = "";

            for (int i = 0; i < MedalsGained.Medal.Length; i++)
                MedalsGained.Medal[i] = Medals.Medal[i] - OldStatistics.Medals.Medal[i];

            if (ID == 2972052)
            {
                Name = "Mlendea Horațiu";
                BirthDay = new CitizenBirthDay(new DateTime(2011, 8, 21));
                Residence = new CitizenResidence("Romania", "Crisana");
            }
        }

        public int GetHitDamage(int weaponQuality)
        {
            int firepower = 0;

            switch (weaponQuality)
            {
                case 0:
                    firepower = 0;
                    break;
                case 1:
                    firepower = 20;
                    break;
                case 2:
                    firepower = 40;
                    break;
                case 3:
                    firepower = 60;
                    break;
                case 4:
                    firepower = 80;
                    break;
                case 5:
                    firepower = 100;
                    break;
                case 6:
                    firepower = 120;
                    break;
                case 7:
                    firepower = 200;
                    break;
            }

            return (int)(10 * (1 + (float)Strength / 400) * (1 + (float)RankValue / 5) * (1 + (float)firepower / 100));
        }
    }
}
