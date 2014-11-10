namespace Web.SMatchEngine
{
    using System;
    using System.Collections;
    using System.Text;
    using Web.Helper;

    public class Quarter
    {
        private Arrange aAway;
        private Arrange[] aAways;
        private Arrange aHome;
        private Arrange[] aHomes;
        private bool blnPower;
        private bool blnStart;
        private bool blnTurn;
        private int intArrangeID;
        private int intQNum;
        private int intQNumC;
        private Random rnd;
        public StringBuilder sbArrange;
        public StringBuilder sbPlayer;
        public StringBuilder sbQuarter;
        public StringBuilder sbScript;
        public Team tAway;
        public Team tHome;
        public Timer timer;

        public Quarter()
        {
            this.sbScript = new StringBuilder();
            this.sbQuarter = new StringBuilder();
            this.sbArrange = new StringBuilder();
            this.sbPlayer = new StringBuilder();
            this.rnd = new Random(DateTime.Now.Millisecond);
        }

        public Quarter(int intMinutes, int intQNum, Team tHome, Team tAway, Arrange[] aHomes, Arrange[] aAways, bool blnPower)
        {
            this.sbScript = new StringBuilder();
            this.sbQuarter = new StringBuilder();
            this.sbArrange = new StringBuilder();
            this.sbPlayer = new StringBuilder();
            this.rnd = new Random(DateTime.Now.Millisecond);
            this.blnPower = blnPower;
            this.intQNum = intQNum;
            this.intQNumC = intQNum;
            this.tHome = tHome;
            this.tAway = tAway;
            this.aHomes = aHomes;
            this.aAways = aAways;
            this.blnStart = false;
            this.blnTurn = true;
            this.timer = new Timer(intMinutes);
            this.intArrangeID = (this.intQNumC * 2) - 1;
        }

        public void AddBlockStatus()
        {
            if (this.blnTurn)
            {
                this.aAway.pDCP.intBlk++;
            }
            else
            {
                this.aHome.pDCP.intBlk++;
            }
        }

        public void AddFoulStatus(bool blnIsOff)
        {
            if (blnIsOff)
            {
                if (this.blnTurn)
                {
                    this.aHome.pOCP.intFoul++;
                }
                else
                {
                    this.aAway.pOCP.intFoul++;
                }
            }
            else if (this.blnTurn)
            {
                this.aAway.pDCP.intFoul++;
            }
            else
            {
                this.aHome.pDCP.intFoul++;
            }
        }

        public void AddFTStatus(bool blnGood)
        {
            if (blnGood)
            {
                if (this.blnTurn)
                {
                    this.tHome.intScore++;
                    this.aHome.pOCP.intScore++;
                    this.aHome.pOCP.intFT++;
                    this.aHome.pOCP.intFTs++;
                }
                else
                {
                    this.tAway.intScore++;
                    this.aAway.pOCP.intScore++;
                    this.aAway.pOCP.intFT++;
                    this.aAway.pOCP.intFTs++;
                }
            }
            else if (this.blnTurn)
            {
                this.aHome.pOCP.intFTs++;
            }
            else
            {
                this.aAway.pOCP.intFTs++;
            }
        }

        public void AddOffStatus(int intOffMethod, bool blnGood, bool blnAssist)
        {
            if (blnGood)
            {
                if (intOffMethod == 5)
                {
                    if (this.blnTurn)
                    {
                        this.tHome.intScore += 3;
                        this.aHome.pOCP.intScore += 3;
                        this.aHome.pOCP.int3P++;
                        this.aHome.pOCP.int3Ps++;
                    }
                    else
                    {
                        this.tAway.intScore += 3;
                        this.aAway.pOCP.intScore += 3;
                        this.aAway.pOCP.int3P++;
                        this.aAway.pOCP.int3Ps++;
                    }
                }
                else if (this.blnTurn)
                {
                    this.tHome.intScore += 2;
                    this.aHome.pOCP.intScore += 2;
                    this.aHome.pOCP.intFG++;
                    this.aHome.pOCP.intFGs++;
                }
                else
                {
                    this.tAway.intScore += 2;
                    this.aAway.pOCP.intScore += 2;
                    this.aAway.pOCP.intFG++;
                    this.aAway.pOCP.intFGs++;
                }
                if (blnAssist)
                {
                    if (this.blnTurn)
                    {
                        this.aHome.pACP.intAst++;
                    }
                    else
                    {
                        this.aAway.pACP.intAst++;
                    }
                }
            }
            else if (intOffMethod == 5)
            {
                if (this.blnTurn)
                {
                    this.aHome.pOCP.int3Ps++;
                }
                else
                {
                    this.aAway.pOCP.int3Ps++;
                }
            }
            else if (this.blnTurn)
            {
                this.aHome.pOCP.intFGs++;
            }
            else
            {
                this.aAway.pOCP.intFGs++;
            }
        }

        public void AddRebStatus(bool blnOff)
        {
            if (blnOff)
            {
                if (this.blnTurn)
                {
                    this.aHome.pORP.intOReb++;
                }
                else
                {
                    this.aAway.pORP.intOReb++;
                }
            }
            else if (this.blnTurn)
            {
                this.aAway.pDRP.intDReb++;
            }
            else
            {
                this.aHome.pDRP.intDReb++;
            }
        }

        public void AddStealStatus()
        {
            if (this.blnTurn)
            {
                this.aAway.pDCP.intStl++;
            }
            else
            {
                this.aHome.pDCP.intStl++;
            }
        }

        public void AddToStatus()
        {
            if (this.blnTurn)
            {
                this.aHome.pOCP.intTo++;
            }
            else
            {
                this.aAway.pOCP.intTo++;
            }
        }

        public bool GetAfterBlk()
        {
            bool flag;
            if (this.blnTurn)
            {
                int num = this.aHome.GetBlkBallAbility();
                int num2 = this.aAway.GetBlkBallAbility();
                if (num > this.rnd.Next(0, num + (num2 * 5)))
                {
                    flag = true;
                    this.sbScript.Append("<Script>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.aHome.pBBP.strName);
                    this.sbScript.Append("得到球，继续进攻！</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    return flag;
                }
                return false;
            }
            int blkBallAbility = this.aAway.GetBlkBallAbility();
            int num4 = this.aHome.GetBlkBallAbility();
            if (blkBallAbility > this.rnd.Next(0, blkBallAbility + (num4 * 5)))
            {
                flag = true;
                this.sbScript.Append("<Script>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aAway.pBBP.strName);
                this.sbScript.Append("得到球，继续进攻！</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                return flag;
            }
            return false;
        }

        public bool GetFT(int intMethod)
        {
            bool flag = true;
            if (this.blnTurn)
            {
                for (int j = 0; j < intMethod; j++)
                {
                    if (this.aHome.pOCP.intFTAbility > this.rnd.Next(0, 800))
                    {
                        this.AddFTStatus(true);
                        if (intMethod > 1)
                        {
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>第");
                            this.sbScript.Append((int) (j + 1));
                            this.sbScript.Append("罚命中！</Content>");
                            this.sbScript.Append("\t\t<Score>");
                            this.sbScript.Append(this.tHome.intScore);
                            this.sbScript.Append(":");
                            this.sbScript.Append(this.tAway.intScore);
                            this.sbScript.Append("</Score>");
                            this.sbScript.Append("\t</Script>");
                        }
                        else
                        {
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>加罚命中！</Content>");
                            this.sbScript.Append("\t\t<Score>");
                            this.sbScript.Append(this.tHome.intScore);
                            this.sbScript.Append(":");
                            this.sbScript.Append(this.tAway.intScore + "</Score>");
                            this.sbScript.Append("\t</Script>");
                        }
                    }
                    else
                    {
                        this.AddFTStatus(false);
                        if (intMethod == 1)
                        {
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>加罚未命中！</Content>");
                            this.sbScript.Append("\t\t<Score>");
                            this.sbScript.Append(this.tHome.intScore);
                            this.sbScript.Append(":");
                            this.sbScript.Append(this.tAway.intScore);
                            this.sbScript.Append("</Score>");
                            this.sbScript.Append("\t</Script>");
                            flag = false;
                        }
                        else
                        {
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>第");
                            this.sbScript.Append((int) (j + 1));
                            this.sbScript.Append("罚未命中！</Content>");
                            this.sbScript.Append("\t\t<Score>");
                            this.sbScript.Append(this.tHome.intScore);
                            this.sbScript.Append(":");
                            this.sbScript.Append(this.tAway.intScore);
                            this.sbScript.Append("</Score>");
                            this.sbScript.Append("\t</Script>");
                            if (j == (intMethod - 1))
                            {
                                flag = false;
                            }
                        }
                    }
                }
                return flag;
            }
            for (int i = 0; i < intMethod; i++)
            {
                if (this.aAway.pOCP.intFTAbility > this.rnd.Next(0, 800))
                {
                    this.AddFTStatus(true);
                    if (intMethod > 1)
                    {
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>第");
                        this.sbScript.Append((int) (i + 1));
                        this.sbScript.Append("罚命中！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                    else
                    {
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>加罚命中！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                }
                else
                {
                    this.AddFTStatus(false);
                    if (intMethod == 1)
                    {
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>加罚未命中！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        flag = false;
                    }
                    else
                    {
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>第");
                        this.sbScript.Append((int) (i + 1));
                        this.sbScript.Append("罚未命中！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        if (i == (intMethod - 1))
                        {
                            flag = false;
                        }
                    }
                }
            }
            return flag;
        }

        public bool GetIsAssist(int intOffMethod)
        {
            if (((intOffMethod == 2) || (intOffMethod == 4)) || (intOffMethod == 8))
            {
                return true;
            }
            int num = this.rnd.Next(0, 100);
            if (intOffMethod == 5)
            {
                if (num < 20)
                {
                    return true;
                }
            }
            else if ((intOffMethod == 7) && (num < 10))
            {
                return true;
            }
            return false;
        }

        private int GetMethodAdd(int intOffense, int intDefense)
        {
            int[,] numArray = new int[,] { { 120, 0x5f, 110, 90 }, { 110, 90, 120, 0x5f }, { 0x5f, 120, 90, 110 }, { 90, 110, 0x5f, 120 } };
            return numArray[intOffense - 1, intDefense - 1];
        }

        public int GetMethodAddForMatch(int intOffense, int intDefense)
        {
            int[,] numArray = new int[,] { { 120, 0x5f, 110, 90 }, { 110, 90, 120, 0x5f }, { 0x5f, 120, 90, 110 }, { 90, 110, 0x5f, 120 } };
            return numArray[intOffense - 1, intDefense - 1];
        }

        public bool GetReb()
        {
            if (this.blnTurn)
            {
                int num = this.aHome.GetOffRebAbility();
                int num2 = this.aAway.GetDefRebAbility();
                if (num > this.rnd.Next(0, num + num2))
                {
                    this.AddRebStatus(true);
                    return true;
                }
                this.AddRebStatus(false);
                return false;
            }
            int offRebAbility = this.aAway.GetOffRebAbility();
            int defRebAbility = this.aHome.GetDefRebAbility();
            if (offRebAbility > this.rnd.Next(0, offRebAbility + defRebAbility))
            {
                this.AddRebStatus(true);
                return true;
            }
            this.AddRebStatus(false);
            return false;
        }

        public void Run()
        {
            while (!this.timer.IsFinished())
            {
                this.RunOneRound();
            }
            this.SetOutArrange();
            this.sbQuarter.Append("<Quarter QuarterID=\"");
            this.sbQuarter.Append(this.intQNumC + "\">");
            this.sbQuarter.Append("\t<ScoreH>");
            this.sbQuarter.Append(this.tHome.intScore);
            this.sbQuarter.Append("</ScoreH>");
            this.sbQuarter.Append("\t<ScoreA>");
            this.sbQuarter.Append(this.tAway.intScore);
            this.sbQuarter.Append("</ScoreA>");
            this.sbQuarter.Append("</Quarter>");
        }

        public void RunOneRound()
        {
            if (!this.timer.IsFinished())
            {
                if (!this.blnStart)
                {
                    if (this.intQNum < 2)
                    {
                        this.aHome = this.aHomes[0];
                        this.aAway = this.aAways[0];
                        this.SetArrangeAbility(0);
                    }
                    else
                    {
                        this.aHome = this.aHomes[0];
                        this.aAway = this.aAways[0];
                        if ((this.tHome.intAChange == 2) || (this.tHome.intAChange == 4))
                        {
                            this.aHome = this.aHomes[this.tHome.intAChange];
                        }
                        else
                        {
                            this.tHome.intAChange = 0;
                        }
                        if ((this.tAway.intAChange == 2) || (this.tAway.intAChange == 4))
                        {
                            this.aAway = this.aAways[this.tAway.intAChange];
                        }
                        else
                        {
                            this.tAway.intAChange = 0;
                        }
                        this.SetArrangeAbility(0);
                    }
                    this.sbArrange.Append("<Arrange ArrangeID=\"");
                    this.sbArrange.Append(this.intArrangeID + "\">");
                    this.sbArrange.Append("\t<ClubID>");
                    this.sbArrange.Append(this.tHome.intClubID);
                    this.sbArrange.Append("</ClubID>");
                    this.sbArrange.Append("\t<QuarterID>");
                    this.sbArrange.Append(this.intQNumC);
                    this.sbArrange.Append("</QuarterID>");
                    this.sbArrange.Append("\t<Offense>");
                    this.sbArrange.Append(this.aHome.intOffense);
                    this.sbArrange.Append("</Offense>");
                    this.sbArrange.Append("\t<Defense>");
                    this.sbArrange.Append(this.aHome.intDefense);
                    this.sbArrange.Append("</Defense>");
                    this.sbArrange.Append("\t<OffHard>");
                    this.sbArrange.Append(this.aHome.intOffHard);
                    this.sbArrange.Append("</OffHard>");
                    this.sbArrange.Append("\t<DefHard>");
                    this.sbArrange.Append(this.aHome.intDefHard);
                    this.sbArrange.Append("</DefHard>");
                    this.sbArrange.Append("</Arrange>");
                    this.sbPlayer.Append("<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aHome.pC.longPlayerID);
                    this.sbPlayer.Append("\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aHome.pC.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aHome.pC.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aHome.pC.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aHome.pC.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aHome.pC.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aHome.pC.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aHome.pC.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                    this.sbPlayer.Append("\t<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aHome.pF.longPlayerID);
                    this.sbPlayer.Append("\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aHome.pF.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aHome.pF.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aHome.pF.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aHome.pF.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aHome.pF.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aHome.pF.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aHome.pF.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                    this.sbPlayer.Append("\t<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aHome.pG.longPlayerID + "\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aHome.pG.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aHome.pG.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aHome.pG.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aHome.pG.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aHome.pG.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aHome.pG.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aHome.pG.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                    this.intArrangeID++;
                    this.sbArrange.Append("<Arrange ArrangeID=\"");
                    this.sbArrange.Append(this.intArrangeID + "\">");
                    this.sbArrange.Append("\t<ClubID>");
                    this.sbArrange.Append(this.tAway.intClubID);
                    this.sbArrange.Append("</ClubID>");
                    this.sbArrange.Append("\t<QuarterID>");
                    this.sbArrange.Append(this.intQNumC);
                    this.sbArrange.Append("</QuarterID>");
                    this.sbArrange.Append("\t<Offense>");
                    this.sbArrange.Append(this.aAway.intOffense);
                    this.sbArrange.Append("</Offense>");
                    this.sbArrange.Append("\t<Defense>");
                    this.sbArrange.Append(this.aAway.intDefense);
                    this.sbArrange.Append("</Defense>");
                    this.sbArrange.Append("\t<OffHard>");
                    this.sbArrange.Append(this.aAway.intOffHard);
                    this.sbArrange.Append("</OffHard>");
                    this.sbArrange.Append("\t<DefHard>");
                    this.sbArrange.Append(this.aAway.intDefHard);
                    this.sbArrange.Append("</DefHard>");
                    this.sbArrange.Append("</Arrange>");
                    this.sbPlayer.Append("<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aAway.pC.longPlayerID);
                    this.sbPlayer.Append("\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aAway.pC.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aAway.pC.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aAway.pC.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aAway.pC.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aAway.pC.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aAway.pC.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aAway.pC.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                    this.sbPlayer.Append("\t<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aAway.pF.longPlayerID);
                    this.sbPlayer.Append("\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aAway.pF.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aAway.pF.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aAway.pF.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aAway.pF.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aAway.pF.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aAway.pF.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aAway.pF.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                    this.sbPlayer.Append("\t<Player PlayerID=\"");
                    this.sbPlayer.Append(this.aAway.pG.longPlayerID);
                    this.sbPlayer.Append("\">");
                    this.sbPlayer.Append("\t\t<ArrangeID>");
                    this.sbPlayer.Append(this.intArrangeID);
                    this.sbPlayer.Append("</ArrangeID>");
                    this.sbPlayer.Append("\t\t<Name>");
                    this.sbPlayer.Append(this.aAway.pG.strName);
                    this.sbPlayer.Append("</Name>");
                    this.sbPlayer.Append("\t\t<Number>");
                    this.sbPlayer.Append(this.aAway.pG.intNumber);
                    this.sbPlayer.Append("</Number>");
                    this.sbPlayer.Append("\t\t<Age>");
                    this.sbPlayer.Append(this.aAway.pG.intAge);
                    this.sbPlayer.Append("</Age>");
                    this.sbPlayer.Append("\t\t<Pos>");
                    this.sbPlayer.Append(this.aAway.pG.intPos);
                    this.sbPlayer.Append("</Pos>");
                    this.sbPlayer.Append("\t\t<Height>");
                    this.sbPlayer.Append(this.aAway.pG.intHeight);
                    this.sbPlayer.Append("</Height>");
                    this.sbPlayer.Append("\t\t<Weight>");
                    this.sbPlayer.Append(this.aAway.pG.intWeight);
                    this.sbPlayer.Append("</Weight>");
                    this.sbPlayer.Append("\t\t<Ability>");
                    this.sbPlayer.Append((float) (((float) this.aAway.pG.intAbility) / 10f));
                    this.sbPlayer.Append("</Ability>");
                    this.sbPlayer.Append("\t</Player>");
                }
                else
                {
                    int num = this.tHome.intScore - this.tAway.intScore;
                    if (num >= 10)
                    {
                        if (((this.tHome.intAChange != 3) && (this.tHome.intAChange != 2)) && (this.tHome.intAChange != 4))
                        {
                            this.aHome = this.aHomes[3];
                            this.tHome.intAChange = 3;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                            this.sbScript.Append(this.tHome.strClubName);
                            this.sbScript.Append("更换领先10分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                        if (this.tAway.intAChange != 4)
                        {
                            this.aAway = this.aAways[4];
                            this.tAway.intAChange = 4;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                            this.sbScript.Append(this.tAway.strClubName);
                            this.sbScript.Append("更换落后10分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(2);
                        }
                    }
                    else if ((num >= 5) && (num < 8))
                    {
                        if (((this.tHome.intAChange != 1) && (this.tHome.intAChange != 2)) && (this.tHome.intAChange != 4))
                        {
                            this.aHome = this.aHomes[1];
                            this.tHome.intAChange = 1;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                            this.sbScript.Append(this.tHome.strClubName);
                            this.sbScript.Append("更换领先5分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                        if ((this.tAway.intAChange != 2) && (this.tAway.intAChange != 4))
                        {
                            this.aAway = this.aAways[2];
                            this.tAway.intAChange = 2;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                            this.sbScript.Append(this.tAway.strClubName);
                            this.sbScript.Append("更换落后5分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(2);
                        }
                    }
                    else if ((num > -2) && (num < 2))
                    {
                        if (((this.tHome.intAChange != 0) && (this.tHome.intAChange != 2)) && (this.tHome.intAChange != 4))
                        {
                            this.aHome = this.aHomes[0];
                            this.tHome.intAChange = 0;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=green&gt;");
                            this.sbScript.Append(this.tHome.strClubName);
                            this.sbScript.Append("恢复初始阵容&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                        if (((this.tAway.intAChange != 0) && (this.tAway.intAChange != 2)) && (this.tAway.intAChange != 4))
                        {
                            this.aAway = this.aAways[0];
                            this.tAway.intAChange = 0;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=green&gt;");
                            this.sbScript.Append(this.tAway.strClubName);
                            this.sbScript.Append("恢复初始阵容&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(2);
                        }
                    }
                    else if ((num <= -5) && (num > -8))
                    {
                        if (((this.tAway.intAChange != 1) && (this.tAway.intAChange != 2)) && (this.tAway.intAChange != 4))
                        {
                            this.aAway = this.aAways[1];
                            this.tAway.intAChange = 1;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                            this.sbScript.Append(this.tAway.strClubName);
                            this.sbScript.Append("更换领先5分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(2);
                        }
                        if ((this.tHome.intAChange != 2) && (this.tHome.intAChange != 4))
                        {
                            this.aHome = this.aHomes[2];
                            this.tHome.intAChange = 2;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                            this.sbScript.Append(this.tHome.strClubName);
                            this.sbScript.Append("更换落后5分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                    }
                    else if (num <= -10)
                    {
                        if (((this.tAway.intAChange != 3) && (this.tAway.intAChange != 2)) && (this.tAway.intAChange != 4))
                        {
                            this.aAway = this.aAways[3];
                            this.tAway.intAChange = 3;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                            this.sbScript.Append(this.tAway.strClubName);
                            this.sbScript.Append("更换领先10分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                        if (this.tHome.intAChange != 4)
                        {
                            this.aHome = this.aHomes[4];
                            this.tHome.intAChange = 4;
                            this.sbScript.Append("<Script>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                            this.sbScript.Append(this.tHome.strClubName);
                            this.sbScript.Append("更换落后10分阵容战术&lt;/font&gt;</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.SetArrangeAbility(1);
                        }
                    }
                }
                if (!this.blnStart)
                {
                    if (this.rnd.Next(0, 100) > 50)
                    {
                        this.blnTurn = true;
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(this.tHome.strClubName);
                        this.sbScript.Append("中圈发球，开始进攻！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                    else
                    {
                        this.blnTurn = false;
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(this.tAway.strClubName);
                        this.sbScript.Append("中圈发球，开始进攻！</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                    this.blnStart = true;
                }
                else
                {
                    if (this.blnTurn)
                    {
                        int offAbility = this.aHome.GetOffAbility();
                        int num3 = (this.aAway.GetDefAbility(this.aHome.pOCP, this.aHome.intOffMethod) * this.GetMethodAdd(this.aHome.intOffense, this.aAway.intDefense)) / 100;
                        this.aHome.pOCP.intPower -= MatchItem.GetSLosePower(this.aHome.pOCP.intStamina, this.aHome.intOffHard);
                        if (this.aHome.pOCP.intPower < 30)
                        {
                            this.aHome.pOCP.intPower = 30;
                        }
                        this.aAway.pDCP.intPower -= MatchItem.GetSLosePower(this.aAway.pDCP.intStamina, this.aAway.intDefHard);
                        if (this.aAway.pDCP.intPower < 30)
                        {
                            this.aAway.pDCP.intPower = 30;
                        }
                        if ((this.tAway.intScore - this.tHome.intScore) > 15)
                        {
                            offAbility = (offAbility * 13) / 10;
                        }
                        if (offAbility > this.rnd.Next(0, offAbility + num3))
                        {
                            int num4 = this.rnd.Next(0, 100);
                            int intOffMethod = this.aHome.intOffMethod;
                            this.timer.Next();
                            bool isAssist = this.GetIsAssist(intOffMethod);
                            if (num4 < 0x58)
                            {
                                if (intOffMethod == 5)
                                {
                                    if ((((this.aHome.pOCP.intPoint3 * 0x12) - (((this.aHome.pOCP.intPoint3 * this.aHome.pOCP.intPoint3) * 9) / 0x3e8)) / 100) > this.rnd.Next(0, 100))
                                    {
                                        this.AddOffStatus(intOffMethod, true, isAssist);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, isAssist));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, isAssist));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.AddOffStatus(intOffMethod, true, isAssist);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, isAssist));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else if ((num4 >= 0x58) && (num4 < 0x5b))
                            {
                                this.AddOffStatus(intOffMethod, true, isAssist);
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, isAssist));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time></Time>");
                                this.sbScript.Append("\t\t<Content>同时造成");
                                this.sbScript.Append(this.aAway.pDCP.strName);
                                this.sbScript.Append("犯规，加罚一次。</Content>");
                                this.sbScript.Append("\t\t<Score></Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                                if (this.GetFT(1))
                                {
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                this.AddOffStatus(intOffMethod, false, false);
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, isAssist));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                                if (intOffMethod == 5)
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>造成");
                                    this.sbScript.Append(this.aAway.pDCP.strName);
                                    this.sbScript.Append("的犯规，罚球3次</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetFT(3))
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>造成");
                                    this.sbScript.Append(this.aAway.pDCP.strName);
                                    this.sbScript.Append("的犯规，罚球2次</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetFT(2))
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int num6 = this.rnd.Next(0, 100);
                            int num7 = this.aHome.intOffMethod;
                            this.timer.Next();
                            bool blnAssist = this.GetIsAssist(num7);
                            if (num6 < 5)
                            {
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetOffFoul(this.aHome.pOCP, this.aAway.pDCP));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(true);
                                this.blnTurn = !this.blnTurn;
                            }
                            else if ((num6 >= 5) && (num6 < 10))
                            {
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetNormalFoul(this.aHome.pOCP, this.aAway.pDCP));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                            }
                            else if ((num6 >= 10) && (num6 < 0x19))
                            {
                                int num8 = this.rnd.Next(0, 100);
                                if (this.aAway.pDCP.intPos > 2)
                                {
                                    if (num8 < 30)
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetSteal(this.aHome.pOCP, this.aAway.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddStealStatus();
                                        this.AddToStatus();
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetTo(this.aHome.pOCP, this.aAway.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddToStatus();
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else if (num8 < 8)
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetSteal(this.aHome.pOCP, this.aAway.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddStealStatus();
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetTo(this.aHome.pOCP, this.aAway.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                int num9 = this.rnd.Next(0, 100);
                                if (this.aAway.pDCP.intPos < 4)
                                {
                                    if ((num9 < 0x12) && (this.aAway.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                                    {
                                        this.AddOffStatus(num7, false, false);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBlock(num7, this.aHome.pACP, this.aHome.pOCP, this.aAway.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddBlockStatus();
                                        if (this.GetAfterBlk())
                                        {
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.AddOffStatus(num7, false, false);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBadOff(num7, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, blnAssist));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else if ((num9 < 3) && (this.aAway.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                                {
                                    this.AddOffStatus(num7, false, false);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetBlock(num7, this.aHome.pACP, this.aHome.pOCP, this.aAway.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddBlockStatus();
                                    if (this.GetAfterBlk())
                                    {
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.AddOffStatus(num7, false, false);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetBadOff(num7, this.aHome.pOCP, this.aHome.pACP, this.aAway.pDCP, blnAssist));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                        }
                    }
                    else
                    {
                        int num10 = this.aAway.GetOffAbility();
                        int num11 = (this.aHome.GetDefAbility(this.aAway.pOCP, this.aAway.intOffMethod) * this.GetMethodAdd(this.aAway.intOffense, this.aHome.intDefense)) / 100;
                        this.aAway.pOCP.intPower -= MatchItem.GetSLosePower(this.aAway.pOCP.intStamina, this.aAway.intOffHard);
                        if (this.aAway.pOCP.intPower < 30)
                        {
                            this.aAway.pOCP.intPower = 30;
                        }
                        this.aHome.pDCP.intPower -= MatchItem.GetSLosePower(this.aHome.pDCP.intStamina, this.aHome.intDefHard);
                        if (this.aHome.pDCP.intPower < 30)
                        {
                            this.aHome.pDCP.intPower = 30;
                        }
                        if ((this.tHome.intScore - this.tAway.intScore) > 15)
                        {
                            num10 = (num10 * 13) / 10;
                        }
                        if (num10 > this.rnd.Next(0, num10 + num11))
                        {
                            int num12 = this.rnd.Next(0, 100);
                            int num13 = this.aAway.intOffMethod;
                            this.timer.Next();
                            bool flag3 = this.GetIsAssist(num13);
                            if (num12 < 0x58)
                            {
                                if (num13 == 5)
                                {
                                    if ((((this.aAway.pOCP.intPoint3 * 0x12) - (((this.aAway.pOCP.intPoint3 * this.aAway.pOCP.intPoint3) * 9) / 0x3e8)) / 100) > this.rnd.Next(0, 100))
                                    {
                                        this.AddOffStatus(num13, true, flag3);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetGoodOff(num13, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag3));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBadOff(num13, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag3));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.AddOffStatus(num13, true, flag3);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetGoodOff(num13, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag3));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else if ((num12 >= 0x58) && (num12 < 0x5b))
                            {
                                this.AddOffStatus(num13, true, flag3);
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetGoodOff(num13, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag3));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time></Time>");
                                this.sbScript.Append("\t\t<Content>同时造成");
                                this.sbScript.Append(this.aHome.pDCP.strName);
                                this.sbScript.Append("犯规，加罚1次。</Content>");
                                this.sbScript.Append("\t\t<Score></Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                                if (this.GetFT(1))
                                {
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                this.AddOffStatus(num13, false, false);
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime() + "</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetBadOff(num13, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag3));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                                if (num13 == 5)
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>造成");
                                    this.sbScript.Append(this.aHome.pDCP.strName);
                                    this.sbScript.Append("的犯规，罚球3次</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetFT(3))
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>造成");
                                    this.sbScript.Append(this.aHome.pDCP.strName);
                                    this.sbScript.Append("的犯规，罚球2次</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetFT(2))
                                    {
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int num14 = this.rnd.Next(0, 100);
                            int num15 = this.aAway.intOffMethod;
                            this.timer.Next();
                            bool flag4 = this.GetIsAssist(num15);
                            if (num14 < 5)
                            {
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetOffFoul(this.aAway.pOCP, this.aHome.pDCP));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(true);
                                this.blnTurn = !this.blnTurn;
                            }
                            else if ((num14 >= 5) && (num14 < 10))
                            {
                                this.sbScript.Append("<Script>");
                                this.sbScript.Append("\t\t<QuarterID>");
                                this.sbScript.Append(this.intQNumC);
                                this.sbScript.Append("</QuarterID>");
                                this.sbScript.Append("\t\t<Time>");
                                this.sbScript.Append(this.timer.GetTime());
                                this.sbScript.Append("</Time>");
                                this.sbScript.Append("\t\t<Content>");
                                this.sbScript.Append(ScriptGenerator.GetNormalFoul(this.aAway.pOCP, this.aHome.pDCP));
                                this.sbScript.Append("</Content>");
                                this.sbScript.Append("\t\t<Score>");
                                this.sbScript.Append(this.tHome.intScore);
                                this.sbScript.Append(":");
                                this.sbScript.Append(this.tAway.intScore);
                                this.sbScript.Append("</Score>");
                                this.sbScript.Append("\t</Script>");
                                this.AddFoulStatus(false);
                            }
                            else if ((num14 >= 10) && (num14 < 0x19))
                            {
                                int num16 = this.rnd.Next(0, 100);
                                if (this.aHome.pDCP.intPos > 2)
                                {
                                    if (num16 < 30)
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetSteal(this.aAway.pOCP, this.aHome.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddStealStatus();
                                        this.AddToStatus();
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetTo(this.aAway.pOCP, this.aHome.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddToStatus();
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else if (num16 < 8)
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetSteal(this.aAway.pOCP, this.aHome.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddStealStatus();
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetTo(this.aAway.pOCP, this.aHome.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddToStatus();
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                            else
                            {
                                int num17 = this.rnd.Next(0, 100);
                                if (this.aHome.pDCP.intPos < 4)
                                {
                                    if ((num17 < 0x12) && (this.aHome.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                                    {
                                        this.AddOffStatus(num15, false, false);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBlock(num15, this.aAway.pACP, this.aAway.pOCP, this.aHome.pDCP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.AddBlockStatus();
                                        if (this.GetAfterBlk())
                                        {
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.blnTurn = !this.blnTurn;
                                    }
                                    else
                                    {
                                        this.AddOffStatus(num15, false, false);
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time>");
                                        this.sbScript.Append(this.timer.GetTime());
                                        this.sbScript.Append("</Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetBadOff(num15, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag4));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score>");
                                        this.sbScript.Append(this.tHome.intScore);
                                        this.sbScript.Append(":");
                                        this.sbScript.Append(this.tAway.intScore);
                                        this.sbScript.Append("</Score>");
                                        this.sbScript.Append("\t</Script>");
                                        if (this.GetReb())
                                        {
                                            this.sbScript.Append("<Script>");
                                            this.sbScript.Append("\t\t<QuarterID>");
                                            this.sbScript.Append(this.intQNumC);
                                            this.sbScript.Append("</QuarterID>");
                                            this.sbScript.Append("\t\t<Time></Time>");
                                            this.sbScript.Append("\t\t<Content>");
                                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                            this.sbScript.Append("</Content>");
                                            this.sbScript.Append("\t\t<Score></Score>");
                                            this.sbScript.Append("\t</Script>");
                                            this.RunOneRound();
                                            return;
                                        }
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.blnTurn = !this.blnTurn;
                                    }
                                }
                                else if ((num17 < 3) && (this.aHome.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                                {
                                    this.AddOffStatus(num15, false, false);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetBlock(num15, this.aAway.pACP, this.aAway.pOCP, this.aHome.pDCP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.AddBlockStatus();
                                    if (this.GetAfterBlk())
                                    {
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.blnTurn = !this.blnTurn;
                                }
                                else
                                {
                                    this.AddOffStatus(num15, false, false);
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time>");
                                    this.sbScript.Append(this.timer.GetTime());
                                    this.sbScript.Append("</Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetBadOff(num15, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, flag4));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score>");
                                    this.sbScript.Append(this.tHome.intScore);
                                    this.sbScript.Append(":");
                                    this.sbScript.Append(this.tAway.intScore);
                                    this.sbScript.Append("</Score>");
                                    this.sbScript.Append("\t</Script>");
                                    if (this.GetReb())
                                    {
                                        this.sbScript.Append("<Script>");
                                        this.sbScript.Append("\t\t<QuarterID>");
                                        this.sbScript.Append(this.intQNumC);
                                        this.sbScript.Append("</QuarterID>");
                                        this.sbScript.Append("\t\t<Time></Time>");
                                        this.sbScript.Append("\t\t<Content>");
                                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                                        this.sbScript.Append("</Content>");
                                        this.sbScript.Append("\t\t<Score></Score>");
                                        this.sbScript.Append("\t</Script>");
                                        this.RunOneRound();
                                        return;
                                    }
                                    this.sbScript.Append("<Script>");
                                    this.sbScript.Append("\t\t<QuarterID>");
                                    this.sbScript.Append(this.intQNumC);
                                    this.sbScript.Append("</QuarterID>");
                                    this.sbScript.Append("\t\t<Time></Time>");
                                    this.sbScript.Append("\t\t<Content>");
                                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                                    this.sbScript.Append("</Content>");
                                    this.sbScript.Append("\t\t<Score></Score>");
                                    this.sbScript.Append("\t</Script>");
                                    this.blnTurn = !this.blnTurn;
                                }
                            }
                        }
                    }
                    this.SetPlayerInjure();
                }
            }
        }

        public void SetArrangeAbility(int intType)
        {
            if (intType == 1)
            {
                this.aHome.pC.SetAbility(1);
                this.aHome.pF.SetAbility(3);
                this.aHome.pG.SetAbility(5);
            }
            else if (intType == 2)
            {
                this.aAway.pC.SetAbility(1);
                this.aAway.pF.SetAbility(3);
                this.aAway.pG.SetAbility(5);
            }
            else
            {
                this.aHome.pC.SetAbility(1);
                this.aHome.pF.SetAbility(3);
                this.aHome.pG.SetAbility(5);
                this.aAway.pC.SetAbility(1);
                this.aAway.pF.SetAbility(3);
                this.aAway.pG.SetAbility(5);
            }
        }

        public void SetOutArrange()
        {
            Player player;
            IDictionaryEnumerator enumerator = this.tHome.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                player = (Player) enumerator.Value;
                if (player != null)
                {
                    player.blnOnArrange = false;
                }
            }
            enumerator = this.tAway.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                player = (Player) enumerator.Value;
                if (player != null)
                {
                    player.blnOnArrange = false;
                }
            }
        }

        public void SetPlayerInjure()
        {
            int num = this.rnd.Next(0, 0x2ee0);
            if (num < 15)
            {
                if (!this.blnTurn)
                {
                    if (((this.aHome.pOCP != null) && (this.aHome.pOCP.intStatus != 2)) && (this.rnd.Next(this.aHome.pOCP.intPower, 100) < 90))
                    {
                        this.aHome.pOCP.blnInjured = true;
                        this.aHome.pOCP.intOffAbility /= 2;
                        this.aHome.pOCP.intDefAbility /= 2;
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aHome.pOCP.strName);
                        this.sbScript.Append("受伤，能力减少50%！&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                }
                else if (((this.aAway.pOCP != null) && (this.aAway.pOCP.intStatus != 2)) && (this.rnd.Next(this.aAway.pOCP.intPower, 100) < 90))
                {
                    this.aAway.pOCP.blnInjured = true;
                    this.aAway.pOCP.intOffAbility /= 2;
                    this.aAway.pOCP.intDefAbility /= 2;
                    this.sbScript.Append("<Script>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aAway.pOCP.strName);
                    this.sbScript.Append("受伤，能力减少50%！&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                }
            }
            else if ((num >= 15) && (num < 30))
            {
                if (this.blnTurn)
                {
                    if (((this.aHome.pDCP != null) && (this.aHome.pDCP.intStatus != 2)) && (this.rnd.Next(this.aHome.pDCP.intPower, 100) < 90))
                    {
                        this.aHome.pDCP.blnInjured = true;
                        this.aHome.pDCP.intOffAbility /= 2;
                        this.aHome.pDCP.intDefAbility /= 2;
                        this.sbScript.Append("<Script>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aHome.pDCP.strName);
                        this.sbScript.Append("受伤，能力减少50%！&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                    }
                }
                else if (((this.aAway.pDCP != null) && (this.aAway.pDCP.intStatus != 2)) && (this.rnd.Next(this.aAway.pDCP.intPower, 100) < 90))
                {
                    this.aAway.pDCP.blnInjured = true;
                    this.aAway.pDCP.intOffAbility /= 2;
                    this.aAway.pDCP.intDefAbility /= 2;
                    this.sbScript.Append("<Script>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aAway.pDCP.strName);
                    this.sbScript.Append("受伤，能力减少50%！&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                }
            }
        }

        public void SetPlayerOnInjure()
        {
            IDictionaryEnumerator enumerator = null;
            enumerator = this.tHome.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ((Player) enumerator.Value).blnOnArrange = false;
            }
            this.aHome.pC.blnPlayed = true;
            this.aHome.pC.blnOnArrange = true;
            this.aHome.pF.blnPlayed = true;
            this.aHome.pF.blnOnArrange = true;
            this.aHome.pG.blnPlayed = true;
            this.aHome.pG.blnOnArrange = true;
            if (this.aHome.pC.blnInjured)
            {
                this.aHome.pC.intOffAbility /= 2;
                this.aHome.pC.intDefAbility /= 2;
            }
            if (this.aHome.pF.blnInjured)
            {
                this.aHome.pF.intOffAbility /= 2;
                this.aHome.pF.intDefAbility /= 2;
            }
            if (this.aHome.pG.blnInjured)
            {
                this.aHome.pG.intOffAbility /= 2;
                this.aHome.pG.intDefAbility /= 2;
            }
            enumerator = this.tAway.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ((Player) enumerator.Value).blnOnArrange = false;
            }
            this.aAway.pC.blnPlayed = true;
            this.aAway.pC.blnOnArrange = true;
            this.aAway.pF.blnPlayed = true;
            this.aAway.pF.blnOnArrange = true;
            this.aAway.pG.blnPlayed = true;
            this.aAway.pG.blnOnArrange = true;
            if (this.aAway.pC.blnInjured)
            {
                this.aAway.pC.intOffAbility /= 2;
                this.aAway.pC.intDefAbility /= 2;
            }
            if (this.aAway.pF.blnInjured)
            {
                this.aAway.pF.intOffAbility /= 2;
                this.aAway.pF.intDefAbility /= 2;
            }
            if (this.aAway.pG.blnInjured)
            {
                this.aAway.pG.intOffAbility /= 2;
                this.aAway.pG.intDefAbility /= 2;
            }
        }
    }
}

