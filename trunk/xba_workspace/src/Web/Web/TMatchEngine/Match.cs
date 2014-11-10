namespace Web.TMatchEngine
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Text;
    using Web.DBData;
    using Web.Helper;

    public class Match
    {
        private Arrange[] aAways = new Arrange[7];
        private Arrange[] aHomes = new Arrange[7];
        private bool blnCanAddA;
        private bool blnCanAddH;
        private bool blnCanPlay;
        private bool blnPower;
        private bool blnTTurn = true;
        private bool blUseStaffA;
        private bool blUseStaffH;
        private byte byAllAddA;
        private byte byAllAddH;
        private int intAbilitySumA;
        private int intAbilitySumH;
        private int[] intADefs = new int[6];
        private int[] intAOffs = new int[6];
        private int[] intHDefs = new int[6];
        private int[] intHOffs = new int[6];
        private int intLoseAbility;
        private int intMaxAbility;
        private int intMVPValue;
        private int intPlayedCountA;
        private int intPlayedCountH;
        private int intRand;
        private int intRand1;
        private int intTag;
        private int intTempMVPValue;
        private int intType;
        private int intWinAbility;
        public Player pMVP = new Player();
        private Random rnd = new Random(DateTime.Now.Millisecond);
        private StringBuilder sbArrange = new StringBuilder();
        private StringBuilder sbClub = new StringBuilder();
        private StringBuilder sbIntro = new StringBuilder();
        private StringBuilder sbPlayer = new StringBuilder();
        private StringBuilder sbQuarter = new StringBuilder();
        public StringBuilder sbRepURL = new StringBuilder();
        public StringBuilder sbRepXml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Report>");
        private StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbStasURL = new StringBuilder();
        public StringBuilder sbStasXml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Stas>");
        private Team tAway;
        private Team tHome;

        public Match(int intClubIDH, int intClubIDA, int intTag)
        {
            this.intTag = intTag;
            this.tHome = new Team(intClubIDH, true);
            this.tAway = new Team(intClubIDA, false);
            this.blnPower = false;
            if (this.tHome.players.Count >= 6)
            {
                this.blnCanPlay = true;
                string str = "1|2|3|4|4|4|4";
                string[] strArray = new string[7];
                strArray = str.Split(new char[] { '|' });
                for (int i = 0; i < 7; i++)
                {
                    if (Convert.ToInt32(strArray[i]) > 0)
                    {
                        if (i == 5)
                        {
                            this.tHome.intWUse = 0;
                        }
                        if (i == 6)
                        {
                            this.tHome.intLUse = 0;
                        }
                        this.aHomes[i] = new Arrange(Convert.ToInt32(strArray[i]), this.tHome, this.intType);
                        if (this.aHomes[i].intOffense == 1)
                        {
                            this.intHOffs[0]++;
                        }
                        else if (this.aHomes[i].intOffense == 2)
                        {
                            this.intHOffs[1]++;
                        }
                        else if (this.aHomes[i].intOffense == 3)
                        {
                            this.intHOffs[2]++;
                        }
                        else if (this.aHomes[i].intOffense == 4)
                        {
                            this.intHOffs[3]++;
                        }
                        else if (this.aHomes[i].intOffense == 5)
                        {
                            this.intHOffs[4]++;
                        }
                        else if (this.aHomes[i].intOffense == 6)
                        {
                            this.intHOffs[5]++;
                        }
                        if (this.aHomes[i].intDefense == 1)
                        {
                            this.intHDefs[0]++;
                        }
                        else if (this.aHomes[i].intDefense == 2)
                        {
                            this.intHDefs[1]++;
                        }
                        else if (this.aHomes[i].intDefense == 3)
                        {
                            this.intHDefs[2]++;
                        }
                        else if (this.aHomes[i].intDefense == 4)
                        {
                            this.intHDefs[3]++;
                        }
                        else if (this.aHomes[i].intDefense == 5)
                        {
                            this.intHDefs[4]++;
                        }
                        else if (this.aHomes[i].intDefense == 6)
                        {
                            this.intHDefs[5]++;
                        }
                    }
                }
                string str2 = "5|6|7|8|8|8|8";
                string[] strArray2 = new string[7];
                strArray2 = str2.Split(new char[] { '|' });
                for (int j = 0; j < 7; j++)
                {
                    if (Convert.ToInt32(strArray2[j]) > 0)
                    {
                        if (j == 5)
                        {
                            this.tAway.intWUse = 0;
                        }
                        if (j == 6)
                        {
                            this.tAway.intLUse = 0;
                        }
                        this.aAways[j] = new Arrange(Convert.ToInt32(strArray2[j]), this.tAway, this.intType);
                        if (this.aAways[j].intOffense == 1)
                        {
                            this.intAOffs[0]++;
                        }
                        else if (this.aAways[j].intOffense == 2)
                        {
                            this.intAOffs[1]++;
                        }
                        else if (this.aAways[j].intOffense == 3)
                        {
                            this.intAOffs[2]++;
                        }
                        else if (this.aAways[j].intOffense == 4)
                        {
                            this.intAOffs[3]++;
                        }
                        else if (this.aAways[j].intOffense == 5)
                        {
                            this.intAOffs[4]++;
                        }
                        else if (this.aAways[j].intOffense == 6)
                        {
                            this.intAOffs[5]++;
                        }
                        if (this.aAways[j].intDefense == 1)
                        {
                            this.intADefs[0]++;
                        }
                        else if (this.aAways[j].intDefense == 2)
                        {
                            this.intADefs[1]++;
                        }
                        else if (this.aAways[j].intDefense == 3)
                        {
                            this.intADefs[2]++;
                        }
                        else if (this.aAways[j].intDefense == 4)
                        {
                            this.intADefs[3]++;
                        }
                        else if (this.aAways[j].intDefense == 5)
                        {
                            this.intADefs[4]++;
                        }
                        else if (this.aAways[j].intDefense == 6)
                        {
                            this.intADefs[5]++;
                        }
                    }
                }
            }
        }

        public void Finished()
        {
            Player player;
            this.sbClub.Append("<Club ClubID=\"");
            this.sbClub.Append(this.tHome.intClubID);
            this.sbClub.Append("\">");
            this.sbClub.Append("\t<Type>1</Type>");
            this.sbClub.Append("\t<ClubName>");
            this.sbClub.Append(this.tHome.strClubName);
            this.sbClub.Append("</ClubName>");
            this.sbClub.Append("\t<Logo>");
            this.sbClub.Append(BTPClubManager.GetClubLogo(this.tHome.intClubID));
            this.sbClub.Append("</Logo>");
            this.sbClub.Append("\t<Score>");
            if ((this.byAllAddH > 0) && (this.tHome.intScore > 20))
            {
                int num = ((this.tHome.intScore * 100) / (100 + (2 * this.byAllAddH))) - this.rnd.Next(5);
                this.sbClub.Append(this.tHome.intScore.ToString() + "[" + num.ToString().Trim() + "]");
            }
            else
            {
                this.sbClub.Append(this.tHome.intScore.ToString());
            }
            this.sbClub.Append("</Score>");
            this.sbClub.Append("<AllAdd>");
            this.sbClub.Append(this.byAllAddH);
            this.sbClub.Append("</AllAdd>");
            this.sbClub.Append("</Club>");
            this.sbClub.Append("<Club ClubID=\"");
            this.sbClub.Append(this.tAway.intClubID);
            this.sbClub.Append("\">");
            this.sbClub.Append("\t<Type>2</Type>");
            this.sbClub.Append("\t<ClubName>");
            this.sbClub.Append(this.tAway.strClubName);
            this.sbClub.Append("</ClubName>");
            this.sbClub.Append("\t<Logo>");
            this.sbClub.Append(BTPClubManager.GetClubLogo(this.tAway.intClubID));
            this.sbClub.Append("</Logo>");
            this.sbClub.Append("\t<Score>");
            if ((this.byAllAddA > 0) && (this.tAway.intScore > 20))
            {
                int num2 = ((this.tAway.intScore * 100) / (100 + (2 * this.byAllAddA))) - this.rnd.Next(5);
                this.sbClub.Append(this.tAway.intScore.ToString() + "[" + num2.ToString().Trim() + "]");
            }
            else
            {
                this.sbClub.Append(this.tAway.intScore.ToString());
            }
            this.sbClub.Append("</Score>");
            this.sbClub.Append("<AllAdd>");
            this.sbClub.Append(this.byAllAddA);
            this.sbClub.Append("</AllAdd>");
            this.sbClub.Append("</Club>");
            this.sbRepXml.Append(this.sbClub.ToString());
            this.sbRepXml.Append(this.sbQuarter.ToString());
            this.sbRepXml.Append(this.sbArrange.ToString());
            this.sbRepXml.Append(this.sbPlayer.ToString());
            this.sbRepXml.Append(this.sbScript.ToString());
            this.sbPlayer = new StringBuilder();
            bool flag = false;
            if (this.tHome.intScore > this.tAway.intScore)
            {
                flag = true;
            }
            bool flag2 = false;
            bool flag3 = false;
            int trainPointMultiple = Config.GetTrainPointMultiple();
            IDictionaryEnumerator enumerator = this.tHome.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                player = (Player) enumerator.Value;
                this.sbPlayer.Append("<Player PlayerID=\"");
                this.sbPlayer.Append(player.longPlayerID);
                this.sbPlayer.Append("\">");
                this.sbPlayer.Append("\t<Name>");
                this.sbPlayer.Append(player.strName);
                this.sbPlayer.Append("</Name>");
                this.sbPlayer.Append("\t<ClubID>");
                this.sbPlayer.Append(this.tHome.intClubID);
                this.sbPlayer.Append("</ClubID>");
                this.sbPlayer.Append("\t<Number>");
                this.sbPlayer.Append(player.intNumber);
                this.sbPlayer.Append("</Number>");
                this.sbPlayer.Append("\t<Pos>");
                this.sbPlayer.Append(player.intPos);
                this.sbPlayer.Append("</Pos>");
                this.sbPlayer.Append("\t<FG>");
                this.sbPlayer.Append(player.intFG);
                this.sbPlayer.Append("</FG>");
                this.sbPlayer.Append("\t<FGs>");
                this.sbPlayer.Append(player.intFGs);
                this.sbPlayer.Append("</FGs>");
                this.sbPlayer.Append("\t<FT>");
                this.sbPlayer.Append(player.intFT);
                this.sbPlayer.Append("</FT>");
                this.sbPlayer.Append("\t<FTs>");
                this.sbPlayer.Append(player.intFTs);
                this.sbPlayer.Append("</FTs>");
                this.sbPlayer.Append("\t<ThreeP>");
                this.sbPlayer.Append(player.int3P);
                this.sbPlayer.Append("</ThreeP>");
                this.sbPlayer.Append("\t<ThreePs>");
                this.sbPlayer.Append(player.int3Ps);
                this.sbPlayer.Append("</ThreePs>");
                this.sbPlayer.Append("\t<To>");
                this.sbPlayer.Append(player.intTo);
                this.sbPlayer.Append("</To>");
                this.sbPlayer.Append("\t<Score>");
                this.sbPlayer.Append(player.intScore);
                this.sbPlayer.Append("</Score>");
                this.sbPlayer.Append("\t<OReb>");
                this.sbPlayer.Append(player.intOReb);
                this.sbPlayer.Append("</OReb>");
                this.sbPlayer.Append("\t<DReb>");
                this.sbPlayer.Append(player.intDReb);
                this.sbPlayer.Append("</DReb>");
                this.sbPlayer.Append("\t<Ast>");
                this.sbPlayer.Append(player.intAst);
                this.sbPlayer.Append("</Ast>");
                this.sbPlayer.Append("\t<Stl>");
                this.sbPlayer.Append(player.intStl);
                this.sbPlayer.Append("</Stl>");
                this.sbPlayer.Append("\t<Blk>");
                this.sbPlayer.Append(player.intBlk);
                this.sbPlayer.Append("</Blk>");
                this.sbPlayer.Append("\t<Foul>");
                this.sbPlayer.Append(player.intFoul);
                this.sbPlayer.Append("</Foul>");
                this.sbPlayer.Append("</Player>");
                if (this.blnPower)
                {
                    if (flag)
                    {
                        if (player.blnPlayed)
                        {
                            player.intHappy += this.rnd.Next(2, 5);
                        }
                        else
                        {
                            player.intHappy += this.rnd.Next(1, 4);
                        }
                        if (player.intHappy > 100)
                        {
                            player.intHappy = 100;
                        }
                    }
                    else
                    {
                        if (player.blnPlayed)
                        {
                            player.intHappy -= this.rnd.Next(4, 7);
                        }
                        else
                        {
                            player.intHappy -= this.rnd.Next(5, 8);
                        }
                        if (player.intHappy < 1)
                        {
                            player.intHappy = 1;
                        }
                    }
                    if (this.intType != 7)
                    {
                        int num4 = 0;
                        if (((this.intType == 2) || (this.intType == 10)) || (this.intType == 11))
                        {
                            if (player.blnPlayed)
                            {
                                num4 = ((((((((((player.intScore + (player.intAst * 2)) + (player.intOReb * 2)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * player.intSkillPotential) / 9) + ((30 - player.intAge) * 0x7d0)) + ((player.intSkillPotential * 0x73) / 10)) / 80) + (((((0x21 - player.intAge) * 4) * ((player.intAge / 3) + 0x27)) / 3) / 1);
                                if (num4 < 0x4b0)
                                {
                                    num4 = ((this.rnd.Next(0, 100) + 800) * 150) / 100;
                                    if (player.intAge > 30)
                                    {
                                        num4 = (num4 * (15 - (player.intAge - 30))) / 15;
                                    }
                                }
                            }
                            else
                            {
                                num4 = ((this.rnd.Next(0, 100) + 600) * 150) / 100;
                                if (player.intAge > 30)
                                {
                                    num4 = (num4 * (15 - (player.intAge - 30))) / 15;
                                }
                            }
                            if ((this.intType == 10) || (this.intType == 11))
                            {
                                double num5 = Config.GetPlayer5TrainPointMultiple();
                                num4 = (int) (num4 * num5);
                            }
                        }
                        if (flag2)
                        {
                            num4 *= 2;
                        }
                        num4 *= trainPointMultiple;
                        if ((this.intType != 2) && ((this.intType == 10) || (this.intType == 11)))
                        {
                            num4 = (num4 * 8) / 100;
                        }
                    }
                }
                if (flag)
                {
                    this.intTempMVPValue = 0;
                    this.intTempMVPValue = ((((player.intScore * 12) + ((player.intOReb + player.intDReb) * 20)) + (player.intAst * 0x17)) + (player.intBlk * 0x2b)) + (player.intStl * 0x23);
                    if (this.intTempMVPValue >= this.intMVPValue)
                    {
                        this.intMVPValue = this.intTempMVPValue;
                        this.pMVP = player;
                    }
                    if (player.intAbility > this.intMaxAbility)
                    {
                        this.intMaxAbility = player.intAbility;
                    }
                }
                if (player.blnPlayed)
                {
                    this.intPlayedCountH++;
                    this.intAbilitySumH += player.intAbility;
                }
            }
            enumerator = this.tAway.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                player = (Player) enumerator.Value;
                this.sbPlayer.Append("<Player PlayerID=\"");
                this.sbPlayer.Append(player.longPlayerID);
                this.sbPlayer.Append("\">");
                this.sbPlayer.Append("\t<Name>");
                this.sbPlayer.Append(player.strName);
                this.sbPlayer.Append("</Name>");
                this.sbPlayer.Append("\t<ClubID>");
                this.sbPlayer.Append(this.tAway.intClubID);
                this.sbPlayer.Append("</ClubID>");
                this.sbPlayer.Append("\t<Number>");
                this.sbPlayer.Append(player.intNumber);
                this.sbPlayer.Append("</Number>");
                this.sbPlayer.Append("\t<Pos>");
                this.sbPlayer.Append(player.intPos);
                this.sbPlayer.Append("</Pos>");
                this.sbPlayer.Append("\t<FG>");
                this.sbPlayer.Append(player.intFG);
                this.sbPlayer.Append("</FG>");
                this.sbPlayer.Append("\t<FGs>");
                this.sbPlayer.Append(player.intFGs);
                this.sbPlayer.Append("</FGs>");
                this.sbPlayer.Append("\t<FT>");
                this.sbPlayer.Append(player.intFT);
                this.sbPlayer.Append("</FT>");
                this.sbPlayer.Append("\t<FTs>");
                this.sbPlayer.Append(player.intFTs);
                this.sbPlayer.Append("</FTs>");
                this.sbPlayer.Append("\t<ThreeP>");
                this.sbPlayer.Append(player.int3P);
                this.sbPlayer.Append("</ThreeP>");
                this.sbPlayer.Append("\t<ThreePs>");
                this.sbPlayer.Append(player.int3Ps);
                this.sbPlayer.Append("</ThreePs>");
                this.sbPlayer.Append("\t<To>");
                this.sbPlayer.Append(player.intTo);
                this.sbPlayer.Append("</To>");
                this.sbPlayer.Append("\t<Score>");
                this.sbPlayer.Append(player.intScore);
                this.sbPlayer.Append("</Score>");
                this.sbPlayer.Append("\t<OReb>");
                this.sbPlayer.Append(player.intOReb);
                this.sbPlayer.Append("</OReb>");
                this.sbPlayer.Append("\t<DReb>");
                this.sbPlayer.Append(player.intDReb);
                this.sbPlayer.Append("</DReb>");
                this.sbPlayer.Append("\t<Ast>");
                this.sbPlayer.Append(player.intAst);
                this.sbPlayer.Append("</Ast>");
                this.sbPlayer.Append("\t<Stl>");
                this.sbPlayer.Append(player.intStl);
                this.sbPlayer.Append("</Stl>");
                this.sbPlayer.Append("\t<Blk>");
                this.sbPlayer.Append(player.intBlk);
                this.sbPlayer.Append("</Blk>");
                this.sbPlayer.Append("\t<Foul>");
                this.sbPlayer.Append(player.intFoul);
                this.sbPlayer.Append("</Foul>");
                this.sbPlayer.Append("</Player>");
                if (this.blnPower)
                {
                    if (!flag)
                    {
                        if (player.blnPlayed)
                        {
                            player.intHappy += this.rnd.Next(2, 5);
                        }
                        else
                        {
                            player.intHappy += this.rnd.Next(1, 4);
                        }
                        if (player.intHappy > 100)
                        {
                            player.intHappy = 100;
                        }
                    }
                    else
                    {
                        if (player.blnPlayed)
                        {
                            player.intHappy -= this.rnd.Next(4, 7);
                        }
                        else
                        {
                            player.intHappy -= this.rnd.Next(5, 8);
                        }
                        if (player.intHappy < 1)
                        {
                            player.intHappy = 1;
                        }
                    }
                    if (player.blnInjured)
                    {
                        InjuryGenerator generator = new InjuryGenerator(player.intPower);
                        generator.SetEvent();
                        int intSuspend = generator.intSuspend;
                        string strEvent = generator.strEvent;
                        PlayerItem.ChangePlayerFromArrange5(player.longPlayerID, this.tAway.intClubID);
                    }
                    if (this.intType != 7)
                    {
                        int num6 = 0;
                        if (((this.intType == 2) || (this.intType == 10)) || (this.intType == 11))
                        {
                            if (player.blnPlayed)
                            {
                                num6 = ((((((((((player.intScore + (player.intAst * 2)) + (player.intOReb * 2)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * player.intSkillPotential) / 9) + ((30 - player.intAge) * 0x7d0)) + ((player.intSkillPotential * 0x73) / 10)) / 80) + (((((0x21 - player.intAge) * 4) * ((player.intAge / 3) + 0x27)) / 3) / 1);
                                if (num6 < 0x4b0)
                                {
                                    num6 = ((this.rnd.Next(0, 100) + 800) * 150) / 100;
                                    if (player.intAge > 30)
                                    {
                                        num6 = (num6 * (15 - (player.intAge - 30))) / 15;
                                    }
                                }
                            }
                            else
                            {
                                num6 = ((this.rnd.Next(0, 100) + 600) * 150) / 100;
                                if (player.intAge > 30)
                                {
                                    num6 = (num6 * (15 - (player.intAge - 30))) / 15;
                                }
                            }
                        }
                        if (flag3)
                        {
                            num6 *= 2;
                        }
                        num6 *= trainPointMultiple;
                        if ((this.intType != 2) && ((this.intType == 10) || (this.intType == 11)))
                        {
                            num6 = (num6 * 8) / 100;
                        }
                    }
                }
                if (!flag)
                {
                    this.intTempMVPValue = 0;
                    this.intTempMVPValue = ((((player.intScore * 12) + ((player.intOReb + player.intDReb) * 20)) + (player.intAst * 0x17)) + (player.intBlk * 0x2b)) + (player.intStl * 0x23);
                    if (this.intTempMVPValue >= this.intMVPValue)
                    {
                        this.intMVPValue = this.intTempMVPValue;
                        this.pMVP = player;
                    }
                    if (player.intAbility > this.intMaxAbility)
                    {
                        this.intMaxAbility = player.intAbility;
                    }
                }
                if (player.blnPlayed)
                {
                    this.intPlayedCountA++;
                    this.intAbilitySumA += player.intAbility;
                }
            }
            player = null;
            this.sbStasXml.Append(this.sbClub.ToString());
            this.sbStasXml.Append(this.sbPlayer.ToString());
            this.sbIntro.Append("<Intro><Intro>");
            Quarter quarter = new Quarter();
            int num7 = ((quarter.GetMethodAddForMatch(this.aAways[0].intOffense, this.aHomes[0].intDefense) + quarter.GetMethodAddForMatch(this.aAways[1].intOffense, this.aHomes[1].intDefense)) + quarter.GetMethodAddForMatch(this.aAways[2].intOffense, this.aHomes[2].intDefense)) + quarter.GetMethodAddForMatch(this.aAways[3].intOffense, this.aHomes[3].intDefense);
            int num8 = ((quarter.GetMethodAddForMatch(this.aHomes[0].intOffense, this.aAways[0].intDefense) + quarter.GetMethodAddForMatch(this.aHomes[1].intOffense, this.aAways[1].intDefense)) + quarter.GetMethodAddForMatch(this.aHomes[2].intOffense, this.aAways[2].intDefense)) + quarter.GetMethodAddForMatch(this.aHomes[3].intOffense, this.aAways[3].intDefense);
            if (flag)
            {
                this.intWinAbility = this.intAbilitySumH / this.intPlayedCountH;
                this.intLoseAbility = this.intAbilitySumA / this.intPlayedCountA;
                if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了经理谨慎的赛前工作,在球队的整体配合与战术的合理安排下," + this.pMVP.strName + "发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "在队友的大力支持与战术的合理安排下,发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbility)) && ((num7 <= num8) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然在战术上没有占得优势,但是由于精心的赛前准备和全队的卓越实力,比赛最终获胜." + this.pMVP.strName + "获得了本场的MVP!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("全队成员在合理的战术安排下赢得了本场比赛," + this.pMVP.strName + "获得了本场的MVP."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("队员们受到经理勤奋的赛前工作的鼓舞,士气大振!超常发挥!赢得了本场比赛的胜利.作为球队核心的" + this.pMVP.strName + "勇夺MVP桂冠!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛没有过多的赛前工作,完全靠球员自身的强大能力获得胜利.这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("尽管赛前部署很周密,但是却没有换得战术的优势,凭借球员自身的能力赢得了胜利." + this.pMVP.strName + "在队友的支持下成为了本场的MVP!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("比赛胜利的关键是经理精心的赛前工作和战术的合理利用,全队打出了120%的实力." + this.pMVP.strName + "成为球队中发挥最好的球员."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然战术设计不占有优势,但是经理细致的赛前工作使得" + this.pMVP.strName + "能发挥出应有水平,成为本场MVP."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员能力又高,又有战术上的优势,获胜应该是理所当然的.观众对于" + this.pMVP.strName + "的超常表现非常满意."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在战术安排得当的情况下," + this.pMVP.strName + "正常发挥出自己的水平,带领球队获得最后的胜利.理所当然的得到了本场的MVP贵冠."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("由于经理费尽心力做好了优秀的赛前工作,才领导球队战胜强敌,本场的MVP不应该属于" + this.pMVP.strName + ",而应属于尽职的经理!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球队在战术上稍占优势,球员们借此拼力一搏,顽强的获得了胜利," + this.pMVP.strName + "能得到MVP不是他一个人的功劳."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在没有任何优势的情况下," + this.pMVP.strName + "力挽狂澜,创造了本场的一个奇迹!他是本场的天皇巨星!永远的MVP!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场的胜利完全靠球员们综合实力,既没有战术的优势也没有赛前的鼓舞,观众们对" + this.pMVP.strName + "夺走了MVP表示非常惊讶!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员爆发出了非凡的韧性和潜力,在逆境之下创造了奇迹!" + this.pMVP.strName + "表现的非常出色!"));
                }
                else
                {
                    this.sbIntro.Append(Language.HighLightRed("经过球员们的不懈努力，球队获得了最终的胜利 "));
                }
            }
            else
            {
                this.intWinAbility = this.intAbilitySumA / this.intPlayedCountA;
                this.intLoseAbility = this.intAbilitySumH / this.intPlayedCountH;
                if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了经理谨慎的赛前工作,在球队的整体配合与战术的合理安排下," + this.pMVP.strName + "发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "在队友的大力支持与战术的合理安排下,发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbility)) && ((num7 >= num8) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然在战术上没有占得优势,但是由于精心的赛前准备和全队的卓越实力,比赛最终获胜." + this.pMVP.strName + "获得了本场的MVP!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("全队成员在战术的合理安排下赢得了本场的MVP," + this.pMVP.strName + "获得了本场的MVP."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("队员们受到经理勤奋的赛前工作的鼓舞,士气大振!超常发挥!赢得了本场比赛的胜利.作为球队核心的" + this.pMVP.strName + "勇夺MVP桂冠!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛没有过多的赛前工作,完全靠球员自身的强大能力获得胜利.这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("尽管赛前部署很周密,但是却没有换得战术的优势,凭借球员自身的能力赢得了胜利." + this.pMVP.strName + "在队友的支持下成为了本场的MVP!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("比赛胜利的关键是经理精心的赛前工作和战术的合理利用,全队打出了120%的实力." + this.pMVP.strName + "成为球队中发挥最好的球员."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然战术设计不占有优势,但是经理细致的赛前工作使得" + this.pMVP.strName + "能发挥出应有水平,成为本场MVP."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员能力又高,又有战术上的优势,获胜应该是理所当然的.观众对于" + this.pMVP.strName + "的超常表现非常满意."));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在战术安排得当的情况下," + this.pMVP.strName + "正常发挥出自己的水平,带领球队获得最后的胜利.理所当然的得到了本场的MVP贵冠."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("由于经理费尽心力做好了优秀的赛前工作,才领导球队战胜强敌,本场的MVP不应该属于" + this.pMVP.strName + ",而应属于尽职的经理!"));
                }
                else if (((num7 <= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球队在战术上稍占优势,球员们借此拼力一搏,顽强的获得了胜利," + this.pMVP.strName + "能得到MVP不是他一个人的功劳."));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在没有任何优势的情况下," + this.pMVP.strName + "力挽狂澜,创造了本场的一个奇迹!他是本场的天皇巨星!永远的MVP!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场的胜利完全靠球员们综合实力,既没有战术的优势也没有赛前的鼓舞,观众们对" + this.pMVP.strName + "夺走了MVP表示非常惊讶!"));
                }
                else if (((num7 >= num8) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员爆发出了非凡的韧性和潜力,在逆境之下创造了奇迹!" + this.pMVP.strName + "表现的非常出色!"));
                }
                else
                {
                    this.sbIntro.Append(Language.HighLightRed("经过球员们的不懈努力，球队获得了最终的胜利 "));
                }
            }
            this.sbIntro.Append("</Intro></Intro>");
            this.sbRepXml.Append(this.sbIntro.ToString());
            this.sbRepXml.Append("</Report>");
            this.sbStasXml.Append(this.sbIntro.ToString());
            this.sbStasXml.Append("</Stas>");
            if (this.intType <= 9)
            {
                string str = StringItem.FormatDate(DateTime.Now, "yyyyMMdd");
                string path = Path.GetFullPath("" + MatchItem.GetMatchPath() + @"\MatchXML\TRep\") + str + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str3 = string.Concat(new object[] { path, "Rep", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbRepURL.Append("MatchXML/TRep/");
                this.sbRepURL.Append(str);
                this.sbRepURL.Append("/Rep");
                this.sbRepURL.Append(this.tHome.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.tAway.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intType);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intTag);
                this.sbRepURL.Append(".xml");
                if (File.Exists(str3))
                {
                    File.Delete(str3);
                }
                using (StreamWriter writer = File.CreateText(str3))
                {
                    writer.Write(this.sbRepXml.ToString());
                }
                path = Path.GetFullPath("" + MatchItem.GetMatchPath() + @"\MatchXML\TStas\") + str + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                str3 = string.Concat(new object[] { path, "Stas", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbStasURL.Append("MatchXML/TStas/");
                this.sbStasURL.Append(str);
                this.sbStasURL.Append("/Stas");
                this.sbStasURL.Append(this.tHome.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.tAway.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intType);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intTag);
                this.sbStasURL.Append(".xml");
                if (File.Exists(str3))
                {
                    File.Delete(str3);
                }
                using (StreamWriter writer2 = File.CreateText(str3))
                {
                    writer2.Write(this.sbStasXml.ToString());
                }
            }
        }

        public int GetAwayScore()
        {
            return this.tAway.intScore;
        }

        public int GetHomeScore()
        {
            return this.tHome.intScore;
        }

        public void Run()
        {
            if (this.blnCanPlay)
            {
                int intQNum = 1;
                while (true)
                {
                    if (intQNum <= 4)
                    {
                        Quarter quarter = new Quarter(12, intQNum, this.tHome, this.tAway, this.aHomes, this.aAways, this.blnPower, this.byAllAddH, this.byAllAddA, this.intType) {
                            blnTTurn = this.blnTTurn
                        };
                        quarter.Run();
                        this.sbQuarter.Append(quarter.sbQuarter.ToString());
                        this.sbArrange.Append(quarter.sbArrange.ToString());
                        this.sbPlayer.Append(quarter.sbPlayer.ToString());
                        this.sbScript.Append(quarter.sbScript.ToString());
                        this.tHome = quarter.tHome;
                        this.tAway = quarter.tAway;
                        this.blnTTurn = quarter.blnTTurn;
                    }
                    else
                    {
                        if (this.tHome.intScore != this.tAway.intScore)
                        {
                            this.Finished();
                            return;
                        }
                        Quarter quarter2 = new Quarter(5, intQNum, this.tHome, this.tAway, this.aHomes, this.aAways, this.blnPower, this.byAllAddH, this.byAllAddA, this.intType);
                        quarter2.Run();
                        this.sbQuarter.Append(quarter2.sbQuarter.ToString());
                        this.sbArrange.Append(quarter2.sbArrange.ToString());
                        this.sbPlayer.Append(quarter2.sbPlayer.ToString());
                        this.sbScript.Append(quarter2.sbScript.ToString());
                        this.tHome = quarter2.tHome;
                        this.tAway = quarter2.tAway;
                    }
                    intQNum++;
                }
            }
            this.sbRepURL = new StringBuilder();
            this.sbRepXml = new StringBuilder();
        }
    }
}

