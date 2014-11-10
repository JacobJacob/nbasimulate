namespace Web.VMatchEngine
{
    using System;
    using System.Collections;
    using System.Text;
    using Web.Helper;
    using Web.Lang;
    using Web.Util;

    public class Quarter
    {
        private Arrange aAway;
        private Arrange[] aAways;
        private Arrange aHome;
        private Arrange[] aHomes;
        public bool blnEnd;
        public bool blnPower;
        public bool blnStart;
        public bool blnTTurn;
        public bool blnTurn;
        private byte byAllAddA;
        private byte byAllAddH;
        private Context context;
        private int intArrangeID;
        private int intQNum;
        private int intQNumC;
        private int intScriptID;
        private int intType;
        public int matchId;
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
            this.intScriptID = 1;
            this.intType = 0;
            this.sbScript = new StringBuilder();
            this.sbQuarter = new StringBuilder();
            this.sbArrange = new StringBuilder();
            this.sbPlayer = new StringBuilder();
            this.byAllAddH = 0;
            this.byAllAddA = 0;
            this.rnd = new Random(DateTime.Now.Millisecond);
        }

        public Quarter(Context context, int intMinutes, int intQNum, Team tHome, Team tAway, Arrange[] aHomes, Arrange[] aAways, bool blnPower, byte byAllAddH, byte byAllAddA, int intType, int matchId)
        {
            this.context = context;
            this.intScriptID = 1;
            this.intType = 0;
            this.sbScript = new StringBuilder();
            this.sbQuarter = new StringBuilder();
            this.sbArrange = new StringBuilder();
            this.sbPlayer = new StringBuilder();
            this.byAllAddH = 0;
            this.byAllAddA = 0;
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
            this.blnTTurn = true;
            this.blnEnd = false;
            this.timer = new Timer(intMinutes);
            this.intArrangeID = (this.intQNumC * 2) - 1;
            this.byAllAddH = byAllAddH;
            this.byAllAddA = byAllAddA;
            this.intType = intType;
            this.matchId = matchId;
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

        public bool AddFoulStatus(bool blnIsOff)
        {
            if (blnIsOff)
            {
                if (this.blnTurn)
                {
                    this.aHome.pOCP.intFoul++;
                    if (this.aHome.pOCP.intFoul >= 6)
                    {
                        this.aHome.pOCP.blnOut = true;
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aHome.pOCP.strName);
                        this.sbScript.Append("犯满离场。&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        return this.PlayOut(true, this.aHome.pOCP, this.blnPower);
                    }
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                    this.sbScript.Append(this.aHome.pOCP.strName);
                    this.sbScript.Append("犯规");
                    this.sbScript.Append(this.aHome.pOCP.intFoul);
                    this.sbScript.Append("次。&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                }
                else
                {
                    this.aAway.pOCP.intFoul++;
                    if (this.aAway.pOCP.intFoul >= 6)
                    {
                        this.aAway.pOCP.blnOut = true;
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aAway.pOCP.strName);
                        this.sbScript.Append("犯满离场。&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        return this.PlayOut(false, this.aAway.pOCP, this.blnPower);
                    }
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                    this.sbScript.Append(this.aAway.pOCP.strName);
                    this.sbScript.Append("犯规");
                    this.sbScript.Append(this.aAway.pOCP.intFoul);
                    this.sbScript.Append("次。&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                }
            }
            else if (this.blnTurn)
            {
                this.aAway.pDCP.intFoul++;
                if (this.aAway.pDCP.intFoul >= 6)
                {
                    this.aAway.pDCP.blnOut = true;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aAway.pDCP.strName);
                    this.sbScript.Append("犯满离场。&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    return this.PlayOut(false, this.aAway.pDCP, this.blnPower);
                }
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                this.sbScript.Append(this.aAway.pDCP.strName);
                this.sbScript.Append("犯规");
                this.sbScript.Append(this.aAway.pDCP.intFoul);
                this.sbScript.Append("次。&lt;/font&gt;</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            else
            {
                this.aHome.pDCP.intFoul++;
                if (this.aHome.pDCP.intFoul >= 6)
                {
                    this.aHome.pDCP.blnOut = true;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aHome.pDCP.strName);
                    this.sbScript.Append("犯满离场。&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    return this.PlayOut(true, this.aHome.pDCP, this.blnPower);
                }
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>&lt;font color=blue&gt;");
                this.sbScript.Append(this.aHome.pDCP.strName);
                this.sbScript.Append("犯规");
                this.sbScript.Append(this.aHome.pDCP.intFoul);
                this.sbScript.Append("次。&lt;/font&gt;</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            return true;
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

        private void DoAwayFailOff(bool blnIsNextOff)
        {
            int num = this.rnd.Next(0, 100);
            int intOffMethod = this.aAway.intOffMethod;
            if (blnIsNextOff)
            {
                this.timer.Next(false, true);
            }
            else if (intOffMethod == 9)
            {
                this.timer.Next(true, false);
            }
            else
            {
                this.timer.Next(false, false);
            }
            bool isAssist = this.GetIsAssist(intOffMethod);
            if (num < 3)
            {
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(ScriptGenerator.GetOffFoul(intOffMethod, this.aAway.pOCP, this.aHome.pDCP));
                this.sbScript.Append("</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (!this.AddFoulStatus(true))
                {
                    this.RunOneRound(false);
                }
                else
                {
                    this.blnTurn = !this.blnTurn;
                }
            }
            else if ((num >= 3) && (num < 10))
            {
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
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
                this.intScriptID++;
                if (!this.AddFoulStatus(false))
                {
                    this.RunOneRound(false);
                }
            }
            else if ((num >= 10) && (num < 0x19))
            {
                int num3 = this.rnd.Next(0, 100);
                if (this.aHome.pDCP.intPos > 2)
                {
                    if (num3 < 30)
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        this.AddStealStatus();
                        this.AddToStatus();
                        this.blnTurn = !this.blnTurn;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime() + "</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetTo(this.aAway.pOCP, this.aHome.pDCP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.AddToStatus();
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else if (num3 < 8)
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    this.AddStealStatus();
                    this.AddToStatus();
                    this.blnTurn = !this.blnTurn;
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    this.AddToStatus();
                    this.blnTurn = !this.blnTurn;
                }
            }
            else
            {
                int num4 = this.rnd.Next(0, 100);
                if (this.aHome.pDCP.intPos < 4)
                {
                    if ((num4 < 0x12) && (this.aHome.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                    {
                        this.AddOffStatus(intOffMethod, false, false);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetBlock(intOffMethod, this.aAway.pACP, this.aAway.pOCP, this.aHome.pDCP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.AddBlockStatus();
                        if (this.GetAfterBlk())
                        {
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.blnTurn = !this.blnTurn;
                        }
                    }
                    else
                    {
                        this.AddOffStatus(intOffMethod, false, false);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        if (this.GetReb())
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.blnTurn = !this.blnTurn;
                        }
                    }
                }
                else if ((num4 < 3) && (this.aHome.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                {
                    this.AddOffStatus(intOffMethod, false, false);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetBlock(intOffMethod, this.aAway.pACP, this.aAway.pOCP, this.aHome.pDCP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.AddBlockStatus();
                    if (this.GetAfterBlk())
                    {
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else
                {
                    this.AddOffStatus(intOffMethod, false, false);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
            }
        }

        private void DoAwaySuccessOff(bool blnIsNextOff)
        {
            int num = this.rnd.Next(0, 100);
            int intOffMethod = this.aAway.intOffMethod;
            if (blnIsNextOff)
            {
                this.timer.Next(false, true);
            }
            else if (intOffMethod == 9)
            {
                this.timer.Next(true, false);
            }
            else
            {
                this.timer.Next(false, false);
            }
            bool isAssist = this.GetIsAssist(intOffMethod);
            if (num < 0x58)
            {
                if (intOffMethod == 5)
                {
                    if ((((this.aAway.pOCP.intPoint3 * 0x12) - (((this.aAway.pOCP.intPoint3 * this.aAway.pOCP.intPoint3) * 9) / 0x3e8)) / 100) > this.rnd.Next(0, 100))
                    {
                        this.AddOffStatus(intOffMethod, true, isAssist);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        if (this.GetReb())
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
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
                else
                {
                    this.AddOffStatus(intOffMethod, true, isAssist);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.blnTurn = !this.blnTurn;
                }
            }
            else if ((num >= 0x58) && (num < 90))
            {
                this.AddOffStatus(intOffMethod, true, isAssist);
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(ScriptGenerator.GetGoodOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                this.sbScript.Append("</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>同时造成");
                this.sbScript.Append(this.aHome.pDCP.strName);
                this.sbScript.Append("犯规，加罚1次。</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (!this.AddFoulStatus(false))
                {
                    this.RunOneRound(false);
                }
                else if (this.GetFT(1))
                {
                    this.blnTurn = !this.blnTurn;
                }
                else if (this.GetReb())
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.RunOneRound(true);
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.blnTurn = !this.blnTurn;
                }
            }
            else
            {
                this.AddOffStatus(intOffMethod, false, false);
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(ScriptGenerator.GetBadOff(intOffMethod, this.aAway.pOCP, this.aAway.pACP, this.aHome.pDCP, isAssist));
                this.sbScript.Append("</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (intOffMethod == 5)
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>同时造成");
                    this.sbScript.Append(this.aHome.pDCP.strName);
                    this.sbScript.Append("的犯规，罚球3次</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (!this.AddFoulStatus(false))
                    {
                        this.RunOneRound(false);
                    }
                    else if (this.GetFT(3))
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                    else if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>同时造成");
                    this.sbScript.Append(this.aHome.pDCP.strName);
                    this.sbScript.Append("的犯规，罚球2次</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (!this.AddFoulStatus(false))
                    {
                        this.RunOneRound(false);
                    }
                    else if (this.GetFT(2))
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                    else if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aAway.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aHome.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
            }
        }

        private void DoFailOff(bool blnIsNextOff)
        {
            int num = this.rnd.Next(0, 100);
            int intOffMethod = this.aHome.intOffMethod;
            if (blnIsNextOff)
            {
                this.timer.Next(false, true);
            }
            else if (intOffMethod == 9)
            {
                this.timer.Next(true, false);
            }
            else
            {
                this.timer.Next(false, false);
            }
            bool isAssist = this.GetIsAssist(intOffMethod);
            if (num < 3)
            {
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(ScriptGenerator.GetOffFoul(intOffMethod, this.aHome.pOCP, this.aAway.pDCP));
                this.sbScript.Append("</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (!this.AddFoulStatus(true))
                {
                    this.RunOneRound(false);
                }
                else
                {
                    this.blnTurn = !this.blnTurn;
                }
            }
            else if ((num >= 3) && (num < 10))
            {
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
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
                this.intScriptID++;
                if (!this.AddFoulStatus(false))
                {
                    this.RunOneRound(false);
                }
            }
            else if ((num >= 10) && (num < 0x19))
            {
                int num3 = this.rnd.Next(0, 100);
                if (this.aAway.pDCP.intPos > 2)
                {
                    if (num3 < 30)
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        this.AddStealStatus();
                        this.AddToStatus();
                        this.blnTurn = !this.blnTurn;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        this.AddToStatus();
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else if (num3 < 8)
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    this.AddStealStatus();
                    this.AddToStatus();
                    this.blnTurn = !this.blnTurn;
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    this.AddToStatus();
                    this.blnTurn = !this.blnTurn;
                }
            }
            else
            {
                int num4 = this.rnd.Next(0, 100);
                if (this.aAway.pDCP.intPos < 4)
                {
                    if ((num4 < 0x12) && (this.aAway.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                    {
                        this.AddOffStatus(intOffMethod, false, false);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time>");
                        this.sbScript.Append(this.timer.GetTime());
                        this.sbScript.Append("</Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetBlock(intOffMethod, this.aHome.pACP, this.aHome.pOCP, this.aAway.pDCP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score>");
                        this.sbScript.Append(this.tHome.intScore);
                        this.sbScript.Append(":");
                        this.sbScript.Append(this.tAway.intScore);
                        this.sbScript.Append("</Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.AddBlockStatus();
                        if (this.GetAfterBlk())
                        {
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.blnTurn = !this.blnTurn;
                        }
                    }
                    else
                    {
                        this.AddOffStatus(intOffMethod, false, false);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        if (this.GetReb())
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.blnTurn = !this.blnTurn;
                        }
                    }
                }
                else if ((num4 < 3) && (this.aAway.pDCP.intBlockAbility > this.rnd.Next(500, 0x7d0)))
                {
                    this.AddOffStatus(intOffMethod, false, false);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetBlock(intOffMethod, this.aHome.pACP, this.aHome.pOCP, this.aAway.pDCP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.AddBlockStatus();
                    if (this.GetAfterBlk())
                    {
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else
                {
                    this.AddOffStatus(intOffMethod, false, false);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
            }
        }

        private void DoSuccessOff(bool blnIsNextOff)
        {
            int num = this.rnd.Next(0, 100);
            int intOffMethod = this.aHome.intOffMethod;
            if (blnIsNextOff)
            {
                this.timer.Next(false, true);
            }
            else if (intOffMethod == 9)
            {
                this.timer.Next(true, false);
            }
            else
            {
                this.timer.Next(false, false);
            }
            bool isAssist = this.GetIsAssist(intOffMethod);
            if (num < 0x58)
            {
                if (intOffMethod == 5)
                {
                    if ((((this.aHome.pOCP.intPoint3 * 0x12) - (((this.aHome.pOCP.intPoint3 * this.aHome.pOCP.intPoint3) * 9) / 0x3e8)) / 100) > this.rnd.Next(0, 100))
                    {
                        this.AddOffStatus(intOffMethod, true, isAssist);
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        if (this.GetReb())
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.RunOneRound(true);
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
                            this.sbScript.Append("\t\t<QuarterID>");
                            this.sbScript.Append(this.intQNumC);
                            this.sbScript.Append("</QuarterID>");
                            this.sbScript.Append("\t\t<Time></Time>");
                            this.sbScript.Append("\t\t<Content>");
                            this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                            this.sbScript.Append("</Content>");
                            this.sbScript.Append("\t\t<Score></Score>");
                            this.sbScript.Append("\t</Script>");
                            this.intScriptID++;
                            this.blnTurn = !this.blnTurn;
                        }
                    }
                }
                else
                {
                    this.AddOffStatus(intOffMethod, true, isAssist);
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
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
                    this.intScriptID++;
                    this.blnTurn = !this.blnTurn;
                }
            }
            else if ((num >= 0x58) && (num < 90))
            {
                this.AddOffStatus(intOffMethod, true, isAssist);
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
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
                this.intScriptID++;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>同时造成");
                this.sbScript.Append(this.aAway.pDCP.strName);
                this.sbScript.Append("犯规，加罚一次。</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (!this.AddFoulStatus(false))
                {
                    this.RunOneRound(false);
                }
                else if (this.GetFT(1))
                {
                    this.blnTurn = !this.blnTurn;
                }
                else if (this.GetReb())
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.RunOneRound(true);
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                    this.sbScript.Append("</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    this.blnTurn = !this.blnTurn;
                }
            }
            else
            {
                this.AddOffStatus(intOffMethod, false, false);
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
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
                this.intScriptID++;
                if (intOffMethod == 5)
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>但造成");
                    this.sbScript.Append(this.aAway.pDCP.strName);
                    this.sbScript.Append("的犯规，罚球3次。</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (!this.AddFoulStatus(false))
                    {
                        this.RunOneRound(false);
                    }
                    else if (this.GetFT(3))
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                    else if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetOffReb(this.aHome.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>但造成");
                    this.sbScript.Append(this.aAway.pDCP.strName);
                    this.sbScript.Append("的犯规，罚球2次</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (!this.AddFoulStatus(false))
                    {
                        this.RunOneRound(false);
                    }
                    else if (this.GetFT(2))
                    {
                        this.blnTurn = !this.blnTurn;
                    }
                    else if (this.GetReb())
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>" + ScriptGenerator.GetOffReb(this.aHome.pORP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.RunOneRound(true);
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>");
                        this.sbScript.Append(ScriptGenerator.GetDefReb(this.aAway.pDRP));
                        this.sbScript.Append("</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        this.blnTurn = !this.blnTurn;
                    }
                }
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
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.aHome.pBBP.strName);
                    this.sbScript.Append("得到球，继续进攻！</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    return flag;
                }
                return false;
            }
            int blkBallAbility = this.aAway.GetBlkBallAbility();
            int num4 = this.aHome.GetBlkBallAbility();
            if (blkBallAbility > this.rnd.Next(0, blkBallAbility + (num4 * 5)))
            {
                flag = true;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aAway.pBBP.strName);
                this.sbScript.Append("得到球，继续进攻！</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
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
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
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
                            this.intScriptID++;
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
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
                            this.intScriptID++;
                        }
                    }
                    else
                    {
                        this.AddFTStatus(false);
                        if (intMethod == 1)
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
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
                            this.intScriptID++;
                            flag = false;
                        }
                        else
                        {
                            this.sbScript.Append("<Script><ScriptID>");
                            this.sbScript.Append(this.intScriptID);
                            this.sbScript.Append("</ScriptID>");
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
                            this.intScriptID++;
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
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                    }
                }
                else
                {
                    this.AddFTStatus(false);
                    if (intMethod == 1)
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
                        flag = false;
                    }
                    else
                    {
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
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
                        this.intScriptID++;
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
            else if (intOffMethod == 7)
            {
                if (num < 10)
                {
                    return true;
                }
            }
            else if ((intOffMethod == 9) && (num < 15))
            {
                return true;
            }
            return false;
        }

        private int GetMethodAdd(int intOffense, int intDefense)
        {
            int[,] numArray = new int[,] { { 0x6a, 0x5e, 0x61, 0x67, 0x70, 0x61 }, { 0x70, 0x61, 0x67, 0x61, 0x6a, 0x5e }, { 0x5e, 0x6a, 0x61, 0x67, 0x61, 0x70 }, { 0x61, 0x61, 0x5e, 0x70, 0x6a, 0x6a }, { 0x6a, 0x6a, 0x70, 0x5e, 0x61, 0x61 }, { 0x61, 0x70, 0x67, 0x61, 0x5e, 0x6a } };
            return numArray[intOffense - 1, intDefense - 1];
        }

        public int GetMethodAddForMatch(int intOffense, int intDefense)
        {
            int[,] numArray = new int[,] { { 0x6a, 0x5e, 0x61, 0x67, 0x70, 0x61 }, { 0x70, 0x61, 0x67, 0x61, 0x6a, 0x5e }, { 0x5e, 0x6a, 0x61, 0x67, 0x61, 0x70 }, { 0x61, 0x61, 0x5e, 0x70, 0x6a, 0x6a }, { 0x6a, 0x6a, 0x70, 0x5e, 0x61, 0x61 }, { 0x61, 0x70, 0x67, 0x61, 0x5e, 0x6a } };
            return numArray[intOffense - 1, intDefense - 1];
        }

        private int GetOCPOffAbility(Player p, int intOffMethod)
        {
            if ((intOffMethod == 1) || (intOffMethod == 2))
            {
                if ((p.intPos == 1) || (p.intPos == 2))
                {
                    return (p.intOffAbility * 0x23);
                }
                if (p.intPos == 3)
                {
                    return (p.intOffAbility * 0x21);
                }
                if (p.intPos == 4)
                {
                    return (p.intOffAbility * 30);
                }
                return (p.intOffAbility * 0x19);
            }
            if ((intOffMethod == 3) || (intOffMethod == 4))
            {
                if (p.intPos == 1)
                {
                    int intOffAbility = p.intOffAbility;
                }
                if (p.intPos == 2)
                {
                    return (p.intOffAbility * 30);
                }
                if (p.intPos == 3)
                {
                    return (p.intOffAbility * 0x21);
                }
                return (p.intOffAbility * 0x23);
            }
            if (intOffMethod == 5)
            {
                if (p.intPos == 1)
                {
                    return (p.intOffAbility * 0x19);
                }
                if (p.intPos == 2)
                {
                    return (p.intOffAbility * 30);
                }
            }
            return (p.intOffAbility * 0x23);
        }

        public Player GetOnPlayer(Team team, int intPos)
        {
            Player player = null;
            Player player2 = null;
            int num = 5;
            int intAbility = 0;
            int num3 = 0;
            int num4 = 15;
            IDictionaryEnumerator enumerator = team.players.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (num4 < 0)
                {
                    break;
                }
                player2 = (Player) enumerator.Value;
                if (!player2.blnOut && !player2.blnOnArrange)
                {
                    num3 = Math.Abs((int) (intPos - player2.intPos));
                    if (num3 < num)
                    {
                        num = num3;
                        player = player2;
                    }
                    if ((num3 == num) && (player2.intAbility > intAbility))
                    {
                        intAbility = player2.intAbility;
                        player = player2;
                    }
                }
                num4--;
            }
            if (player != null)
            {
                player.SetAbility(intPos);
                return player;
            }
            return null;
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

        public bool PlayOut(bool blnIsHome, Player player, bool blnPower)
        {
            int intPos = player.intPos;
            Player onPlayer = null;
            if (blnIsHome)
            {
                onPlayer = this.GetOnPlayer(this.tHome, intPos);
                switch (intPos)
                {
                    case 1:
                        this.aHome.pC = onPlayer;
                        goto Label_00F3;

                    case 2:
                        this.aHome.pPF = onPlayer;
                        goto Label_00F3;

                    case 3:
                        this.aHome.pSF = onPlayer;
                        goto Label_00F3;

                    case 4:
                        this.aHome.pSG = onPlayer;
                        goto Label_00F3;
                }
                this.aHome.pPG = onPlayer;
            }
            else
            {
                onPlayer = this.GetOnPlayer(this.tAway, intPos);
                switch (intPos)
                {
                    case 1:
                        this.aAway.pC = onPlayer;
                        goto Label_00F3;

                    case 2:
                        this.aAway.pPF = onPlayer;
                        goto Label_00F3;

                    case 3:
                        this.aAway.pSF = onPlayer;
                        goto Label_00F3;

                    case 4:
                        this.aAway.pSG = onPlayer;
                        goto Label_00F3;
                }
                this.aAway.pPG = onPlayer;
            }
        Label_00F3:
            if (onPlayer == null)
            {
                if (blnIsHome)
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.tHome.strClubName);
                    this.sbScript.Append("没有队员可以上场，输掉比赛！</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (this.tHome.intScore >= this.tAway.intScore)
                    {
                        this.tHome.intScore = 0;
                        this.tAway.intScore = 2;
                    }
                }
                else
                {
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.tAway.strClubName);
                    this.sbScript.Append("没有队员可以上场，输掉比赛！</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    if (this.tAway.intScore >= this.tHome.intScore)
                    {
                        this.tHome.intScore = 2;
                        this.tAway.intScore = 0;
                    }
                }
                this.timer.SetFinished();
                this.blnEnd = true;
                return false;
            }
            this.sbScript.Append("<Script><ScriptID>");
            this.sbScript.Append(this.intScriptID);
            this.sbScript.Append("</ScriptID>");
            this.sbScript.Append("\t\t<QuarterID>");
            this.sbScript.Append(this.intQNumC);
            this.sbScript.Append("</QuarterID>");
            this.sbScript.Append("\t\t<Time></Time>");
            this.sbScript.Append("\t\t<Content>");
            this.sbScript.Append(onPlayer.strName + "替换上场。</Content>");
            this.sbScript.Append("\t\t<Score></Score>");
            this.sbScript.Append("\t</Script>");
            this.intScriptID++;
            return true;
        }

        public void Run()
        {
            while (!this.timer.IsFinished())
            {
                this.RunOneRound(false);
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

        public void RunFirstStart()
        {
            if (this.intQNum > 5)
            {
                this.intQNum = 5;
            }
            int num = this.tHome.intScore - this.tAway.intScore;
            if (num >= 15)
            {
                if (this.tHome.intWUse == 0)
                {
                    this.aHome = this.aHomes[5];
                }
                else
                {
                    this.aHome = this.aHomes[this.intQNum - 1];
                }
                if (this.tAway.intLUse == 0)
                {
                    this.aAway = this.aAways[6];
                }
                else
                {
                    this.aAway = this.aAways[this.intQNum - 1];
                }
            }
            else if ((num < 15) && (num > -15))
            {
                this.aHome = this.aHomes[this.intQNum - 1];
                this.aAway = this.aAways[this.intQNum - 1];
            }
            else
            {
                if (this.tHome.intLUse == 0)
                {
                    this.aHome = this.aHomes[6];
                }
                else
                {
                    this.aHome = this.aHomes[this.intQNum - 1];
                }
                if (this.tAway.intWUse == 0)
                {
                    this.aAway = this.aAways[5];
                }
                else
                {
                    this.aAway = this.aAways[this.intQNum - 1];
                }
            }
            this.SetOnArrange();
            if (!this.SetPlayerOn())
            {
                this.RunOneRound(false);
            }
            else
            {
                this.SetArrangeAbility();
                this.sbArrange.Append("<Arrange ArrangeID=\"");
                this.sbArrange.Append(this.intArrangeID);
                this.sbArrange.Append("\">");
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
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aHome.pC, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aHome.pPF, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aHome.pSF, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aHome.pSG, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aHome.pPG, this.intArrangeID);
                this.intArrangeID++;
                this.sbArrange.Append("<Arrange ArrangeID=\"");
                this.sbArrange.Append(this.intArrangeID);
                this.sbArrange.Append("\">");
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
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aAway.pC, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aAway.pPF, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aAway.pSF, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aAway.pSG, this.intArrangeID);
                ReportHelper.BuildPlayerrReport(this.sbPlayer, this.aAway.pPG, this.intArrangeID);
            }
        }

        public void RunOneRound(bool blnIsNextOff)
        {
            if (this.timer.IsFinished())
            {
                return;
            }
            if (!this.blnStart)
            {
                this.RunFirstStart();
            }
            if (((this.intQNum == 1) || (this.intQNum > 4)) && !this.blnStart)
            {
                this.StartJumpBall();
                return;
            }
            if (!this.blnStart)
            {
                if (this.intQNum == 2)
                {
                    this.StartRound2();
                }
                else if (this.intQNum == 3)
                {
                    this.StartRound3();
                }
                else if (this.blnTTurn)
                {
                    this.blnTurn = false;
                    this.blnTTurn = false;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.aAway.pC.strName);
                    this.sbScript.Append("底线发球，比赛开始。</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                }
                else
                {
                    this.blnTurn = true;
                    this.blnTTurn = true;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time>");
                    this.sbScript.Append(this.timer.GetTime());
                    this.sbScript.Append("</Time>");
                    this.sbScript.Append("\t\t<Content>");
                    this.sbScript.Append(this.aHome.pC.strName);
                    this.sbScript.Append("底线发球，比赛开始。</Content>");
                    this.sbScript.Append("\t\t<Score>");
                    this.sbScript.Append(this.tHome.intScore);
                    this.sbScript.Append(":");
                    this.sbScript.Append(this.tAway.intScore);
                    this.sbScript.Append("</Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                }
                this.blnStart = true;
                return;
            }
            if (!this.blnTurn)
            {
                int offCheckValue = ((this.aAway.GetOffAbility() * 2) * (100 + this.byAllAddA)) / 100;
                int defCheckValue = (((this.aHome.GetDefAbility(this.aAway.pOCP, this.aAway.intOffMethod) * this.GetMethodAdd(this.aAway.intOffense, this.aHome.intDefense)) / 100) * (100 + this.byAllAddH)) / 100;
                int currentOffCheckValue = (this.GetOCPOffAbility(this.aAway.pOCP, this.aAway.intOffMethod) * (100 + this.byAllAddA)) / 100;
                if ((this.intType == 8) && ((offCheckValue < 0xa410) || (defCheckValue < 0xa410)))
                {
                    int num12 = 0;
                    if ((offCheckValue > defCheckValue) && ((this.tAway.intScore - this.tHome.intScore) > 5))
                    {
                        num12 = offCheckValue - defCheckValue;
                        if ((this.tAway.intScore - this.tHome.intScore) > 10)
                        {
                            offCheckValue -= num12;
                        }
                        else
                        {
                            offCheckValue -= (num12 * this.rnd.Next(70, 90)) / 100;
                        }
                    }
                    else if ((offCheckValue < defCheckValue) && ((this.tHome.intScore - this.tAway.intScore) > 5))
                    {
                        num12 = defCheckValue - offCheckValue;
                        if ((this.tHome.intScore - this.tAway.intScore) > 15)
                        {
                            defCheckValue -= num12;
                        }
                        else
                        {
                            defCheckValue -= (num12 * this.rnd.Next(70, 90)) / 100;
                        }
                    }
                }
                if ((this.tHome.intScore - this.tAway.intScore) > 12)
                {
                    offCheckValue = (offCheckValue + (this.aAway.GetHardnessTotal() * 10)) + (this.aAway.GetLeadshipTotal() * 10);
                }
                int teamValueCheckRnd = this.rnd.Next(0, offCheckValue + defCheckValue);
                int playerValueCheckRnd = this.rnd.Next(0, currentOffCheckValue + defCheckValue);
                this.context.MatchTotal.SetActionTotal(false, this.aAway.pOCP.strName, offCheckValue, defCheckValue, teamValueCheckRnd, currentOffCheckValue, playerValueCheckRnd);
                string logFile = Web.VMatchEngine.Logger.GetLogPath(this.intType, this.matchId, "log");
                object obj3 = string.Concat(new object[] { "客队进攻.当前进攻球员[", this.aAway.pOCP.strName, "], 当前客队进攻值[", offCheckValue, "], 当前主队防守值[", defCheckValue, "], 当前客队进攻球员进攻值[", currentOffCheckValue, "]" });
                string msg = string.Concat(new object[] { obj3, ", 当前球队随机值[", teamValueCheckRnd, "], 当前球员随机值[", playerValueCheckRnd, "]" });
                Web.VMatchEngine.Logger.WriteLog(logFile, msg);
                int methodAddForMatch = this.GetMethodAddForMatch(this.aAway.intOffense, this.aHome.intDefense);
                bool flag2 = MatchHelper.AbilityShotCheck(this.aAway.intAbility, this.aHome.intAbility, methodAddForMatch, this.tAway.intScore - this.tHome.intScore);
                Web.Util.Logger.Debug(msg);
                switch (this.context.MatchTotal.Check(false))
                {
                    case 1:
                        this.DoAwaySuccessOff(blnIsNextOff);
                        goto Label_0BF0;

                    case -1:
                        this.DoAwayFailOff(blnIsNextOff);
                        goto Label_0BF0;
                }
                if (((offCheckValue > teamValueCheckRnd) && (currentOffCheckValue > playerValueCheckRnd)) || flag2)
                {
                    Web.Util.Logger.Debug("客队进攻成功");
                    this.DoAwaySuccessOff(blnIsNextOff);
                }
                else
                {
                    Web.Util.Logger.Debug("客队进攻失败");
                    this.DoAwayFailOff(blnIsNextOff);
                }
            }
            else
            {
                int num = ((this.aHome.GetOffAbility() * 2) * (100 + this.byAllAddH)) / 100;
                int num2 = (((this.aAway.GetDefAbility(this.aHome.pOCP, this.aHome.intOffMethod) * this.GetMethodAdd(this.aHome.intOffense, this.aAway.intDefense)) / 100) * (100 + this.byAllAddA)) / 100;
                int num3 = (this.GetOCPOffAbility(this.aHome.pOCP, this.aHome.intOffMethod) * (100 + this.byAllAddH)) / 100;
                if ((this.intType == MatchType.MATCH_CATEGORY_ONLY_ONE) && ((num < 0xa410) || (num2 < 0xa410)))
                {
                    int num4 = 0;
                    if ((num > num2) && ((this.tHome.intScore - this.tAway.intScore) > 5))
                    {
                        num4 = num - num2;
                        if ((this.tHome.intScore - this.tAway.intScore) > 10)
                        {
                            num -= num4;
                        }
                        else
                        {
                            num -= (num4 * this.rnd.Next(70, 90)) / 100;
                        }
                    }
                    else if ((num < num2) && ((this.tAway.intScore - this.tHome.intScore) > 5))
                    {
                        num4 = num2 - num;
                        if ((this.tAway.intScore - this.tHome.intScore) > 15)
                        {
                            num2 -= num4;
                        }
                        else
                        {
                            num2 -= (num4 * this.rnd.Next(70, 90)) / 100;
                        }
                    }
                }
                if ((this.tAway.intScore - this.tHome.intScore) > 10)
                {
                    num = (num + (this.aHome.GetHardnessTotal() * 10)) + (this.aHome.GetLeadshipTotal() * 10);
                }
                int num5 = this.rnd.Next(0, num + num2);
                int num6 = this.rnd.Next(0, num3 + num2);
                this.context.MatchTotal.SetActionTotal(true, this.aHome.pOCP.strName, num, num2, num5, num3, num6);
                string str = Web.VMatchEngine.Logger.GetLogPath(this.intType, this.matchId, "log");
                object obj2 = string.Concat(new object[] { "主队进攻.当前进攻球员[", this.aHome.pOCP.strName, "], 当前主队进攻值[", num, "], 当前客队防守值[", num2, "], 当前主队进攻球员进攻值[", num3, "]" });
                string str2 = string.Concat(new object[] { obj2, ", 当前球队随机值[", num5, "], 当前球员随机值[", num6, "]" });
                Web.VMatchEngine.Logger.WriteLog(str, str2);
                int attackArrangeAdd = this.GetMethodAddForMatch(this.aHome.intOffense, this.aAway.intDefense);
                bool flag = MatchHelper.AbilityShotCheck(this.aHome.intAbility, this.aAway.intAbility, attackArrangeAdd, this.tHome.intScore - this.tAway.intScore);
                Web.Util.Logger.Debug(str2);
                switch (this.context.MatchTotal.Check(true))
                {
                    case 1:
                        this.DoSuccessOff(blnIsNextOff);
                        break;

                    case -1:
                        this.DoFailOff(blnIsNextOff);
                        break;

                    default:
                        if (((num > num5) && (num3 > num6)) || flag)
                        {
                            Web.Util.Logger.Debug("主队进攻成功");
                            this.DoSuccessOff(blnIsNextOff);
                        }
                        else
                        {
                            Web.Util.Logger.Debug("主队进攻成功");
                            this.DoFailOff(blnIsNextOff);
                        }
                        break;
                }
                this.aHome.pOCP.intPower -= MatchItem.GetVLosePower(this.aHome.pOCP.intStamina, this.aHome.intOffHard);
                if ((this.aHome.pOCP.intPower < 30) && (this.intType != 10))
                {
                    this.aHome.pOCP.intPower = 30;
                }
                else if ((this.aHome.pOCP.intPower < 0) && (this.intType == 10))
                {
                    this.aHome.pOCP.intPower = 0;
                }
                this.aAway.pDCP.intPower -= MatchItem.GetVLosePower(this.aAway.pDCP.intStamina, this.aAway.intDefHard);
                if ((this.aAway.pDCP.intPower < 30) && (this.intType != 10))
                {
                    this.aAway.pDCP.intPower = 30;
                }
                else if ((this.aAway.pDCP.intPower < 0) && (this.intType == 10))
                {
                    this.aAway.pDCP.intPower = 0;
                }
                goto Label_0D1E;
            }
        Label_0BF0:
            this.aAway.pOCP.intPower -= MatchItem.GetVLosePower(this.aAway.pOCP.intStamina, this.aAway.intOffHard);
            if ((this.aAway.pOCP.intPower < 30) && (this.intType != 10))
            {
                this.aAway.pOCP.intPower = 30;
            }
            else if ((this.aAway.pOCP.intPower < 0) && (this.intType == 10))
            {
                this.aAway.pOCP.intPower = 0;
            }
            this.aHome.pDCP.intPower -= MatchItem.GetVLosePower(this.aHome.pDCP.intStamina, this.aHome.intDefHard);
            if ((this.aHome.pDCP.intPower < 30) && (this.intType != 10))
            {
                this.aHome.pDCP.intPower = 30;
            }
            else if ((this.aHome.pDCP.intPower < 0) && (this.intType == 10))
            {
                this.aHome.pDCP.intPower = 0;
            }
        Label_0D1E:
            if (this.blnPower && !this.SetPlayerInjure())
            {
                this.RunOneRound(false);
            }
        }

        public void SetArrangeAbility()
        {
            this.aHome.pC.SetAbility(1);
            this.aHome.pPF.SetAbility(2);
            this.aHome.pSF.SetAbility(3);
            this.aHome.pSG.SetAbility(4);
            this.aHome.pPG.SetAbility(5);
            this.aAway.pC.SetAbility(1);
            this.aAway.pPF.SetAbility(2);
            this.aAway.pSF.SetAbility(3);
            this.aAway.pSG.SetAbility(4);
            this.aAway.pPG.SetAbility(5);
            ASObject aso = new ASObject();
            aso["Name"] = this.aHome.pC.strName;
            aso["OffAbility"] = this.aHome.pC.intOffAbility;
            aso["DefAbility"] = this.aHome.pC.intDefAbility;
            aso["Pos"] = "中锋";
            this.context.MatchTotal.SetAbility(true, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aHome.pPF.strName;
            aso["OffAbility"] = this.aHome.pPF.intOffAbility;
            aso["DefAbility"] = this.aHome.pPF.intDefAbility;
            aso["Pos"] = "大前";
            this.context.MatchTotal.SetAbility(true, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aHome.pSF.strName;
            aso["OffAbility"] = this.aHome.pSF.intOffAbility;
            aso["DefAbility"] = this.aHome.pSF.intDefAbility;
            aso["Pos"] = "小前";
            this.context.MatchTotal.SetAbility(true, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aHome.pSG.strName;
            aso["OffAbility"] = this.aHome.pSG.intOffAbility;
            aso["DefAbility"] = this.aHome.pSG.intDefAbility;
            aso["Pos"] = "分卫";
            this.context.MatchTotal.SetAbility(true, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aHome.pPG.strName;
            aso["OffAbility"] = this.aHome.pPG.intOffAbility;
            aso["DefAbility"] = this.aHome.pPG.intDefAbility;
            aso["Pos"] = "控卫";
            this.context.MatchTotal.SetAbility(true, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aAway.pC.strName;
            aso["OffAbility"] = this.aAway.pC.intOffAbility;
            aso["DefAbility"] = this.aAway.pC.intDefAbility;
            aso["Pos"] = "中锋";
            this.context.MatchTotal.SetAbility(false, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aAway.pPF.strName;
            aso["OffAbility"] = this.aAway.pPF.intOffAbility;
            aso["DefAbility"] = this.aAway.pPF.intDefAbility;
            aso["Pos"] = "大前";
            this.context.MatchTotal.SetAbility(false, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aAway.pSF.strName;
            aso["OffAbility"] = this.aAway.pSF.intOffAbility;
            aso["DefAbility"] = this.aAway.pSF.intDefAbility;
            aso["Pos"] = "小前";
            this.context.MatchTotal.SetAbility(false, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aAway.pSG.strName;
            aso["OffAbility"] = this.aAway.pSG.intOffAbility;
            aso["DefAbility"] = this.aAway.pSG.intDefAbility;
            aso["Pos"] = "分卫";
            this.context.MatchTotal.SetAbility(false, this.intQNum, aso);
            aso = new ASObject();
            aso["Name"] = this.aAway.pPG.strName;
            aso["OffAbility"] = this.aAway.pPG.intOffAbility;
            aso["DefAbility"] = this.aAway.pPG.intDefAbility;
            aso["Pos"] = "控卫";
            this.context.MatchTotal.SetAbility(false, this.intQNum, aso);
            int num = 100 - this.GetMethodAddForMatch(this.aHome.intOffense, this.aAway.intDefense);
            int num2 = 100 - this.GetMethodAddForMatch(this.aAway.intOffense, this.aHome.intDefense);
            aso = new ASObject();
            aso["homeArrangeAdd"] = num;
            aso["awayArrangeAdd"] = num2;
            this.context.MatchTotal.SetArrangeInfo(this.intQNum, aso);
        }

        public void SetOnArrange()
        {
            if (this.aHome.pC != null)
            {
                this.aHome.pC.blnOnArrange = true;
            }
            if (this.aHome.pPF != null)
            {
                this.aHome.pPF.blnOnArrange = true;
            }
            if (this.aHome.pSF != null)
            {
                this.aHome.pSF.blnOnArrange = true;
            }
            if (this.aHome.pSG != null)
            {
                this.aHome.pSG.blnOnArrange = true;
            }
            if (this.aHome.pPG != null)
            {
                this.aHome.pPG.blnOnArrange = true;
            }
            if (this.aAway.pC != null)
            {
                this.aAway.pC.blnOnArrange = true;
            }
            if (this.aAway.pPF != null)
            {
                this.aAway.pPF.blnOnArrange = true;
            }
            if (this.aAway.pSF != null)
            {
                this.aAway.pSF.blnOnArrange = true;
            }
            if (this.aAway.pSG != null)
            {
                this.aAway.pSG.blnOnArrange = true;
            }
            if (this.aAway.pPG != null)
            {
                this.aAway.pPG.blnOnArrange = true;
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

        public bool SetPlayerInjure()
        {
            int num = this.rnd.Next(0, 0x2ee0);
            int num2 = 0x5f;
            if (num < 15)
            {
                if (!this.blnTurn)
                {
                    if ((this.aHome.pOCP != null) && (this.rnd.Next(this.aHome.pOCP.intPower, 100) < num2))
                    {
                        this.aHome.pOCP.blnInjured = true;
                        this.aHome.pOCP.blnOut = true;
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aHome.pOCP.strName);
                        this.sbScript.Append("受伤！&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        return this.PlayOut(true, this.aHome.pOCP, this.blnPower);
                    }
                }
                else if ((this.aAway.pOCP != null) && (this.rnd.Next(this.aAway.pOCP.intPower, 100) < num2))
                {
                    this.aAway.pOCP.blnInjured = true;
                    this.aAway.pOCP.blnOut = true;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aAway.pOCP.strName);
                    this.sbScript.Append("受伤！&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    return this.PlayOut(false, this.aAway.pOCP, this.blnPower);
                }
            }
            else if ((num >= 15) && (num < 30))
            {
                if (this.blnTurn)
                {
                    if ((this.aHome.pDCP != null) && (this.rnd.Next(this.aHome.pDCP.intPower, 100) < num2))
                    {
                        this.aHome.pDCP.blnInjured = true;
                        this.aHome.pDCP.blnOut = true;
                        this.sbScript.Append("<Script><ScriptID>");
                        this.sbScript.Append(this.intScriptID);
                        this.sbScript.Append("</ScriptID>");
                        this.sbScript.Append("\t\t<QuarterID>");
                        this.sbScript.Append(this.intQNumC);
                        this.sbScript.Append("</QuarterID>");
                        this.sbScript.Append("\t\t<Time></Time>");
                        this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                        this.sbScript.Append(this.aHome.pDCP.strName);
                        this.sbScript.Append("受伤！&lt;/font&gt;</Content>");
                        this.sbScript.Append("\t\t<Score></Score>");
                        this.sbScript.Append("\t</Script>");
                        this.intScriptID++;
                        return this.PlayOut(true, this.aHome.pDCP, this.blnPower);
                    }
                }
                else if ((this.aAway.pDCP != null) && (this.rnd.Next(this.aAway.pDCP.intPower, 100) < num2))
                {
                    this.aAway.pDCP.blnInjured = true;
                    this.aAway.pDCP.blnOut = true;
                    this.sbScript.Append("<Script><ScriptID>");
                    this.sbScript.Append(this.intScriptID);
                    this.sbScript.Append("</ScriptID>");
                    this.sbScript.Append("\t\t<QuarterID>");
                    this.sbScript.Append(this.intQNumC);
                    this.sbScript.Append("</QuarterID>");
                    this.sbScript.Append("\t\t<Time></Time>");
                    this.sbScript.Append("\t\t<Content>&lt;font color=red&gt;");
                    this.sbScript.Append(this.aAway.pDCP.strName);
                    this.sbScript.Append("受伤！&lt;/font&gt;</Content>");
                    this.sbScript.Append("\t\t<Score></Score>");
                    this.sbScript.Append("\t</Script>");
                    this.intScriptID++;
                    return this.PlayOut(false, this.aAway.pDCP, this.blnPower);
                }
            }
            return true;
        }

        public bool SetPlayerOn()
        {
            if (this.aHome.pC == null)
            {
                this.aHome.pC = this.GetOnPlayer(this.tHome, 1);
            }
            else if (this.aHome.pC.blnOut)
            {
                this.aHome.pC = this.GetOnPlayer(this.tHome, 1);
            }
            if (this.aHome.pPF == null)
            {
                this.aHome.pPF = this.GetOnPlayer(this.tHome, 2);
            }
            else if (this.aHome.pPF.blnOut)
            {
                this.aHome.pPF = this.GetOnPlayer(this.tHome, 2);
            }
            if (this.aHome.pSF == null)
            {
                this.aHome.pSF = this.GetOnPlayer(this.tHome, 3);
            }
            else if (this.aHome.pSF.blnOut)
            {
                this.aHome.pSF = this.GetOnPlayer(this.tHome, 3);
            }
            if (this.aHome.pSG == null)
            {
                this.aHome.pSG = this.GetOnPlayer(this.tHome, 4);
            }
            else if (this.aHome.pSG.blnOut)
            {
                this.aHome.pSG = this.GetOnPlayer(this.tHome, 4);
            }
            if (this.aHome.pPG == null)
            {
                this.aHome.pPG = this.GetOnPlayer(this.tHome, 5);
            }
            else if (this.aHome.pPG.blnOut)
            {
                this.aHome.pPG = this.GetOnPlayer(this.tHome, 5);
            }
            if (((this.aHome.pPG == null) || (this.aHome.pSG == null)) || (((this.aHome.pSF == null) || (this.aHome.pPF == null)) || (this.aHome.pC == null)))
            {
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.tHome.strClubName);
                this.sbScript.Append("没有队员可以上场，输掉比赛！</Content>");
                this.sbScript.Append("\t\t<Score></Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
                if (this.tHome.intScore >= this.tAway.intScore)
                {
                    this.tHome.intScore = 0;
                    this.tAway.intScore = 2;
                }
                this.timer.SetFinished();
                this.blnEnd = true;
                return false;
            }
            this.aHome.intAbility = (((this.aHome.pC.intAbility + this.aHome.pPF.intAbility) + this.aHome.pSF.intAbility) + this.aHome.pSG.intAbility) + this.aHome.pPG.intAbility;
            this.aHome.intAbility /= 5;
            if (this.aAway.pC == null)
            {
                this.aAway.pC = this.GetOnPlayer(this.tAway, 1);
            }
            else if (this.aAway.pC.blnOut)
            {
                this.aAway.pC = this.GetOnPlayer(this.tAway, 1);
            }
            if (this.aAway.pPF == null)
            {
                this.aAway.pPF = this.GetOnPlayer(this.tAway, 2);
            }
            else if (this.aAway.pPF.blnOut)
            {
                this.aAway.pPF = this.GetOnPlayer(this.tAway, 2);
            }
            if (this.aAway.pSF == null)
            {
                this.aAway.pSF = this.GetOnPlayer(this.tAway, 3);
            }
            else if (this.aAway.pSF.blnOut)
            {
                this.aAway.pSF = this.GetOnPlayer(this.tAway, 3);
            }
            if (this.aAway.pSG == null)
            {
                this.aAway.pSG = this.GetOnPlayer(this.tAway, 4);
            }
            else if (this.aAway.pSG.blnOut)
            {
                this.aAway.pSG = this.GetOnPlayer(this.tAway, 4);
            }
            if (this.aAway.pPG == null)
            {
                this.aAway.pPG = this.GetOnPlayer(this.tAway, 5);
            }
            else if (this.aAway.pPG.blnOut)
            {
                this.aAway.pPG = this.GetOnPlayer(this.tAway, 5);
            }
            if ((((this.aAway.pPG != null) && (this.aAway.pSG != null)) && ((this.aAway.pSF != null) && (this.aAway.pPF != null))) && (this.aAway.pC != null))
            {
                this.aAway.intAbility = (((this.aAway.pC.intAbility + this.aAway.pPF.intAbility) + this.aAway.pSF.intAbility) + this.aAway.pSG.intAbility) + this.aAway.pPG.intAbility;
                this.aAway.intAbility /= 5;
                return true;
            }
            this.sbScript.Append("<Script><ScriptID>");
            this.sbScript.Append(this.intScriptID);
            this.sbScript.Append("</ScriptID>");
            this.sbScript.Append("\t\t<QuarterID>");
            this.sbScript.Append(this.intQNumC);
            this.sbScript.Append("</QuarterID>");
            this.sbScript.Append("\t\t<Time></Time>");
            this.sbScript.Append("\t\t<Content>");
            this.sbScript.Append(this.tAway.strClubName);
            this.sbScript.Append("没有队员可以上场，输掉比赛！</Content>");
            this.sbScript.Append("\t\t<Score></Score>");
            this.sbScript.Append("\t</Script>");
            this.intScriptID++;
            if (this.tAway.intScore >= this.tHome.intScore)
            {
                this.tHome.intScore = 2;
                this.tAway.intScore = 0;
            }
            this.timer.SetFinished();
            this.blnEnd = true;
            return false;
        }

        private void StartJumpBall()
        {
            this.sbScript.Append("<Script><ScriptID>");
            this.sbScript.Append(this.intScriptID);
            this.sbScript.Append("</ScriptID>");
            this.sbScript.Append("\t\t<QuarterID>");
            this.sbScript.Append(this.intQNumC);
            this.sbScript.Append("</QuarterID>");
            this.sbScript.Append("\t\t<Time>");
            this.sbScript.Append(this.timer.GetTime());
            this.sbScript.Append("</Time>");
            this.sbScript.Append("\t\t<Content>");
            this.sbScript.Append(this.aHome.pC.strName);
            this.sbScript.Append("与");
            this.sbScript.Append(this.aAway.pC.strName);
            this.sbScript.Append("跳球，比赛开始！</Content>");
            this.sbScript.Append("\t\t<Score>");
            this.sbScript.Append(this.tHome.intScore);
            this.sbScript.Append(":");
            this.sbScript.Append(this.tAway.intScore);
            this.sbScript.Append("</Score>");
            this.sbScript.Append("\t</Script>");
            this.intScriptID++;
            int jumpAbility = this.aHome.GetJumpAbility();
            int num2 = this.aAway.GetJumpAbility();
            if (jumpAbility > this.rnd.Next(0, jumpAbility + num2))
            {
                this.blnTurn = true;
                this.blnTTurn = true;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.tHome.strClubName);
                this.sbScript.Append("获得球权，开始进攻！</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            else
            {
                this.blnTurn = false;
                this.blnTTurn = false;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time></Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.tAway.strClubName);
                this.sbScript.Append("获得球权，开始进攻！</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            this.blnStart = true;
        }

        private void StartRound2()
        {
            if (this.blnTTurn)
            {
                this.blnTurn = false;
                this.blnTTurn = false;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aAway.pC.strName);
                this.sbScript.Append("底线发球，比赛开始。</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            else
            {
                this.blnTurn = true;
                this.blnTTurn = true;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aHome.pC.strName);
                this.sbScript.Append("底线发球，比赛开始。</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
        }

        private void StartRound3()
        {
            if (this.blnTTurn)
            {
                this.blnTurn = true;
                this.blnTTurn = true;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aHome.pC.strName);
                this.sbScript.Append("底线发球，比赛开始。</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
            else
            {
                this.blnTurn = false;
                this.blnTTurn = false;
                this.sbScript.Append("<Script><ScriptID>");
                this.sbScript.Append(this.intScriptID);
                this.sbScript.Append("</ScriptID>");
                this.sbScript.Append("\t\t<QuarterID>");
                this.sbScript.Append(this.intQNumC);
                this.sbScript.Append("</QuarterID>");
                this.sbScript.Append("\t\t<Time>");
                this.sbScript.Append(this.timer.GetTime());
                this.sbScript.Append("</Time>");
                this.sbScript.Append("\t\t<Content>");
                this.sbScript.Append(this.aAway.pC.strName);
                this.sbScript.Append("底线发球，比赛开始。</Content>");
                this.sbScript.Append("\t\t<Score>");
                this.sbScript.Append(this.tHome.intScore);
                this.sbScript.Append(":");
                this.sbScript.Append(this.tAway.intScore);
                this.sbScript.Append("</Score>");
                this.sbScript.Append("\t</Script>");
                this.intScriptID++;
            }
        }
    }
}

