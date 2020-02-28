using System;
using System.Collections.Generic;
using System.Text;

namespace eRepublik.Citizen.Achievements
{
    public class Medals
    {
        public int FreedomFighter { get { return Medal[0]; } set { Medal[0] = value; } }
        public int HardWorker { get { return Medal[1]; } set { Medal[1] = value; } }
        public int CongressMember { get { return Medal[2]; } set { Medal[2] = value; } }
        public int CountryPresident { get { return Medal[3]; } set { Medal[3] = value; } }
        public int MediaMogul { get { return Medal[4]; } set { Medal[4] = value; } }
        public int BattleHero { get { return Medal[5]; } set { Medal[5] = value; } }
        public int CampaignHero { get { return Medal[6]; } set { Medal[6] = value; } }
        public int ResistanceHero { get { return Medal[7]; } set { Medal[7] = value; } }
        public int SuperSoldier { get { return Medal[8]; } set { Medal[8] = value; } }
        public int SocietyBuilder { get { return Medal[9]; } set { Medal[9] = value; } }
        public int Mercenary { get { return Medal[10]; } set { Medal[10] = value; } }
        public int TopFighter { get { return Medal[11]; } set { Medal[11] = value; } }
        public int TruePatriot { get { return Medal[12]; } set { Medal[12] = value; } }
        public int Count
        {
            get
            {
                return
                    HardWorker +
                    CongressMember +
                    CountryPresident +
                    MediaMogul +
                    BattleHero +
                    CampaignHero +
                    ResistanceHero +
                    SuperSoldier +
                    SocietyBuilder +
                    TopFighter +
                    TruePatriot;
            }
        }
        public int[] Medal = new int[13];
        public int Total
        {
            get
            {
                int total = 0;

                for (int i = 0; i < Medal.Length; i++)
                    total += Medal[i];

                return total;
            }
        }

        public Medals Clone()
        {
            Medals mdl = new Medals();

            for (int i = 0; i < Medal.Length; i++)
                mdl.Medal[i] = Medal[i];

            return mdl;
        }
    }
}
