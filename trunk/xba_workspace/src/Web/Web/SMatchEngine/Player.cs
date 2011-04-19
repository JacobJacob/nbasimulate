namespace Web.SMatchEngine
{
    using System;
    using System.Data;
    using Web.Helper;

    public class Player
    {
        public bool blnInjured;
        public bool blnOnArrange;
        public bool blnOut;
        public bool blnPlayed;
        public int int3P;
        public int int3Ps;
        public int intAbility;
        public int intAge;
        public int intAst;
        public int intAstAbility;
        public int intAttack;
        public int intBlk;
        public int intBlock;
        public int intBlockAbility;
        public int intDefAbility;
        public int intDefense;
        public int intDReb;
        public int intDRebAbility;
        public int intDribble;
        public int intDunk;
        public int intFG;
        public int intFGs;
        public int intFoul;
        public int intFT;
        public int intFTAbility;
        public int intFTs;
        public int intHappy;
        public int intHeight;
        public int intJump;
        public int intNumber;
        public int intOffAbility;
        public int intOReb;
        public int intORebAbility;
        public int intPass;
        public int intPoint3;
        public int intPos;
        public int intPower;
        public int intRebound;
        public int intScore;
        public int intShot;
        public int intSpeed;
        public int intStamina;
        public int intStatus;
        public int intSteal;
        public int intStl;
        public int intStrength;
        public int intTeam;
        public int intTo;
        public int intWeight;
        public long longPlayerID;
        public string strName;

        public Player()
        {
        }

        public Player(DataRow drPlayer, bool blnIsHome)
        {
            this.longPlayerID = (long) drPlayer["PlayerID"];
            this.blnPlayed = false;
            this.blnOut = false;
            this.blnOnArrange = false;
            this.blnInjured = false;
            this.strName = StringItem.GetXMLTrueBody(drPlayer["Name"].ToString().Trim());
            if (blnIsHome)
            {
                this.strName = "&lt;u&gt;" + this.strName + "&lt;/u&gt;";
            }
            this.intHeight = (byte) drPlayer["Height"];
            this.intWeight = (byte) drPlayer["Weight"];
            this.intPos = (byte) drPlayer["Pos"];
            this.intStatus = (byte) drPlayer["Status"];
            this.intPower = (byte) drPlayer["Power"];
            this.intFGs = 0;
            this.intFG = 0;
            this.int3Ps = 0;
            this.int3P = 0;
            this.intFTs = 0;
            this.intFT = 0;
            this.intTo = 0;
            this.intScore = 0;
            this.intOReb = 0;
            this.intDReb = 0;
            this.intAst = 0;
            this.intStl = 0;
            this.intBlk = 0;
            this.intFoul = 0;
            this.intSpeed = (int) drPlayer["Speed"];
            this.intJump = (int) drPlayer["Jump"];
            this.intStrength = (int) drPlayer["Strength"];
            this.intStamina = (int) drPlayer["Stamina"];
            this.intShot = (int) drPlayer["Shot"];
            this.intPoint3 = (int) drPlayer["Point3"];
            this.intDribble = (int) drPlayer["Dribble"];
            this.intPass = (int) drPlayer["Pass"];
            this.intRebound = (int) drPlayer["Rebound"];
            this.intSteal = (int) drPlayer["Steal"];
            this.intBlock = (int) drPlayer["Block"];
            this.intAttack = (int) drPlayer["Attack"];
            this.intDefense = (int) drPlayer["Defense"];
            this.intTeam = (int) drPlayer["Team"];
            this.intAbility = (int) drPlayer["Ability"];
            this.intDunk = (this.intHeight * 10) + this.intJump;
            this.intNumber = (byte) drPlayer["Number"];
            this.intAge = (byte) drPlayer["Age"];
            this.intHappy = (byte) drPlayer["Happy"];
            this.intOffAbility = 0;
            this.intDefAbility = 0;
            this.intORebAbility = 0;
            this.intDRebAbility = 0;
            this.intAstAbility = 0;
        }

        public void SetAbility(int intPos)
        {
            this.blnOnArrange = true;
            this.blnPlayed = true;
            switch (intPos)
            {
                case 1:
                    this.intOffAbility = (((((((((this.intSpeed + ((this.intJump * 120) / 100)) + ((this.intStrength * 170) / 100)) + this.intShot) + this.intPoint3) + this.intDribble) + this.intPass) + ((this.intAttack * 140) / 100)) + this.intTeam) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    this.intDefAbility = (((((((((this.intSpeed + ((this.intJump * 120) / 100)) + ((this.intStrength * 160) / 100)) + this.intStamina) + ((this.intRebound * 190) / 100)) + this.intSteal) + ((this.intBlock * 190) / 100)) + ((this.intDefense * 150) / 100)) + this.intTeam) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    break;

                case 3:
                    this.intOffAbility = (((((((((((this.intSpeed * 140) / 100) + ((this.intJump * 110) / 100)) + ((this.intStrength * 130) / 100)) + ((this.intShot * 160) / 100)) + ((this.intPoint3 * 110) / 100)) + ((this.intDribble * 140) / 100)) + ((this.intPass * 130) / 100)) + ((this.intAttack * 150) / 100)) + ((this.intTeam * 110) / 100)) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    this.intDefAbility = (((((((((((this.intSpeed * 130) / 100) + this.intJump) + ((this.intStrength * 120) / 100)) + this.intStamina) + ((this.intRebound * 140) / 100)) + ((this.intSteal * 120) / 100)) + ((this.intBlock * 130) / 100)) + ((this.intDefense * 150) / 100)) + this.intTeam) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    break;

                case 5:
                    this.intOffAbility = (((((((((((this.intSpeed * 110) / 100) + this.intJump) + this.intStrength) + ((this.intShot * 150) / 100)) + ((this.intPoint3 * 130) / 100)) + ((this.intDribble * 150) / 100)) + ((this.intPass * 140) / 100)) + ((this.intAttack * 150) / 100)) + ((this.intTeam * 150) / 100)) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    this.intDefAbility = (((((((((((this.intSpeed * 120) / 100) + this.intJump) + this.intStrength) + this.intStamina) + this.intRebound) + ((this.intSteal * 170) / 100)) + this.intBlock) + ((this.intDefense * 150) / 100)) + ((this.intTeam * 0xa5) / 100)) * MatchItem.GetPowerAffect(this.intPower)) / 100;
                    break;
            }
            if (this.intStatus == 2)
            {
                this.intOffAbility /= 2;
                this.intDefAbility /= 2;
            }
            this.intAstAbility = ((((this.intSpeed + (this.intPass * 3)) + this.intTeam) + this.intAttack) + (this.intDribble * 2)) + ((100 * this.intPos) * this.intPos);
            this.intORebAbility = ((((((this.intJump / 2) + (this.intStrength / 2)) + (this.intRebound * 3)) + (this.intAttack / 2)) + ((this.intHeight * (6 - this.intPos)) / 3)) + (this.intWeight / 3)) / 7;
            this.intDRebAbility = (((((this.intJump / 2) + (this.intStrength / 2)) + (this.intRebound * 3)) + (this.intDefense / 2)) + ((this.intHeight * (6 - this.intPos)) / 3)) + (this.intWeight / 3);
            int num = this.intHeight + (this.intWeight / 2);
            int num2 = 100;
            if (intPos == 1)
            {
                if (this.intHeight < 0xcd)
                {
                    num2 = 100 - (0xcd - this.intHeight);
                }
                if (num2 < 0)
                {
                    num2 = 10;
                }
                this.intDefAbility = (this.intDefAbility * num2) / 100;
            }
            else if (intPos == 5)
            {
                if (this.intHeight > 0xcb)
                {
                    num2 = 100 - (this.intHeight - 0xcb);
                }
                if (num2 < 0)
                {
                    num2 = 10;
                }
                this.intOffAbility = (this.intOffAbility * num2) / 100;
            }
            this.intOffAbility += num;
            this.intDefAbility += num;
            this.intFTAbility = this.intShot + (this.intPoint3 / 3);
            if (this.intFTAbility > 700)
            {
                this.intFTAbility = 700;
            }
            if (this.intFTAbility < 300)
            {
                this.intFTAbility = 300;
            }
            this.intBlockAbility = ((this.intBlock + this.intJump) + this.intDefense) + (this.intHeight * (5 - intPos));
            if (intPos == 1)
            {
                this.intBlockAbility = (this.intBlockAbility * num2) / 100;
            }
            this.intPos = intPos;
        }
    }
}

