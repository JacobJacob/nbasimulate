namespace Web.SMatchEngine
{
    using System;
    using System.Data;
    using Web.DBData;
    using Web.Helper;

    public class Arrange
    {
        public int intDefAbility;
        public int intDefAdd;
        public int intDefense;
        public int intDefHard;
        public int intDRebAbility;
        public int intOffAbility;
        public int intOffAdd;
        public int intOffense;
        public int intOffHard;
        public int intOffMethod;
        public int intORebAbility;
        public Player pACP;
        public Player pBBP;
        public Player pC;
        public Player pDCP;
        public Player pDRP;
        public Player pF;
        public Player pG;
        public Player pOCP;
        public Player pORP;
        private Random rnd = new Random(DateTime.Now.Millisecond);

        public Arrange(int intArrange3ID, Team team)
        {
            DataRow row = BTPArrange3Manager.GetArrange3RowByArrange3ID(intArrange3ID);
            try
            {
                this.pC = (Player) team.players[row["CID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange3((long) row["CID"], team.intClubID);
                row = BTPArrange3Manager.GetArrange3RowByArrange3ID(intArrange3ID);
                this.pC = (Player) team.players[row["CID"].ToString()];
            }
            try
            {
                this.pF = (Player) team.players[row["FID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange3((long) row["FID"], team.intClubID);
                row = BTPArrange3Manager.GetArrange3RowByArrange3ID(intArrange3ID);
                this.pF = (Player) team.players[row["FID"].ToString()];
            }
            try
            {
                this.pG = (Player) team.players[row["GID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange3((long) row["GID"], team.intClubID);
                row = BTPArrange3Manager.GetArrange3RowByArrange3ID(intArrange3ID);
                this.pG = (Player) team.players[row["GID"].ToString()];
            }
            this.intOffense = (byte) row["Offense"];
            this.intDefense = (byte) row["Defense"];
            this.intOffHard = (byte) row["OffHard"];
            this.intDefHard = (byte) row["DefHard"];
            DataRow dr = BTPArrangeLvlManager.GetArrange3Lvl(team.intUserID);
            this.intOffAdd = this.GetOffAdd(dr);
            this.intDefAdd = this.GetDefAdd(dr);
        }

        private void GetACP(int intPos)
        {
            int num;
            int intAstAbility;
            int num3;
            int num4;
            switch (intPos)
            {
                case 1:
                    intAstAbility = this.pF.intAstAbility;
                    num3 = (this.pG.intAstAbility * 120) / 100;
                    num4 = intAstAbility + num3;
                    if (this.rnd.Next(0, num4) < intAstAbility)
                    {
                        this.pACP = this.pF;
                        return;
                    }
                    this.pACP = this.pG;
                    return;

                case 3:
                    num = this.pC.intAstAbility;
                    num3 = (this.pG.intAstAbility * 120) / 100;
                    num4 = num + num3;
                    if (this.rnd.Next(0, num4) < num)
                    {
                        this.pACP = this.pC;
                        return;
                    }
                    this.pACP = this.pG;
                    return;

                case 5:
                    num = this.pC.intAstAbility;
                    intAstAbility = (this.pF.intAstAbility * 110) / 100;
                    num4 = num + intAstAbility;
                    if (this.rnd.Next(0, num4) < num)
                    {
                        this.pACP = this.pC;
                        return;
                    }
                    this.pACP = this.pF;
                    return;
            }
            this.pACP = this.pG;
        }

        public int GetBlkBallAbility()
        {
            int intOffAbility = 10;
            if (this.intOffAbility > 10)
            {
                intOffAbility = this.intOffAbility;
            }
            int num2 = this.rnd.Next(0, 100);
            if (num2 < 30)
            {
                this.pBBP = this.pC;
                return intOffAbility;
            }
            if ((num2 >= 30) && (num2 < 60))
            {
                this.pBBP = this.pF;
                return intOffAbility;
            }
            this.pBBP = this.pG;
            return intOffAbility;
        }

        private void GetDCP(int intPos, int intOffMethod, int intCAbility, int intFAbility, int intGAbility)
        {
            int[,] numArray = new int[,] { { 170, 110, 120, 110 }, { 150, 80, 110, 130 }, { 110, 170, 120, 110 }, { 80, 150, 110, 130 }, { 110, 190, 150, 150 }, { 140, 110, 130, 100 }, { 120, 100, 110, 130 }, { 120, 100, 110, 130 } };
            switch (intPos)
            {
                case 1:
                    intCAbility += (intCAbility * 15) / 100;
                    break;

                case 3:
                    intFAbility += (intFAbility * 15) / 100;
                    break;

                case 5:
                    intGAbility += (intGAbility * 15) / 100;
                    break;

                default:
                    intGAbility += (intGAbility * 15) / 100;
                    break;
            }
            this.intDefAbility = (((intCAbility + intFAbility) + intGAbility) * numArray[intOffMethod - 1, this.intDefense - 1]) / 100;
            int num = this.rnd.Next(0, this.intDefAbility);
            if (num < intCAbility)
            {
                this.pDCP = this.pC;
            }
            else if ((num >= intCAbility) && (num < (intCAbility + intFAbility)))
            {
                this.pDCP = this.pF;
            }
            else
            {
                this.pDCP = this.pG;
            }
        }

        public int GetDefAbility(Player pOCP, int intOffMethod)
        {
            int num;
            int num2;
            int num3;
            int intPos = pOCP.intPos;
            switch (this.intDefense)
            {
                case 1:
                    num = (this.pC.intDefAbility * 120) / 100;
                    num2 = (this.pF.intDefAbility * 120) / 100;
                    num3 = (this.pG.intDefAbility * 120) / 100;
                    this.GetDCP(intPos, intOffMethod, num, num2, num3);
                    break;

                case 2:
                    num = (this.pC.intDefAbility * 130) / 100;
                    num2 = (this.pF.intDefAbility * 120) / 100;
                    num3 = (this.pG.intDefAbility * 110) / 100;
                    this.GetDCP(intPos, intOffMethod, num, num2, num3);
                    break;

                case 3:
                    num = (this.pC.intDefAbility * 110) / 100;
                    num2 = (this.pF.intDefAbility * 120) / 100;
                    num3 = (this.pG.intDefAbility * 130) / 100;
                    this.GetDCP(intPos, intOffMethod, num, num2, num3);
                    break;

                default:
                    num = (this.pC.intDefAbility * 120) / 100;
                    num2 = (this.pF.intDefAbility * 120) / 100;
                    num3 = (this.pG.intDefAbility * 120) / 100;
                    this.GetDCP(intPos, intOffMethod, num, num2, num3);
                    break;
            }
            this.intDefAbility = ((this.intDefAbility * this.intDefHard) * (100 + this.intDefAdd)) / 0x2710;
            return this.intDefAbility;
        }

        private int GetDefAdd(DataRow dr)
        {
            int intLvl = 0;
            switch (this.intDefense)
            {
                case 1:
                    intLvl = (byte) dr["SDOneInside"];
                    break;

                case 2:
                    intLvl = (byte) dr["SDOneOutside"];
                    break;

                case 3:
                    intLvl = (byte) dr["SDOne"];
                    break;

                case 4:
                    intLvl = (byte) dr["SDArea"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetDefRebAbility()
        {
            int num = (this.pC.intDRebAbility * 12) / 10;
            int intDRebAbility = this.pF.intDRebAbility;
            int num3 = this.pG.intDRebAbility;
            this.intDRebAbility = (num + intDRebAbility) + num3;
            int num4 = this.rnd.Next(0, this.intDRebAbility);
            if (num4 < num)
            {
                this.pDRP = this.pC;
                this.intDRebAbility += (num * 15) / 100;
            }
            else if ((num4 >= num) && (num4 < (num + intDRebAbility)))
            {
                this.pDRP = this.pF;
                this.intDRebAbility += (intDRebAbility * 15) / 100;
            }
            else
            {
                this.pDRP = this.pG;
                this.intDRebAbility += (num3 * 15) / 100;
            }
            this.intDRebAbility = (this.intDRebAbility * this.intDefHard) / 100;
            return this.intDRebAbility;
        }

        private void GetOCP(int intCAbility, int intFAbility, int intGAbility)
        {
            int num = this.rnd.Next(0, this.intOffAbility);
            if (num < intCAbility)
            {
                this.intOffAbility += (intCAbility * 15) / 100;
                this.pOCP = this.pC;
            }
            else if ((num >= intCAbility) && (num < (intCAbility + intFAbility)))
            {
                this.intOffAbility += (intFAbility * 15) / 100;
                this.pOCP = this.pF;
            }
            else
            {
                this.intOffAbility += (intGAbility * 15) / 100;
                this.pOCP = this.pG;
            }
        }

        public int GetOffAbility()
        {
            int num;
            int num2;
            int intOffAbility;
            int num4 = this.rnd.Next(0, 100);
            switch (this.intOffense)
            {
                case 1:
                    if (num4 < 0x20)
                    {
                        this.intOffMethod = 1;
                        break;
                    }
                    if ((num4 < 0x20) || (num4 >= 0x3a))
                    {
                        if ((num4 >= 0x3a) && (num4 < 0x41))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num4 >= 0x41) && (num4 < 0x48))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num4 >= 0x48) && (num4 < 0x4b))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num4 >= 0x4b) && (num4 < 0x54))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num4 >= 0x54) && (num4 < 0x5d))
                        {
                            this.intOffMethod = 7;
                        }
                        else
                        {
                            this.intOffMethod = 8;
                        }
                        break;
                    }
                    this.intOffMethod = 2;
                    break;

                case 2:
                    if (num4 < 10)
                    {
                        this.intOffMethod = 1;
                        break;
                    }
                    if ((num4 < 10) || (num4 >= 0x2a))
                    {
                        if ((num4 >= 0x2a) && (num4 < 0x2f))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num4 >= 0x2f) && (num4 < 0x3e))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num4 >= 0x3e) && (num4 < 0x45))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num4 >= 0x45) && (num4 < 0x4a))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num4 >= 0x4a) && (num4 < 90))
                        {
                            this.intOffMethod = 7;
                        }
                        else
                        {
                            this.intOffMethod = 8;
                        }
                        break;
                    }
                    this.intOffMethod = 2;
                    break;

                case 3:
                    if (num4 < 3)
                    {
                        this.intOffMethod = 1;
                        break;
                    }
                    if ((num4 < 3) || (num4 >= 8))
                    {
                        if ((num4 >= 8) && (num4 < 0x17))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num4 >= 0x17) && (num4 < 0x2e))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num4 >= 0x2e) && (num4 < 70))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num4 >= 70) && (num4 < 80))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num4 >= 80) && (num4 < 90))
                        {
                            this.intOffMethod = 7;
                        }
                        else
                        {
                            this.intOffMethod = 8;
                        }
                        break;
                    }
                    this.intOffMethod = 2;
                    break;

                case 4:
                    if (num4 < 4)
                    {
                        this.intOffMethod = 1;
                        break;
                    }
                    if ((num4 < 4) || (num4 >= 0x17))
                    {
                        if ((num4 >= 0x17) && (num4 < 0x1c))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num4 >= 0x1c) && (num4 < 0x2f))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num4 >= 0x2f) && (num4 < 60))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num4 >= 60) && (num4 < 0x42))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num4 >= 0x42) && (num4 < 0x53))
                        {
                            this.intOffMethod = 7;
                        }
                        else
                        {
                            this.intOffMethod = 8;
                        }
                        break;
                    }
                    this.intOffMethod = 2;
                    break;
            }
            switch (this.intOffMethod)
            {
                case 1:
                    num = (this.pC.intOffAbility * 130) / 100;
                    num2 = (this.pF.intOffAbility * 130) / 100;
                    intOffAbility = this.pG.intOffAbility;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 2:
                    num = ((this.pC.intOffAbility * 120) / 100) + ((this.pG.intOffAbility * 20) / 100);
                    num2 = ((this.pF.intOffAbility * 120) / 100) + ((this.pG.intOffAbility * 10) / 100);
                    intOffAbility = this.pG.intOffAbility;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 3:
                    num = this.pC.intOffAbility;
                    num2 = (this.pF.intOffAbility * 0x7d) / 100;
                    intOffAbility = (this.pG.intOffAbility * 0x87) / 100;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 4:
                    num = this.pC.intOffAbility;
                    num2 = ((this.pF.intOffAbility * 120) / 100) + ((this.pG.intOffAbility * 10) / 100);
                    intOffAbility = (this.pG.intOffAbility * 140) / 100;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 5:
                    num = this.pC.intOffAbility;
                    num2 = (this.pF.intOffAbility * 120) / 100;
                    intOffAbility = (this.pG.intOffAbility * 130) / 100;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 6:
                    num = this.pC.intOffAbility;
                    num2 = (this.pF.intOffAbility * 130) / 100;
                    intOffAbility = (this.pG.intOffAbility * 130) / 100;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                case 7:
                    num = this.pC.intOffAbility;
                    num2 = ((this.pF.intOffAbility * 120) / 100) + ((this.pC.intOffAbility * 120) / 100);
                    intOffAbility = ((this.pG.intOffAbility * 120) / 100) + ((this.pC.intOffAbility * 110) / 100);
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;

                default:
                    num = this.pC.intOffAbility;
                    num2 = ((this.pF.intOffAbility * 120) / 100) + ((this.pG.intOffAbility * 20) / 100);
                    intOffAbility = (this.pG.intOffAbility * 130) / 100;
                    this.intOffAbility = (num + num2) + intOffAbility;
                    this.GetOCP(num, num2, intOffAbility);
                    break;
            }
            this.GetACP(this.pOCP.intPos);
            this.intOffAbility = ((this.intOffAbility * this.intOffHard) * (100 + this.intOffAdd)) / 0x2710;
            return this.intOffAbility;
        }

        private int GetOffAdd(DataRow dr)
        {
            int intLvl = 0;
            switch (this.intOffense)
            {
                case 1:
                    intLvl = (byte) dr["SOInside"];
                    break;

                case 2:
                    intLvl = (byte) dr["SOCHelp"];
                    break;

                case 3:
                    intLvl = (byte) dr["SOOutside"];
                    break;

                case 4:
                    intLvl = (byte) dr["SOAll"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetOffRebAbility()
        {
            int num = (this.pC.intORebAbility * 12) / 10;
            int intORebAbility = this.pF.intORebAbility;
            int num3 = this.pG.intORebAbility;
            this.intORebAbility = (num + intORebAbility) + num3;
            int num4 = this.rnd.Next(0, this.intORebAbility);
            if (num4 < num)
            {
                this.pORP = this.pC;
                this.intORebAbility += (num * 15) / 100;
            }
            else if ((num4 >= num) && (num4 < (num + intORebAbility)))
            {
                this.pORP = this.pF;
                this.intORebAbility += (intORebAbility * 15) / 100;
            }
            else
            {
                this.pORP = this.pG;
                this.intORebAbility += (num3 * 15) / 100;
            }
            this.intORebAbility = (this.intORebAbility * this.intOffHard) / 100;
            return this.intORebAbility;
        }
    }
}

