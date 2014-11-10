namespace Web.SMatchEngine
{
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Text;
    using Web.DBData;
    using Web.Helper;

    public class Match
    {
        private Arrange[] aAways = new Arrange[5];
        private Arrange[] aHomes = new Arrange[5];
        private bool blnCanPlay;
        private bool blnPower;
        private int intAbilitySumA;
        private int intAbilitySumH;
        private int[] intADefs = new int[4];
        private int[] intAOffs = new int[4];
        private int[] intHDefs = new int[4];
        private int[] intHOffs = new int[4];
        private int intLoseAbility;
        public int intMaxAbilityValue;
        public int intMVPValue;
        private int intPlayedCountA;
        private int intPlayedCountH;
        private int intTag;
        public int intTempMVPValue;
        private int intType;
        private int intWinAbility;
        public Player pMVP = new Player();
        private Random rnd = new Random(DateTime.Now.Millisecond);
        private StringBuilder sbArrange = new StringBuilder();
        private StringBuilder sbClub = new StringBuilder();
        public StringBuilder sbIntro = new StringBuilder();
        private StringBuilder sbPlayer = new StringBuilder();
        private StringBuilder sbQuarter = new StringBuilder();
        public StringBuilder sbRepURL = new StringBuilder();
        public StringBuilder sbRepXml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Report>");
        private StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbStasURL = new StringBuilder();
        public StringBuilder sbStasXml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Stas>");
        private Team tAway;
        private Team tHome;

        public Match(int intClubIDH, int intClubIDA, bool blnPower, int intType, int intTag)
        {
            this.blnPower = blnPower;
            this.intType = intType;
            this.intTag = intTag;
            this.tHome = new Team(intClubIDH, true);
            this.tAway = new Team(intClubIDA, false);
            if (this.tHome.players.Count < 4)
            {
                this.tHome.intScore = 0;
                this.tAway.intScore = 2;
                this.blnCanPlay = false;
            }
            else if (this.tAway.players.Count < 4)
            {
                this.tHome.intScore = 2;
                this.tAway.intScore = 0;
                this.blnCanPlay = false;
            }
            else
            {
                this.blnCanPlay = true;
                DataRow cArrangeRowByClubID = BTPClubManager.GetCArrangeRowByClubID(intClubIDH);
                for (int i = 0; i < 5; i++)
                {
                    this.aHomes[i] = new Arrange((int) cArrangeRowByClubID["Arrange" + (i + 1)], this.tHome);
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
                }
                DataRow row2 = BTPClubManager.GetCArrangeRowByClubID(intClubIDA);
                for (int j = 0; j < 5; j++)
                {
                    this.aAways[j] = new Arrange((int) row2["Arrange" + (j + 1)], this.tAway);
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
            this.sbClub.Append(this.tHome.intScore);
            this.sbClub.Append("</Score>");
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
            this.sbClub.Append(this.tAway.intScore);
            this.sbClub.Append("</Score>");
            this.sbClub.Append("</Club>");
            this.sbRepXml.Append(this.sbClub.ToString());
            this.sbRepXml.Append(this.sbQuarter.ToString());
            this.sbRepXml.Append(this.sbArrange.ToString());
            this.sbRepXml.Append(this.sbPlayer.ToString());
            this.sbRepXml.Append(this.sbScript.ToString());
            this.sbRepXml.Append("</Report>");
            this.sbPlayer = new StringBuilder();
            bool flag = false;
            if (this.tHome.intScore > this.tAway.intScore)
            {
                flag = true;
            }
            if (this.blnPower)
            {
                BTPArrangeLvlManager.SetArrange3Point(this.tHome.intClubID, this.intHOffs, this.intHDefs);
                BTPArrangeLvlManager.SetArrange3Point(this.tAway.intClubID, this.intAOffs, this.intADefs);
            }
            bool flag2 = false;
            bool flag3 = false;
            int trainPointMultiple = Config.GetTrainPointMultiple();
            if (((this.intType == 3) || (this.intType == 2)) || (this.intType == 5))
            {
                if (BTPToolLinkManager.HasTool(this.tHome.intUserID, 1, 9, 0) || BTPToolLinkManager.HasTool(this.tHome.intUserID, 1, 10, 0))
                {
                    flag2 = true;
                }
                if (BTPToolLinkManager.HasTool(this.tAway.intUserID, 1, 9, 0) || BTPToolLinkManager.HasTool(this.tAway.intUserID, 1, 10, 0))
                {
                    flag3 = true;
                }
            }
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
                    if (this.intType < 3)
                    {
                        if (!flag)
                        {
                            if (player.blnPlayed)
                            {
                                player.intHappy += 2;
                            }
                            else
                            {
                                player.intHappy++;
                            }
                        }
                        else if (player.blnPlayed)
                        {
                            player.intHappy--;
                        }
                        else
                        {
                            player.intHappy -= 2;
                        }
                    }
                    else
                    {
                        player.intHappy = (player.intHappy - ((100 - player.intPower) / 5)) - 10;
                    }
                    if (player.intHappy > 100)
                    {
                        player.intHappy = 100;
                    }
                    if (player.intHappy < 1)
                    {
                        player.intHappy = 1;
                    }
                    int intTrainPoint = ((((((this.rnd.Next(12, 0x12) + player.intScore) + player.intAst) + (player.intOReb * 3)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * 20;
                    if (flag2)
                    {
                        intTrainPoint *= 2;
                    }
                    intTrainPoint *= trainPointMultiple;
                    int intStatus = 1;
                    int intSuspend = 0;
                    string strEvent = "";
                    if (player.blnInjured)
                    {
                        InjuryGenerator generator = new InjuryGenerator(player.intPower);
                        generator.SetEvent();
                        intStatus = 2;
                        intSuspend = generator.intSuspend;
                        strEvent = generator.strEvent;
                    }
                    BTPPlayer3Manager.UpdatePlayerStas(player.longPlayerID, player.intScore, player.intOReb + player.intDReb, player.intAst, player.intBlk, player.intStl, player.blnPlayed, player.intHappy, player.intPower, intTrainPoint, intStatus, strEvent, intSuspend);
                }
                if (flag)
                {
                    this.intTempMVPValue = 0;
                    this.intTempMVPValue = ((((player.intScore * 12) + ((player.intOReb + player.intDReb) * 20)) + (player.intAst * 0x17)) + (player.intBlk * 0x2b)) + (player.intStl * 0x23);
                    if (this.intTempMVPValue > this.intMVPValue)
                    {
                        this.intMVPValue = this.intTempMVPValue;
                        this.pMVP = player;
                    }
                    if (player.intAbility > this.intMaxAbilityValue)
                    {
                        this.intMaxAbilityValue = player.intAbility;
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
                    if (this.intType < 3)
                    {
                        if (!flag)
                        {
                            if (player.blnPlayed)
                            {
                                player.intHappy += 2;
                            }
                            else
                            {
                                player.intHappy++;
                            }
                        }
                        else if (player.blnPlayed)
                        {
                            player.intHappy--;
                        }
                        else
                        {
                            player.intHappy -= 2;
                        }
                    }
                    else
                    {
                        player.intHappy = (player.intHappy - ((100 - player.intPower) / 5)) - 10;
                    }
                    if (player.intHappy > 100)
                    {
                        player.intHappy = 100;
                    }
                    if (player.intHappy < 1)
                    {
                        player.intHappy = 1;
                    }
                    int num5 = ((((((this.rnd.Next(15, 20) + player.intScore) + player.intAst) + (player.intOReb * 3)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * 20;
                    if (flag3)
                    {
                        num5 *= 2;
                    }
                    num5 *= trainPointMultiple;
                    int num6 = 1;
                    int num7 = 0;
                    string str2 = "";
                    if (player.blnInjured)
                    {
                        InjuryGenerator generator2 = new InjuryGenerator(player.intPower);
                        generator2.SetEvent();
                        num6 = 2;
                        num7 = generator2.intSuspend;
                        str2 = generator2.strEvent;
                    }
                    BTPPlayer3Manager.UpdatePlayerStas(player.longPlayerID, player.intScore, player.intOReb + player.intDReb, player.intAst, player.intBlk, player.intStl, player.blnPlayed, player.intHappy, player.intPower, num5, num6, str2, num7);
                }
                if (!flag)
                {
                    this.intTempMVPValue = 0;
                    this.intTempMVPValue = ((((player.intScore * 12) + ((player.intOReb + player.intDReb) * 20)) + (player.intAst * 0x17)) + (player.intBlk * 0x2b)) + (player.intStl * 0x23);
                    if (this.intTempMVPValue > this.intMVPValue)
                    {
                        this.intMVPValue = this.intTempMVPValue;
                        this.pMVP = player;
                    }
                    if (player.intAbility > this.intMaxAbilityValue)
                    {
                        this.intMaxAbilityValue = player.intAbility;
                    }
                }
                if (player.blnPlayed)
                {
                    this.intPlayedCountA++;
                    this.intAbilitySumA += player.intAbility;
                }
            }
            player = null;
            if (this.intType == 2)
            {
                BTPClubManager.ChangeReputation(this.tHome.intClubID, this.tAway.intClubID, this.tHome.intScore, this.tAway.intScore);
            }
            this.sbStasXml.Append(this.sbClub.ToString());
            this.sbStasXml.Append(this.sbPlayer.ToString());
            this.sbIntro.Append("<Intro><Intro>");
            Quarter quarter = new Quarter();
            int num8 = (quarter.GetMethodAddForMatch(this.aAways[0].intOffense, this.aHomes[0].intDefense) + quarter.GetMethodAddForMatch(this.aAways[1].intOffense, this.aHomes[1].intDefense)) + quarter.GetMethodAddForMatch(this.aAways[2].intOffense, this.aHomes[2].intDefense);
            int num9 = (quarter.GetMethodAddForMatch(this.aHomes[0].intOffense, this.aAways[0].intDefense) + quarter.GetMethodAddForMatch(this.aHomes[1].intOffense, this.aAways[1].intDefense)) + quarter.GetMethodAddForMatch(this.aHomes[2].intOffense, this.aAways[2].intDefense);
            if (flag)
            {
                this.intWinAbility = this.intAbilitySumH / this.intPlayedCountH;
                this.intLoseAbility = this.intAbilitySumA / this.intPlayedCountA;
                if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛简直是压倒性的胜利,毫无悬念可言!" + this.pMVP.strName + "成为MVP也是理所当然."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了实力战术只是次要的!本场比赛完全靠球员自身的强大能力获得胜利,这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("这场比赛验证了战术的重要性,合理的战术造就了" + this.pMVP.strName + "成为本场MVP,虽然他不是最强的."));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("只要有破敌之策和一名勇将,胜利就不是问题!" + this.pMVP.strName + "和他的经理给我们上了生动的一课!"));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("在球员强大的能力下,战术已经显得没那么重要,胜利才使关键!但是大家对于" + this.pMVP.strName + "的表现颇感惊讶."));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "凭借个人能力力挽狂澜,创造了本场的一个奇迹!他无愧成为本场的MVP!连对手也啧啧称赞!"));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛告诉我们什么叫运筹帷幄之中,制胜千里之外,合理的战术决定了本场比赛的胜利!" + this.pMVP.strName + "应当把MVP的荣誉赠与他们优秀的经理!"));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("如果拼劲全力,就算实力弱,就算战术没有优势,胜利也不是毫无希望的!" + this.pMVP.strName + "拿着MVP的奖章流出了胜利的眼泪!"));
                }
            }
            else
            {
                this.intWinAbility = this.intAbilitySumA / this.intPlayedCountA;
                this.intLoseAbility = this.intAbilitySumH / this.intPlayedCountH;
                this.intWinAbility = this.intAbilitySumH / this.intPlayedCountH;
                this.intLoseAbility = this.intAbilitySumA / this.intPlayedCountA;
                if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛简直是压倒性的胜利,毫无悬念可言!" + this.pMVP.strName + "成为MVP也是理所当然."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了实力战术只是次要的!本场比赛完全靠球员自身的强大能力获得胜利,这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("这场比赛验证了战术的重要性,合理的战术造就了" + this.pMVP.strName + "成为本场MVP,虽然他不是最强的."));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("只要有破敌之策和一名勇将,胜利就不是问题!" + this.pMVP.strName + "和他的经理给我们上了生动的一课!"));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("在球员强大的能力下,战术已经显得没那么重要,胜利才使关键!但是大家对于" + this.pMVP.strName + "的表现颇感惊讶."));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "凭借个人能力力挽狂澜,创造了本场的一个奇迹!他无愧成为本场的MVP!连对手也啧啧称赞!"));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 <= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛告诉我们什么叫运筹帷幄之中,制胜千里之外,合理的战术决定了本场比赛的胜利!" + this.pMVP.strName + "应当把MVP的荣誉赠与他们优秀的经理!"));
                }
                else if (((this.intWinAbility < this.intLoseAbility) && (this.pMVP.intAbility < this.intMaxAbilityValue)) && (num8 >= num9))
                {
                    this.sbIntro.Append(Language.HighLightRed("如果拼劲全力,就算实力弱,就算战术没有优势,胜利也不是毫无希望的!" + this.pMVP.strName + "拿着MVP的奖章流出了胜利的眼泪!"));
                }
            }
            this.sbIntro.Append("</Intro></Intro>");
            this.sbStasXml.Append(this.sbIntro.ToString());
            this.sbStasXml.Append("</Stas>");
            if (this.intType < 3)
            {
                string str3 = StringItem.FormatDate(DateTime.Now, "yyyyMMdd");
                string path = Path.GetFullPath("" + MatchItem.GetMatchPath() + @"\MatchXML\SRep\") + str3 + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str5 = string.Concat(new object[] { path, "Rep", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbRepURL.Append("MatchXML/SRep/");
                this.sbRepURL.Append(str3);
                this.sbRepURL.Append("/Rep");
                this.sbRepURL.Append(this.tHome.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.tAway.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intType);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intTag);
                this.sbRepURL.Append(".xml");
                if (File.Exists(str5))
                {
                    File.Delete(str5);
                }
                using (StreamWriter writer = File.CreateText(str5))
                {
                    writer.Write(this.sbRepXml.ToString());
                }
                path = Path.GetFullPath("" + MatchItem.GetMatchPath() + @"\MatchXML\SStas\") + str3 + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                str5 = string.Concat(new object[] { path, "Stas", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbStasURL.Append("MatchXML/SStas/");
                this.sbStasURL.Append(str3);
                this.sbStasURL.Append("/Stas");
                this.sbStasURL.Append(this.tHome.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.tAway.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intType);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intTag);
                this.sbStasURL.Append(".xml");
                if (File.Exists(str5))
                {
                    File.Delete(str5);
                }
                using (StreamWriter writer2 = File.CreateText(str5))
                {
                    writer2.Write(this.sbStasXml.ToString());
                }
            }
            if (this.intType == 5)
            {
                BTPFriMatchManager.TrainCenterMatchEndByFriMatchID(this.intTag);
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
                    if (intQNum == 1)
                    {
                        Quarter quarter = new Quarter(20, intQNum, this.tHome, this.tAway, this.aHomes, this.aAways, this.blnPower);
                        quarter.Run();
                        this.sbQuarter.Append(quarter.sbQuarter.ToString());
                        this.sbArrange.Append(quarter.sbArrange.ToString());
                        this.sbPlayer.Append(quarter.sbPlayer.ToString());
                        this.sbScript.Append(quarter.sbScript.ToString());
                        this.tHome = quarter.tHome;
                        this.tAway = quarter.tAway;
                    }
                    else
                    {
                        if (this.tHome.intScore != this.tAway.intScore)
                        {
                            this.Finished();
                            return;
                        }
                        Quarter quarter2 = new Quarter(5, intQNum, this.tHome, this.tAway, this.aHomes, this.aAways, this.blnPower);
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

