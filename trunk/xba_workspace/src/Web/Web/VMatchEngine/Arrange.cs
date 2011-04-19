namespace Web.VMatchEngine
{
    using System;
    using System.Data;
    using Web.DBData;
    using Web.Helper;

    public class Arrange
    {
        public int intDefAbility;
        public int intDefAdd;
        public int intDefCenter;
        public int intDefense;
        public int intDefHard;
        public int intDRebAbility;
        public int intOffAbility;
        public int intOffAdd;
        public int intOffCenter;
        public int intOffense;
        public int intOffHard;
        public int intOffMethod;
        public int intORebAbility;
        public Player pACP;
        public Player pBBP;
        public Player pC;
        public Player pDCP;
        public Player pDRP;
        public Player pOCP;
        public Player pORP;
        public Player pPF;
        public Player pPG;
        public Player pSF;
        public Player pSG;
        private Random rnd;

        public Arrange()
        {
            this.intOffAbility = 0;
            this.intDefAbility = 0;
            this.intORebAbility = 0;
            this.intDRebAbility = 0;
            this.rnd = new Random(DateTime.Now.Millisecond);
        }

        public Arrange(int intArrange5ID, Team team, int intType)
        {
            this.intOffAbility = 0;
            this.intDefAbility = 0;
            this.intORebAbility = 0;
            this.intDRebAbility = 0;
            this.rnd = new Random(DateTime.Now.Millisecond);
            DataRow row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
            try
            {
                this.pC = (Player) team.players[row["CID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange5((long) row["CID"], team.intClubID);
                row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
                this.pC = (Player) team.players[row["CID"].ToString()];
            }
            try
            {
                this.pPF = (Player) team.players[row["PFID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange5((long) row["PFID"], team.intClubID);
                row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
                this.pPF = (Player) team.players[row["PFID"].ToString()];
            }
            try
            {
                this.pSF = (Player) team.players[row["SFID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange5((long) row["SFID"], team.intClubID);
                row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
                this.pSF = (Player) team.players[row["SFID"].ToString()];
            }
            try
            {
                this.pSG = (Player) team.players[row["SGID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange5((long) row["SGID"], team.intClubID);
                row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
                this.pSG = (Player) team.players[row["SGID"].ToString()];
            }
            try
            {
                this.pPG = (Player) team.players[row["PGID"].ToString()];
            }
            catch
            {
                PlayerItem.ChangePlayerFromArrange5((long) row["PGID"], team.intClubID);
                row = BTPArrange5Manager.GetArrange5RowByArrange5ID(intArrange5ID);
                this.pPG = (Player) team.players[row["PGID"].ToString()];
            }
            if (((intType == 1) || (intType == 2)) || ((intType == 3) || (intType == 6)))
            {
                this.intOffHard = (byte) row["OffHard"];
                this.intDefHard = (byte) row["DefHard"];
            }
            else
            {
                this.intOffHard = 100;
                this.intDefHard = 100;
            }
            this.intOffense = (byte) row["Offense"];
            this.intDefense = (byte) row["Defense"];
            this.intOffCenter = (byte) row["OffCenter"];
            this.intDefCenter = (byte) row["DefCenter"];
            DataRow dr = BTPArrangeLvlManager.GetArrange5Lvl(team.intUserID);
            this.intOffAdd = this.GetOffAdd(dr);
            this.intDefAdd = this.GetDefAdd(dr);
        }

        private void GetACP(int intPos)
        {
            int intAstAbility;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            switch (intPos)
            {
                case 1:
                    num2 = (this.pPF.intAstAbility * 11) / 10;
                    num3 = (this.pSF.intAstAbility * 12) / 10;
                    num4 = (this.pSG.intAstAbility * 13) / 10;
                    num5 = (this.pPG.intAstAbility * 15) / 10;
                    num6 = ((num2 + num3) + num4) + num5;
                    num7 = this.rnd.Next(0, num6);
                    if (num7 >= num2)
                    {
                        if ((num7 >= num2) && (num7 < (num2 + num3)))
                        {
                            this.pACP = this.pSF;
                            return;
                        }
                        if ((num7 >= (num2 + num3)) && (num7 < ((num2 + num3) + num4)))
                        {
                            this.pACP = this.pSG;
                            return;
                        }
                        this.pACP = this.pPG;
                        return;
                    }
                    this.pACP = this.pPF;
                    return;

                case 2:
                    intAstAbility = this.pC.intAstAbility;
                    num3 = (this.pSF.intAstAbility * 12) / 10;
                    num4 = (this.pSG.intAstAbility * 13) / 10;
                    num5 = (this.pPG.intAstAbility * 15) / 10;
                    num6 = ((intAstAbility + num3) + num4) + num5;
                    num7 = this.rnd.Next(0, num6);
                    if (num7 >= intAstAbility)
                    {
                        if ((num7 >= intAstAbility) && (num7 < (intAstAbility + num3)))
                        {
                            this.pACP = this.pSF;
                            return;
                        }
                        if ((num7 >= (intAstAbility + num3)) && (num7 < ((intAstAbility + num3) + num4)))
                        {
                            this.pACP = this.pSG;
                            return;
                        }
                        this.pACP = this.pPG;
                        return;
                    }
                    this.pACP = this.pC;
                    return;

                case 3:
                    intAstAbility = this.pC.intAstAbility;
                    num2 = (this.pPF.intAstAbility * 11) / 10;
                    num4 = (this.pSG.intAstAbility * 13) / 10;
                    num5 = (this.pPG.intAstAbility * 15) / 10;
                    num6 = ((intAstAbility + num2) + num4) + num5;
                    num7 = this.rnd.Next(0, num6);
                    if (num7 >= intAstAbility)
                    {
                        if ((num7 >= intAstAbility) && (num7 < (intAstAbility + num2)))
                        {
                            this.pACP = this.pPF;
                            return;
                        }
                        if ((num7 >= (intAstAbility + num2)) && (num7 < ((intAstAbility + num2) + num4)))
                        {
                            this.pACP = this.pSG;
                            return;
                        }
                        this.pACP = this.pPG;
                        return;
                    }
                    this.pACP = this.pC;
                    return;

                case 4:
                    intAstAbility = this.pC.intAstAbility;
                    num2 = (this.pPF.intAstAbility * 11) / 10;
                    num3 = (this.pSF.intAstAbility * 12) / 10;
                    num5 = (this.pPG.intAstAbility * 15) / 10;
                    num6 = ((intAstAbility + num2) + num3) + num5;
                    num7 = this.rnd.Next(0, num6);
                    if (num7 >= intAstAbility)
                    {
                        if ((num7 >= intAstAbility) && (num7 < (intAstAbility + num2)))
                        {
                            this.pACP = this.pPF;
                            return;
                        }
                        if ((num7 >= (intAstAbility + num2)) && (num7 < ((intAstAbility + num2) + num3)))
                        {
                            this.pACP = this.pSF;
                            return;
                        }
                        this.pACP = this.pPG;
                        return;
                    }
                    this.pACP = this.pC;
                    return;
            }
            intAstAbility = this.pC.intAstAbility;
            num2 = (this.pPF.intAstAbility * 11) / 10;
            num3 = (this.pSF.intAstAbility * 12) / 10;
            num4 = (this.pSG.intAstAbility * 13) / 10;
            num6 = ((intAstAbility + num2) + num3) + num4;
            num7 = this.rnd.Next(0, num6);
            if (num7 < intAstAbility)
            {
                this.pACP = this.pC;
            }
            else if ((num7 >= intAstAbility) && (num7 < (intAstAbility + num2)))
            {
                this.pACP = this.pPF;
            }
            else if ((num7 >= (intAstAbility + num2)) && (num7 < ((intAstAbility + num2) + num3)))
            {
                this.pACP = this.pSF;
            }
            else
            {
                this.pACP = this.pSG;
            }
        }

        public int GetBlkBallAbility()
        {
            int intOffAbility = 10;
            if (this.intOffAbility > 10)
            {
                intOffAbility = this.intOffAbility;
            }
            int num2 = this.rnd.Next(0, 100);
            if (num2 < 15)
            {
                this.pBBP = this.pC;
                return intOffAbility;
            }
            if ((num2 >= 15) && (num2 < 30))
            {
                this.pBBP = this.pPF;
                return intOffAbility;
            }
            if ((num2 >= 30) && (num2 < 50))
            {
                this.pBBP = this.pSF;
                return intOffAbility;
            }
            if ((num2 >= 50) && (num2 < 0x4b))
            {
                this.pBBP = this.pSG;
                return intOffAbility;
            }
            this.pBBP = this.pPG;
            return intOffAbility;
        }

        private void GetDCP(int intPos, int intOffMethod, int intCAbility, int intPFAbility, int intSFAbility, int intSGAbility, int intPGAbility)
        {
            int[,] numArray = new int[,] { { 170, 60, 100, 160, 220, 100 }, { 220, 100, 160, 90, 170, 60 }, { 60, 170, 90, 160, 100, 220 }, { 100, 220, 160, 90, 60, 170 }, { 100, 230, 170, 160, 100, 230 }, { 100, 80, 100, 180, 180, 100 }, { 170, 120, 160, 90, 110, 80 }, { 180, 120, 160, 100, 100, 90 }, { 90, 90, 80, 160, 140, 140 } };
            switch (intPos)
            {
                case 1:
                    intCAbility += (this.pC.intDefAbility * 15) / 100;
                    break;

                case 2:
                    intPFAbility += (this.pPF.intDefAbility * 15) / 100;
                    break;

                case 3:
                    intSFAbility += (this.pSF.intDefAbility * 15) / 100;
                    break;

                case 4:
                    intSGAbility += (this.pSG.intDefAbility * 15) / 100;
                    break;

                default:
                    intPGAbility += (this.pPG.intDefAbility * 15) / 100;
                    break;
            }
            this.intDefAbility = (((((intCAbility + intPFAbility) + intSFAbility) + intSGAbility) + intPGAbility) * numArray[intOffMethod - 1, this.intDefense - 1]) / 100;
            this.intDefAbility = ((((this.intDefAbility / 10) * this.intDefHard) / 100) * (100 + this.intDefAdd)) / 10;
            if (this.intOffCenter == this.intDefCenter)
            {
                this.intDefAbility = (this.intDefAbility * this.rnd.Next(0x5f, 120)) / 100;
            }
            int num = this.rnd.Next(0, this.intDefAbility);
            if (num < intCAbility)
            {
                this.pDCP = this.pC;
            }
            else if ((num >= intCAbility) && (num < (intCAbility + intPFAbility)))
            {
                this.pDCP = this.pPF;
            }
            else if ((num >= (intCAbility + intPFAbility)) && (num < ((intCAbility + intPFAbility) + intSFAbility)))
            {
                this.pDCP = this.pSF;
            }
            else if ((num >= ((intCAbility + intPFAbility) + intSFAbility)) && (num < (((intCAbility + intPFAbility) + intSFAbility) + intSGAbility)))
            {
                this.pDCP = this.pSG;
            }
            else
            {
                this.pDCP = this.pPG;
            }
        }

        public int GetDefAbility(Player pOCP, int intOffMethod)
        {
            int num2;
            int num3;
            int num4;
            int intDefAbility;
            int num6;
            int intPos = pOCP.intPos;
            switch (this.intDefense)
            {
                case 1:
                    num2 = ((this.pC.intDefAbility * 140) / 100) + ((this.pPG.intDefAbility * 20) / 100);
                    num3 = ((this.pPF.intDefAbility * 130) / 100) + ((this.pPG.intDefAbility * 10) / 100);
                    num4 = (this.pSF.intDefAbility * 120) / 100;
                    intDefAbility = this.pSG.intDefAbility;
                    num6 = this.pPG.intDefAbility;
                    break;

                case 2:
                    num2 = this.pC.intDefAbility;
                    num3 = this.pPF.intDefAbility;
                    num4 = (this.pSF.intDefAbility * 120) / 100;
                    intDefAbility = ((this.pSG.intDefAbility * 130) / 100) + ((this.pPG.intDefAbility * 10) / 100);
                    num6 = (this.pPG.intDefAbility * 130) / 100;
                    break;

                case 3:
                    num2 = (this.pC.intDefAbility * 130) / 100;
                    num3 = (this.pPF.intDefAbility * 120) / 100;
                    num4 = (this.pSF.intDefAbility * 120) / 100;
                    intDefAbility = (this.pSG.intDefAbility * 0x7d) / 100;
                    num6 = (this.pPG.intDefAbility * 110) / 100;
                    break;

                case 4:
                    num2 = (this.pC.intDefAbility * 120) / 100;
                    num3 = (this.pPF.intDefAbility * 130) / 100;
                    num4 = (this.pSF.intDefAbility * 120) / 100;
                    intDefAbility = (this.pSG.intDefAbility * 0x7d) / 100;
                    num6 = (this.pPG.intDefAbility * 110) / 100;
                    break;

                case 5:
                    num2 = (this.pC.intDefAbility * 140) / 100;
                    num3 = (this.pPF.intDefAbility * 130) / 100;
                    num4 = (this.pSF.intDefAbility * 130) / 100;
                    intDefAbility = this.pSG.intDefAbility;
                    num6 = this.pPG.intDefAbility;
                    break;

                default:
                    num2 = this.pC.intDefAbility;
                    num3 = (this.pPF.intDefAbility * 120) / 100;
                    num4 = (this.pSF.intDefAbility * 130) / 100;
                    intDefAbility = (this.pSG.intDefAbility * 0x7d) / 100;
                    num6 = (this.pPG.intDefAbility * 0x73) / 100;
                    break;
            }
            if (((intPos == 1) || (intPos == 2)) && ((intOffMethod == 1) || (intOffMethod == 2)))
            {
                switch (this.intDefense)
                {
                    case 1:
                        if ((this.pC.intHeight - pOCP.intHeight) >= 5)
                        {
                            if ((this.pC.intHeight - pOCP.intHeight) > 5)
                            {
                                int num7 = this.pC.intHeight - pOCP.intHeight;
                                if (num7 > 15)
                                {
                                    num7 = 15;
                                }
                                num2 = (num2 * (100 + (0x19 * num7))) / 100;
                            }
                            break;
                        }
                        num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        break;

                    case 2:
                        if ((this.pC.intHeight - pOCP.intHeight) >= 5)
                        {
                            if ((this.pC.intHeight - pOCP.intHeight) > 5)
                            {
                                int num9 = this.pC.intHeight - pOCP.intHeight;
                                if (num9 > 15)
                                {
                                    num9 = 15;
                                }
                                num2 = (num2 * (100 + (0x19 * num9))) / 100;
                            }
                        }
                        else
                        {
                            num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        }
                        goto Label_0647;

                    case 3:
                        if ((this.pC.intHeight - pOCP.intHeight) >= 5)
                        {
                            if ((this.pC.intHeight - pOCP.intHeight) > 5)
                            {
                                int num10 = this.pC.intHeight - pOCP.intHeight;
                                if (num10 > 15)
                                {
                                    num10 = 15;
                                }
                                num2 = (num2 * (100 + (0x19 * num10))) / 100;
                            }
                        }
                        else
                        {
                            num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        }
                        goto Label_0647;

                    case 4:
                        if ((this.pC.intHeight - pOCP.intHeight) >= 5)
                        {
                            if ((this.pC.intHeight - pOCP.intHeight) > 5)
                            {
                                int num11 = this.pC.intHeight - pOCP.intHeight;
                                if (num11 > 15)
                                {
                                    num11 = 15;
                                }
                                num2 = (num2 * (100 + (0x19 * num11))) / 100;
                            }
                        }
                        else
                        {
                            num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        }
                        goto Label_0647;

                    case 5:
                        if ((this.pC.intHeight - pOCP.intHeight) >= 5)
                        {
                            if ((this.pC.intHeight - pOCP.intHeight) > 5)
                            {
                                int num12 = this.pC.intHeight - pOCP.intHeight;
                                if (num12 > 15)
                                {
                                    num12 = 15;
                                }
                                num2 = (num2 * (100 + (0x19 * num12))) / 100;
                            }
                        }
                        else
                        {
                            num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        }
                        if ((this.pPF.intHeight - pOCP.intHeight) > 5)
                        {
                            int num13 = this.pPF.intHeight - pOCP.intHeight;
                            if (num13 > 15)
                            {
                                num13 = 15;
                            }
                            num3 = (num3 * (100 + (0x19 * num13))) / 100;
                        }
                        goto Label_0647;

                    default:
                        if ((this.pC.intHeight - pOCP.intHeight) < 5)
                        {
                            num2 = (num2 * (100 + (2 * (this.pC.intHeight - pOCP.intHeight)))) / 100;
                        }
                        else if ((this.pC.intHeight - pOCP.intHeight) > 5)
                        {
                            int num14 = this.pC.intHeight - pOCP.intHeight;
                            if (num14 > 15)
                            {
                                num14 = 15;
                            }
                            num2 = (num2 * (100 + (0x19 * num14))) / 100;
                        }
                        goto Label_0647;
                }
                if ((this.pPF.intHeight - pOCP.intHeight) > 5)
                {
                    int num8 = this.pPF.intHeight - pOCP.intHeight;
                    if (num8 > 15)
                    {
                        num8 = 15;
                    }
                    num3 = (num3 * (100 + (0x19 * num8))) / 100;
                }
            }
        Label_0647:
            if (this.intDefCenter == 1)
            {
                num2 += this.pC.intHardness * 5;
            }
            else if (this.intDefCenter == 2)
            {
                num3 += this.pPF.intHardness * 5;
            }
            else if (this.intDefCenter == 3)
            {
                num4 += this.pSF.intHardness * 5;
            }
            else if (this.intDefCenter == 4)
            {
                intDefAbility += this.pSG.intHardness * 5;
            }
            else if (this.intDefCenter == 5)
            {
                num6 += this.pPG.intHardness * 5;
            }
            else
            {
                num2 += this.pC.intHardness;
                num3 += this.pPF.intHardness;
                num4 += this.pSF.intHardness;
                intDefAbility += this.pSG.intHardness;
                num6 += this.pPG.intHardness;
            }
            this.intDefAbility = ((this.intDefAbility * this.intDefHard) * (100 + this.intDefAdd)) / 0x2710;
            this.GetDCP(intPos, intOffMethod, num2, num3, num4, intDefAbility, num6);
            return this.intDefAbility;
        }

        private int GetDefAdd(DataRow dr)
        {
            int intLvl = 0;
            switch (this.intDefense)
            {
                case 1:
                    intLvl = (byte) dr["VDArea23"];
                    break;

                case 2:
                    intLvl = (byte) dr["VDArea32"];
                    break;

                case 3:
                    intLvl = (byte) dr["VDArea212"];
                    break;

                case 4:
                    intLvl = (byte) dr["VDOne"];
                    break;

                case 5:
                    intLvl = (byte) dr["VDOneInside"];
                    break;

                case 6:
                    intLvl = (byte) dr["VDOneOutside"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetDefAddByDefense(DataRow dr, int intDefense)
        {
            int intLvl = 0;
            switch (intDefense)
            {
                case 1:
                    intLvl = (byte) dr["VDArea23"];
                    break;

                case 2:
                    intLvl = (byte) dr["VDArea32"];
                    break;

                case 3:
                    intLvl = (byte) dr["VDArea212"];
                    break;

                case 4:
                    intLvl = (byte) dr["VDOne"];
                    break;

                case 5:
                    intLvl = (byte) dr["VDOneInside"];
                    break;

                case 6:
                    intLvl = (byte) dr["VDOneOutside"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetDefRebAbility()
        {
            int num = (((this.pC.intDRebAbility * 12) / 10) * this.pC.intHeight) / 210;
            int num2 = (((this.pPF.intDRebAbility * 11) / 10) * this.pPF.intHeight) / 0xcd;
            int num3 = (this.pSF.intDRebAbility * this.pSF.intHeight) / 200;
            int intDRebAbility = this.pSG.intDRebAbility;
            int num5 = this.pPG.intDRebAbility;
            this.intDRebAbility = (((num + num2) + num3) + intDRebAbility) + num5;
            int num6 = this.rnd.Next(0, this.intDRebAbility);
            if (num6 < num)
            {
                this.pDRP = this.pC;
                this.intDRebAbility += (num * 15) / 100;
            }
            else if ((num6 >= num) && (num6 < (num + num2)))
            {
                this.pDRP = this.pPF;
                this.intDRebAbility += (num2 * 15) / 100;
            }
            else if ((num6 >= (num + num2)) && (num6 < ((num + num2) + num3)))
            {
                this.pDRP = this.pSF;
                this.intDRebAbility += (num3 * 15) / 100;
            }
            else if ((num6 >= ((num + num2) + num3)) && (num6 < (((num + num2) + num3) + intDRebAbility)))
            {
                this.pDRP = this.pSG;
                this.intDRebAbility += (intDRebAbility * 15) / 100;
            }
            else
            {
                this.pDRP = this.pPG;
                this.intDRebAbility += (num5 * 15) / 100;
            }
            this.intDRebAbility = (this.intDRebAbility * this.intOffHard) / 100;
            return this.intDRebAbility;
        }

        public int GetHardnessTotal()
        {
            return ((((this.pC.intHardness + this.pPF.intHardness) + this.pSF.intHardness) + this.pSG.intHardness) + this.pPG.intHardness);
        }

        public int GetJumpAbility()
        {
            return ((this.pC.intHeight + this.pC.intJump) + this.pC.intTeam);
        }

        public int GetLeadshipTotal()
        {
            if (this.intOffCenter == 1)
            {
                return (this.pC.intLeadship * 5);
            }
            if (this.intOffCenter == 2)
            {
                return (this.pPG.intLeadship * 5);
            }
            if (this.intOffCenter == 3)
            {
                return (this.pSF.intLeadship * 5);
            }
            if (this.intOffCenter == 4)
            {
                return (this.pSG.intLeadship * 5);
            }
            if (this.intOffCenter == 5)
            {
                return (this.pPG.intLeadship * 5);
            }
            return ((((this.pC.intLeadship + this.pPF.intLeadship) + this.pSF.intLeadship) + this.pSG.intLeadship) + this.pPG.intLeadship);
        }

        private void GetOCP(int intCAbility, int intPFAbility, int intSFAbility, int intSGAbility, int intPGAbility)
        {
            int num = this.rnd.Next(0, this.intOffAbility);
            if (num < intCAbility)
            {
                this.intOffAbility += (this.pC.intOffAbility * 15) / 100;
                this.pOCP = this.pC;
            }
            else if ((num >= intCAbility) && (num < (intCAbility + intPFAbility)))
            {
                this.intOffAbility += (this.pPF.intOffAbility * 15) / 100;
                this.pOCP = this.pPF;
            }
            else if ((num >= (intCAbility + intPFAbility)) && (num < ((intCAbility + intPFAbility) + intSFAbility)))
            {
                this.intOffAbility += (this.pSF.intOffAbility * 15) / 100;
                this.pOCP = this.pSF;
            }
            else if ((num >= ((intCAbility + intPFAbility) + intSFAbility)) && (num < (((intCAbility + intPFAbility) + intSFAbility) + intSGAbility)))
            {
                this.intOffAbility += (this.pSG.intOffAbility * 15) / 100;
                this.pOCP = this.pSG;
            }
            else
            {
                this.intOffAbility += (this.pPG.intOffAbility * 15) / 100;
                this.pOCP = this.pPG;
            }
        }

        public int GetOffAbility()
        {
            int num2;
            int num3;
            int intOffAbility;
            int num5;
            int num6;
            int num = this.rnd.Next(0, 100);
            switch (this.intOffense)
            {
                case 1:
                    if (num >= 50)
                    {
                        if ((num >= 50) && (num < 0x3a))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 0x3a) && (num < 0x3f))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 0x3f) && (num < 0x44))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x44) && (num < 70))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 80) && (num < 0x54))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x54) && (num < 0x5b))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x5b) && (num < 0x62))
                        {
                            this.intOffMethod = 8;
                        }
                        else
                        {
                            this.intOffMethod = 9;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;

                case 2:
                    if (num >= 8)
                    {
                        if ((num >= 8) && (num < 0x3a))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 0x3a) && (num < 0x3f))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 0x3f) && (num < 0x42))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x42) && (num < 0x44))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 0x44) && (num < 0x49))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x49) && (num < 0x58))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x58) && (num < 0x62))
                        {
                            this.intOffMethod = 8;
                        }
                        else
                        {
                            this.intOffMethod = 9;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;

                case 3:
                    if (num >= 3)
                    {
                        if ((num >= 3) && (num < 6))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 6) && (num < 0x33))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 0x33) && (num < 0x38))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x38) && (num < 0x4f))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 0x4f) && (num < 0x55))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x55) && (num < 0x5b))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x5b) && (num < 0x61))
                        {
                            this.intOffMethod = 8;
                        }
                        else
                        {
                            this.intOffMethod = 9;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;

                case 4:
                    if (num >= 8)
                    {
                        if ((num >= 8) && (num < 13))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 13) && (num < 0x15))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 0x15) && (num < 0x1a))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x1a) && (num < 0x22))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 0x22) && (num < 0x40))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x40) && (num < 0x48))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x48) && (num < 80))
                        {
                            this.intOffMethod = 8;
                        }
                        else
                        {
                            this.intOffMethod = 9;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;

                case 5:
                    if (num >= 2)
                    {
                        if ((num >= 2) && (num < 0x16))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 0x16) && (num < 0x18))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 0x18) && (num < 0x2c))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x2c) && (num < 0x36))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 0x36) && (num < 0x39))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x39) && (num < 0x4d))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x4d) && (num < 0x61))
                        {
                            this.intOffMethod = 7;
                        }
                        else
                        {
                            this.intOffMethod = 8;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;

                case 6:
                    if (num >= 2)
                    {
                        if ((num >= 2) && (num < 4))
                        {
                            this.intOffMethod = 2;
                        }
                        else if ((num >= 4) && (num < 6))
                        {
                            this.intOffMethod = 3;
                        }
                        else if ((num >= 6) && (num < 0x38))
                        {
                            this.intOffMethod = 4;
                        }
                        else if ((num >= 0x38) && (num < 0x42))
                        {
                            this.intOffMethod = 5;
                        }
                        else if ((num >= 0x42) && (num < 0x44))
                        {
                            this.intOffMethod = 6;
                        }
                        else if ((num >= 0x44) && (num < 0x58))
                        {
                            this.intOffMethod = 7;
                        }
                        else if ((num >= 0x58) && (num < 0x62))
                        {
                            this.intOffMethod = 8;
                        }
                        else
                        {
                            this.intOffMethod = 9;
                        }
                        break;
                    }
                    this.intOffMethod = 1;
                    break;
            }
            switch (this.intOffMethod)
            {
                case 1:
                    num2 = (this.pC.intOffAbility * 150) / 100;
                    num3 = (this.pPF.intOffAbility * 140) / 100;
                    intOffAbility = this.pSF.intOffAbility;
                    num5 = this.pSG.intOffAbility;
                    num6 = this.pPG.intOffAbility;
                    break;

                case 2:
                    num2 = ((this.pC.intOffAbility * 140) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    num3 = ((this.pPF.intOffAbility * 120) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    intOffAbility = this.pSF.intOffAbility;
                    num5 = this.pSG.intOffAbility;
                    num6 = this.pPG.intOffAbility;
                    break;

                case 3:
                    num2 = this.pC.intOffAbility;
                    num3 = (this.pPF.intOffAbility * 110) / 100;
                    intOffAbility = (this.pSF.intOffAbility * 140) / 100;
                    num5 = (this.pSG.intOffAbility * 140) / 100;
                    num6 = (this.pPG.intOffAbility * 110) / 100;
                    break;

                case 4:
                    num2 = this.pC.intOffAbility;
                    num3 = (this.pPF.intOffAbility * 120) / 100;
                    intOffAbility = ((this.pSF.intOffAbility * 130) / 100) + ((this.pPG.intOffAbility * 10) / 100);
                    num5 = ((this.pSG.intOffAbility * 140) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    num6 = this.pPG.intOffAbility;
                    break;

                case 5:
                    num2 = (this.pC.intOffAbility * 70) / 100;
                    num3 = (this.pPF.intOffAbility * 90) / 100;
                    intOffAbility = (this.pSF.intOffAbility * 120) / 100;
                    num5 = (this.pSG.intOffAbility * 130) / 100;
                    num6 = (this.pPG.intOffAbility * 120) / 100;
                    break;

                case 6:
                    num2 = this.pC.intOffAbility;
                    num3 = this.pPF.intOffAbility;
                    intOffAbility = (this.pSF.intOffAbility * 120) / 100;
                    num5 = (this.pSG.intOffAbility * 140) / 100;
                    num6 = (this.pPG.intOffAbility * 130) / 100;
                    break;

                case 7:
                    num2 = this.pC.intOffAbility;
                    num3 = ((this.pPF.intOffAbility * 120) / 100) + ((this.pC.intOffAbility * 30) / 100);
                    intOffAbility = ((this.pSF.intOffAbility * 120) / 100) + ((this.pC.intOffAbility * 20) / 100);
                    num5 = ((this.pSG.intOffAbility * 120) / 100) + ((this.pC.intOffAbility * 20) / 100);
                    num6 = this.pPG.intOffAbility;
                    break;

                case 8:
                    num2 = this.pC.intOffAbility;
                    num3 = ((this.pPF.intOffAbility * 120) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    intOffAbility = ((this.pSF.intOffAbility * 120) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    num5 = ((this.pSG.intOffAbility * 110) / 100) + ((this.pPG.intOffAbility * 20) / 100);
                    num6 = this.pPG.intOffAbility;
                    break;

                default:
                    num2 = this.pC.intOffAbility;
                    num3 = this.pPF.intOffAbility;
                    intOffAbility = (this.pSF.intOffAbility * 130) / 100;
                    num5 = ((this.pSG.intOffAbility * 130) / 100) + ((this.pPG.intOffAbility * 10) / 100);
                    num6 = (this.pPG.intOffAbility * 130) / 100;
                    break;
            }
            if (this.intOffCenter == 1)
            {
                num2 += this.pC.intLeadship * 5;
            }
            else if (this.intOffCenter == 2)
            {
                num3 += this.pPF.intLeadship * 5;
            }
            else if (this.intOffCenter == 3)
            {
                intOffAbility += this.pSF.intLeadship * 5;
            }
            else if (this.intOffCenter == 4)
            {
                num5 += this.pSG.intLeadship * 5;
            }
            else if (this.intOffCenter == 5)
            {
                num6 += this.pPG.intLeadship * 5;
            }
            else
            {
                num2 += this.pC.intLeadship;
                num3 += this.pPF.intLeadship;
                intOffAbility += this.pSF.intLeadship;
                num5 += this.pSG.intLeadship;
                num6 += this.pPG.intLeadship;
            }
            this.intOffAbility = (((num2 + num3) + intOffAbility) + num5) + num6;
            this.GetOCP(num2, num3, intOffAbility, num5, num6);
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
                    intLvl = (byte) dr["VOInside"];
                    break;

                case 2:
                    intLvl = (byte) dr["VOCHelp"];
                    break;

                case 3:
                    intLvl = (byte) dr["VOOutside"];
                    break;

                case 4:
                    intLvl = (byte) dr["VOSpeed"];
                    break;

                case 5:
                    intLvl = (byte) dr["VOAll"];
                    break;

                case 6:
                    intLvl = (byte) dr["VOBlock"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetOffAddByOffense(DataRow dr, int intOffense)
        {
            int intLvl = 0;
            switch (intOffense)
            {
                case 1:
                    intLvl = (byte) dr["VOInside"];
                    break;

                case 2:
                    intLvl = (byte) dr["VOCHelp"];
                    break;

                case 3:
                    intLvl = (byte) dr["VOOutside"];
                    break;

                case 4:
                    intLvl = (byte) dr["VOSpeed"];
                    break;

                case 5:
                    intLvl = (byte) dr["VOAll"];
                    break;

                case 6:
                    intLvl = (byte) dr["VOBlock"];
                    break;

                default:
                    intLvl = 0;
                    break;
            }
            return MatchItem.GetArrangeAdd(intLvl);
        }

        public int GetOffRebAbility()
        {
            int num = (((this.pC.intORebAbility * 12) / 10) * this.pC.intHeight) / 210;
            int num2 = (((this.pPF.intORebAbility * 11) / 10) * this.pPF.intHeight) / 0xcd;
            int num3 = (this.pSF.intORebAbility * this.pSF.intHeight) / 200;
            int intORebAbility = this.pSG.intORebAbility;
            int num5 = this.pPG.intORebAbility;
            this.intORebAbility = (((num + num2) + num3) + intORebAbility) + num5;
            int num6 = this.rnd.Next(0, this.intORebAbility);
            if (num6 < num)
            {
                this.pORP = this.pC;
                this.intORebAbility += (num * 15) / 100;
            }
            else if ((num6 >= num) && (num6 < (num + num2)))
            {
                this.pORP = this.pPF;
                this.intORebAbility += (num2 * 15) / 100;
            }
            else if ((num6 >= (num + num2)) && (num6 < ((num + num2) + num3)))
            {
                this.pORP = this.pSF;
                this.intORebAbility += (num3 * 15) / 100;
            }
            else if ((num6 >= ((num + num2) + num3)) && (num6 < (((num + num2) + num3) + intORebAbility)))
            {
                this.pORP = this.pSG;
                this.intORebAbility += (intORebAbility * 15) / 100;
            }
            else
            {
                this.pORP = this.pPG;
                this.intORebAbility += (num5 * 15) / 100;
            }
            this.intORebAbility = (this.intORebAbility * this.intOffHard) / 100;
            return this.intORebAbility;
        }
    }
}

