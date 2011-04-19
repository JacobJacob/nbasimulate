namespace Web.VMatchEngine
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
        private Arrange[] aAways = new Arrange[7];
        private Arrange[] aHomes = new Arrange[7];
        private bool blnCanAddA = false;
        private bool blnCanAddH = false;
        private bool blnCanPlay;
        private bool blnPower;
        private bool blnTTurn = true;
        private bool blUseStaffA;
        private bool blUseStaffH;
        private byte byAllAddA = 0;
        private byte byAllAddH = 0;
        private int intAbilitySumA = 0;
        private int intAbilitySumH = 0;
        private int[] intADefs = new int[6];
        private int[] intAOffs = new int[6];
        private int[] intHDefs = new int[6];
        private int[] intHOffs = new int[6];
        private int intLoseAbility;
        private int intMaxAbility = 0;
        private int intMVPValue = 0;
        private int intPlayedCountA = 0;
        private int intPlayedCountH = 0;
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

        public Match(int intClubIDH, int intClubIDA, bool blnPower, int intType, int intTag, bool blUseStaffH, bool blUseStaffA, byte byAllAddH, byte byAllAddA)
        {
            this.blnPower = blnPower;
            this.intType = intType;
            this.intTag = intTag;
            this.blUseStaffH = blUseStaffH;
            this.blUseStaffA = blUseStaffA;
            this.tHome = new Team(intClubIDH, true);
            this.tAway = new Team(intClubIDA, false);
            this.byAllAddH = byAllAddH;
            this.byAllAddA = byAllAddA;
            if (this.tHome.players.Count < 6)
            {
                this.tHome.intScore = 0;
                this.tAway.intScore = 20;
                this.blnCanPlay = false;
                if (this.intType == 7)
                {
                    BTPFriMatchManager.AddMoneyForMatch(this.tHome.intUserID, this.tAway.intUserID, this.tHome.intScore, this.tAway.intScore, false, false);
                }
            }
            else if (this.tAway.players.Count < 6)
            {
                this.tHome.intScore = 20;
                this.tAway.intScore = 0;
                this.blnCanPlay = false;
                if (this.intType == 7)
                {
                    BTPFriMatchManager.AddMoneyForMatch(this.tHome.intUserID, this.tAway.intUserID, this.tHome.intScore, this.tAway.intScore, false, false);
                }
            }
            else
            {
                this.blnCanPlay = true;
                DataRow cArrangeRowByClubID = BTPClubManager.GetCArrangeRowByClubID(intClubIDH);
                DataRow row2 = BTPClubManager.GetCArrangeRowByClubID(intClubIDA);
                if (intType == 2)
                {
                    IntPtr ptr;
                    DataRow dr = BTPArrangeLvlManager.GetArrange5Lvl(this.tHome.intUserID);
                    DataRow row4 = BTPArrangeLvlManager.GetArrange5Lvl(this.tAway.intUserID);
                    if (((this.intType == 2) && this.blUseStaffH) && (!this.blUseStaffA && (this.tAway.dtActiveTime.AddDays(1.0) > DateTime.Now)))
                    {
                        string str = cArrangeRowByClubID["ArrangeDev"].ToString().Trim();
                        string[] strArray = new string[7];
                        strArray = str.Split(new char[] { '|' });
                        string str2 = row2["ArrangeDev"].ToString().Trim();
                        string[] strArray2 = new string[7];
                        strArray2 = str2.Split(new char[] { '|' });
                        for (int i = 0; i < 7; i++)
                        {
                            if ((Convert.ToInt32(strArray[i]) > 0) && (Convert.ToInt32(strArray2[i]) > 0))
                            {
                                if (i == 5)
                                {
                                    this.tHome.intWUse = 0;
                                    this.tAway.intWUse = 0;
                                }
                                if (i == 6)
                                {
                                    this.tHome.intLUse = 0;
                                    this.tAway.intLUse = 0;
                                }
                                this.aHomes[i] = new Arrange(Convert.ToInt32(strArray[i]), this.tHome, 2);
                                this.aAways[i] = new Arrange(Convert.ToInt32(strArray[i]), this.tAway, 2);
                                this.intRand = this.rnd.Next(100);
                                if (this.intRand <= 30)
                                {
                                    if (this.aAways[i].intOffense == 1)
                                    {
                                        this.aHomes[i].intDefense = 5;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[4]++;
                                        this.intAOffs[0]++;
                                    }
                                    else if (this.aAways[i].intOffense == 2)
                                    {
                                        this.aHomes[i].intDefense = 1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[0]++;
                                        this.intAOffs[1]++;
                                    }
                                    else if (this.aAways[i].intOffense == 3)
                                    {
                                        this.aHomes[i].intDefense = 6;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[5]++;
                                        this.intAOffs[2]++;
                                    }
                                    else if (this.aAways[i].intOffense == 4)
                                    {
                                        this.aHomes[i].intDefense = 4;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[3]++;
                                        this.intAOffs[3]++;
                                    }
                                    else if (this.aAways[i].intOffense == 5)
                                    {
                                        this.aHomes[i].intDefense = 3;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[2]++;
                                        this.intAOffs[4]++;
                                    }
                                    else if (this.aAways[i].intOffense == 6)
                                    {
                                        this.aHomes[i].intDefense = 2;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[1]++;
                                        this.intAOffs[5]++;
                                    }
                                    if (this.aAways[i].intDefense == 1)
                                    {
                                        this.aHomes[i].intOffense = 3;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[2]++;
                                        this.intADefs[0]++;
                                    }
                                    else if (this.aAways[i].intDefense == 2)
                                    {
                                        this.aHomes[i].intOffense = 1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[0]++;
                                        this.intADefs[1]++;
                                    }
                                    else if (this.aAways[i].intDefense == 3)
                                    {
                                        this.aHomes[i].intOffense = 4;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[3]++;
                                        this.intADefs[2]++;
                                    }
                                    else if (this.aAways[i].intDefense == 4)
                                    {
                                        this.aHomes[i].intOffense = 6;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[5]++;
                                        this.intADefs[3]++;
                                    }
                                    else if (this.aAways[i].intDefense == 5)
                                    {
                                        this.aHomes[i].intOffense = 6;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[5]++;
                                        this.intADefs[4]++;
                                    }
                                    else if (this.aAways[i].intDefense == 6)
                                    {
                                        this.aHomes[i].intOffense = 2;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[1]++;
                                        this.intADefs[5]++;
                                    }
                                }
                                else if ((this.intRand > 30) && (this.intRand <= 80))
                                {
                                    this.intRand1 = this.rnd.Next(100);
                                    if (this.aAways[i].intOffense == 1)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 3;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[2]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 4;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[3]++;
                                        }
                                        this.intAOffs[0]++;
                                    }
                                    else if (this.aAways[i].intOffense == 2)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 3;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[2]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 4;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[3]++;
                                        }
                                        this.intAOffs[1]++;
                                    }
                                    else if (this.aAways[i].intOffense == 3)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 3;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[2]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 4;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[3]++;
                                        }
                                        this.intAOffs[2]++;
                                    }
                                    else if (this.aAways[i].intOffense == 4)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 5;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[4]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 6;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[5]++;
                                        }
                                        this.intAOffs[3]++;
                                    }
                                    else if (this.aAways[i].intOffense == 5)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 1;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[0]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 2;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[1]++;
                                        }
                                        this.intAOffs[4]++;
                                    }
                                    else if (this.aAways[i].intOffense == 6)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intDefense = 1;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[0]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intDefense = 6;
                                            this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                            this.intHDefs[5]++;
                                        }
                                        this.intAOffs[5]++;
                                    }
                                    if (this.aAways[i].intDefense == 1)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 4;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 5;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[4]++;
                                        }
                                        this.intADefs[0]++;
                                    }
                                    else if (this.aAways[i].intDefense == 2)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 4;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 5;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[4]++;
                                        }
                                        this.intADefs[1]++;
                                    }
                                    else if (this.aAways[i].intDefense == 3)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 4;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 5;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[4]++;
                                        }
                                        this.intADefs[2]++;
                                    }
                                    else if (this.aAways[i].intDefense == 4)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 4;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 6;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[5]++;
                                        }
                                        this.intADefs[3]++;
                                    }
                                    else if (this.aAways[i].intDefense == 5)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 1;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[0]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 2;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[1]++;
                                        }
                                        this.intADefs[4]++;
                                    }
                                    else if (this.aAways[i].intDefense == 6)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aHomes[i].intOffense = 1;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[0]++;
                                        }
                                        else
                                        {
                                            this.aHomes[i].intOffense = 3;
                                            this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                            this.intHOffs[2]++;
                                        }
                                        this.intADefs[5]++;
                                    }
                                }
                                else
                                {
                                    this.intRand1 = 1 + this.rnd.Next(6);
                                    if (this.intRand1 > 6)
                                    {
                                        this.intRand1 = 6;
                                    }
                                    if (this.aAways[i].intOffense == 1)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[0]++;
                                    }
                                    else if (this.aAways[i].intOffense == 2)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[1]++;
                                    }
                                    else if (this.aAways[i].intOffense == 3)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[2]++;
                                    }
                                    else if (this.aAways[i].intOffense == 4)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[3]++;
                                    }
                                    else if (this.aAways[i].intOffense == 5)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[4]++;
                                    }
                                    else if (this.aAways[i].intOffense == 6)
                                    {
                                        this.aHomes[i].intDefense = this.intRand1;
                                        this.aHomes[i].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[i].intDefense);
                                        this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                        this.intAOffs[5]++;
                                    }
                                    if (this.aAways[i].intDefense == 1)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[0]++;
                                    }
                                    else if (this.aAways[i].intDefense == 2)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[1]++;
                                    }
                                    else if (this.aAways[i].intDefense == 3)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[2]++;
                                    }
                                    else if (this.aAways[i].intDefense == 4)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[3]++;
                                    }
                                    else if (this.aAways[i].intDefense == 5)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[4]++;
                                    }
                                    else if (this.aAways[i].intDefense == 6)
                                    {
                                        this.aHomes[i].intOffense = this.intRand1;
                                        this.aHomes[i].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[i].intOffense);
                                        this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                        this.intADefs[5]++;
                                    }
                                }
                            }
                        }
                    }
                    else if (((this.intType == 2) && this.blUseStaffH) && (!this.blUseStaffA && (this.tAway.dtActiveTime.AddDays(1.0) < DateTime.Now)))
                    {
                        string devMatchArrange = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray3 = new string[7];
                        if ((devMatchArrange.Trim() != "NO") && (devMatchArrange.Trim() != ""))
                        {
                            strArray3 = devMatchArrange.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray3 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        string str4 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray4 = new string[7];
                        if ((str4.Trim() != "NO") && (str4.Trim() != ""))
                        {
                            strArray4 = str4.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray4 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        for (int j = 0; j < 7; j++)
                        {
                            if ((Convert.ToInt32(strArray3[j]) > 0) && (Convert.ToInt32(strArray4[j]) > 0))
                            {
                                if (j == 5)
                                {
                                    this.tHome.intWUse = 0;
                                    this.tAway.intWUse = 0;
                                }
                                if (j == 6)
                                {
                                    this.tHome.intLUse = 0;
                                    this.tAway.intLUse = 0;
                                }
                                this.aHomes[j] = new Arrange(Convert.ToInt32(strArray3[j]), this.tHome, 2);
                                this.aAways[j] = new Arrange(Convert.ToInt32(strArray3[j]), this.tAway, 2);
                                if (this.aAways[j].intOffense == 1)
                                {
                                    this.aHomes[j].intDefense = 5;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[4]++;
                                    this.intAOffs[0]++;
                                }
                                else if (this.aAways[j].intOffense == 2)
                                {
                                    this.aHomes[j].intDefense = 1;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[0]++;
                                    this.intAOffs[1]++;
                                }
                                else if (this.aAways[j].intOffense == 3)
                                {
                                    this.aHomes[j].intDefense = 6;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[5]++;
                                    this.intAOffs[2]++;
                                }
                                else if (this.aAways[j].intOffense == 4)
                                {
                                    this.aHomes[j].intDefense = 4;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[3]++;
                                    this.intAOffs[3]++;
                                }
                                else if (this.aAways[j].intOffense == 5)
                                {
                                    this.aHomes[j].intDefense = 3;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[2]++;
                                    this.intAOffs[4]++;
                                }
                                else if (this.aAways[j].intOffense == 6)
                                {
                                    this.aHomes[j].intDefense = 2;
                                    this.aHomes[j].intDefAdd = new Arrange().GetDefAddByDefense(dr, this.aHomes[j].intOffense);
                                    this.intHDefs[1]++;
                                    this.intAOffs[5]++;
                                }
                                if (this.aAways[j].intDefense == 1)
                                {
                                    this.aHomes[j].intOffense = 3;
                                    this.aHomes[j].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[j].intOffense);
                                    this.intHOffs[2]++;
                                    this.intADefs[0]++;
                                }
                                else if (this.aAways[j].intDefense == 2)
                                {
                                    this.aHomes[j].intOffense = 1;
                                    this.intHOffs[0]++;
                                    this.intADefs[1]++;
                                }
                                else if (this.aAways[j].intDefense == 3)
                                {
                                    this.aHomes[j].intOffense = 4;
                                    this.aHomes[j].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[j].intOffense);
                                    this.intHOffs[3]++;
                                    this.intADefs[2]++;
                                }
                                else if (this.aAways[j].intDefense == 4)
                                {
                                    this.aHomes[j].intOffense = 6;
                                    this.aHomes[j].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[j].intOffense);
                                    this.intHOffs[5]++;
                                    this.intADefs[3]++;
                                }
                                else if (this.aAways[j].intDefense == 5)
                                {
                                    this.aHomes[j].intOffense = 6;
                                    this.aHomes[j].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[j].intOffense);
                                    this.intHOffs[5]++;
                                    this.intADefs[4]++;
                                }
                                else if (this.aAways[j].intDefense == 6)
                                {
                                    this.aHomes[j].intOffense = 2;
                                    this.aHomes[j].intOffAdd = new Arrange().GetOffAddByOffense(dr, this.aHomes[j].intOffense);
                                    this.intHOffs[1]++;
                                    this.intADefs[5]++;
                                }
                            }
                        }
                    }
                    else if (((this.intType == 2) && !this.blUseStaffH) && (this.blUseStaffA && (this.tHome.dtActiveTime.AddDays(1.0) > DateTime.Now)))
                    {
                        string str5 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray5 = new string[7];
                        if ((str5.Trim() != "NO") && (str5.Trim() != ""))
                        {
                            strArray5 = str5.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray5 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        string str6 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray6 = new string[7];
                        if ((str6.Trim() != "NO") && (str6.Trim() != ""))
                        {
                            strArray6 = str6.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray6 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        for (int k = 0; k < 7; k++)
                        {
                            if ((Convert.ToInt32(strArray5[k]) > 0) && (Convert.ToInt32(strArray6[k]) > 0))
                            {
                                if (k == 5)
                                {
                                    this.tHome.intWUse = 0;
                                    this.tAway.intWUse = 0;
                                }
                                if (k == 6)
                                {
                                    this.tHome.intLUse = 0;
                                    this.tAway.intLUse = 0;
                                }
                                this.aHomes[k] = new Arrange(Convert.ToInt32(strArray5[k]), this.tHome, 2);
                                this.aAways[k] = new Arrange(Convert.ToInt32(strArray6[k]), this.tAway, 2);
                                this.intRand = this.rnd.Next(100);
                                if (this.intRand <= 30)
                                {
                                    if (this.aHomes[k].intOffense == 1)
                                    {
                                        this.aAways[k].intDefense = 5;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[4]++;
                                        this.intHOffs[0]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 2)
                                    {
                                        this.aAways[k].intDefense = 1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[0]++;
                                        this.intHOffs[1]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 3)
                                    {
                                        this.aAways[k].intDefense = 6;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[5]++;
                                        this.intHOffs[2]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 4)
                                    {
                                        this.aAways[k].intDefense = 4;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[3]++;
                                        this.intHOffs[3]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 5)
                                    {
                                        this.aAways[k].intDefense = 3;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[2]++;
                                        this.intHOffs[4]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 6)
                                    {
                                        this.aAways[k].intDefense = 2;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[1]++;
                                        this.intHOffs[5]++;
                                    }
                                    if (this.aHomes[k].intDefense == 1)
                                    {
                                        this.aAways[k].intOffense = 3;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[2]++;
                                        this.intHDefs[0]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 2)
                                    {
                                        this.aAways[k].intOffense = 1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[0]++;
                                        this.intHDefs[1]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 3)
                                    {
                                        this.aAways[k].intOffense = 4;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[3]++;
                                        this.intHDefs[2]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 4)
                                    {
                                        this.aAways[k].intOffense = 6;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[5]++;
                                        this.intHDefs[3]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 5)
                                    {
                                        this.aAways[k].intOffense = 6;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[5]++;
                                        this.intHDefs[4]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 6)
                                    {
                                        this.aAways[k].intOffense = 2;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[1]++;
                                        this.intHDefs[5]++;
                                    }
                                }
                                else if ((this.intRand > 30) && (this.intRand <= 80))
                                {
                                    this.intRand1 = this.rnd.Next(100);
                                    if (this.aHomes[k].intOffense == 1)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 3;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[2]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 4;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[3]++;
                                        }
                                        this.intHOffs[0]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 2)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 3;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[2]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 4;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[3]++;
                                        }
                                        this.intHOffs[1]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 3)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 3;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[2]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 4;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[3]++;
                                        }
                                        this.intHOffs[2]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 4)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 5;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[4]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 6;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[5]++;
                                        }
                                        this.intHOffs[3]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 5)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 1;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[0]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 2;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[1]++;
                                        }
                                        this.intHOffs[4]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 6)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intDefense = 1;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[0]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intDefense = 6;
                                            this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                            this.intADefs[5]++;
                                        }
                                        this.intHOffs[5]++;
                                    }
                                    if (this.aHomes[k].intDefense == 1)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 4;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 5;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[4]++;
                                        }
                                        this.intHDefs[0]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 2)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 4;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 5;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[4]++;
                                        }
                                        this.intHDefs[1]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 3)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 4;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 5;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[4]++;
                                        }
                                        this.intHDefs[2]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 4)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 4;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[3]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 6;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[5]++;
                                        }
                                        this.intHDefs[3]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 5)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 1;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[0]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 2;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[1]++;
                                        }
                                        this.intHDefs[4]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 6)
                                    {
                                        if (this.intRand1 <= 50)
                                        {
                                            this.aAways[k].intOffense = 1;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[0]++;
                                        }
                                        else
                                        {
                                            this.aAways[k].intOffense = 3;
                                            this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                            this.intAOffs[2]++;
                                        }
                                        this.intHDefs[5]++;
                                    }
                                }
                                else
                                {
                                    this.intRand1 = 1 + this.rnd.Next(6);
                                    if (this.intRand1 > 6)
                                    {
                                        this.intRand1 = 6;
                                    }
                                    if (this.intRand1 > 6)
                                    {
                                        this.intRand1 = 6;
                                    }
                                    this.aHomes[k].intDefense = this.intRand1;
                                    this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                    if (this.aHomes[k].intOffense == 1)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[0]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 2)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[1]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 3)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[2]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 4)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[3]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 5)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[4]++;
                                    }
                                    else if (this.aHomes[k].intOffense == 6)
                                    {
                                        this.aAways[k].intDefense = this.intRand1;
                                        this.aAways[k].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[k].intDefense);
                                        this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                                        this.intHOffs[5]++;
                                    }
                                    if (this.aHomes[k].intDefense == 1)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[0]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 2)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[1]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 3)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[2]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 4)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[3]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 5)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[4]++;
                                    }
                                    else if (this.aHomes[k].intDefense == 6)
                                    {
                                        this.aAways[k].intOffense = this.intRand1;
                                        this.aAways[k].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[k].intOffense);
                                        this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                        this.intHDefs[5]++;
                                    }
                                }
                            }
                        }
                    }
                    else if (((this.intType == 2) && !this.blUseStaffH) && (this.blUseStaffA && (this.tHome.dtActiveTime.AddDays(1.0) < DateTime.Now)))
                    {
                        string str7 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray7 = new string[7];
                        if ((str7.Trim() != "NO") && (str7.Trim() != ""))
                        {
                            strArray7 = str7.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray7 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        string str8 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray8 = new string[7];
                        if ((str8.Trim() != "NO") && (str8.Trim() != ""))
                        {
                            strArray8 = str8.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray8 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        for (int m = 0; m < 7; m++)
                        {
                            if ((Convert.ToInt32(strArray7[m]) > 0) && (Convert.ToInt32(strArray8[m]) > 0))
                            {
                                if (m == 5)
                                {
                                    this.tHome.intWUse = 0;
                                    this.tAway.intWUse = 0;
                                }
                                if (m == 6)
                                {
                                    this.tHome.intLUse = 0;
                                    this.tAway.intLUse = 0;
                                }
                                this.aHomes[m] = new Arrange(Convert.ToInt32(strArray7[m]), this.tHome, 2);
                                this.aAways[m] = new Arrange(Convert.ToInt32(strArray8[m]), this.tAway, 2);
                                if (this.aHomes[m].intOffense == 1)
                                {
                                    this.aAways[m].intDefense = 5;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[4]++;
                                    this.intHOffs[0]++;
                                }
                                else if (this.aHomes[m].intOffense == 2)
                                {
                                    this.aAways[m].intDefense = 1;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[0]++;
                                    this.intHOffs[1]++;
                                }
                                else if (this.aHomes[m].intOffense == 3)
                                {
                                    this.aAways[m].intDefense = 6;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[5]++;
                                    this.intHOffs[2]++;
                                }
                                else if (this.aHomes[m].intOffense == 4)
                                {
                                    this.aAways[m].intDefense = 4;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[3]++;
                                    this.intHOffs[3]++;
                                }
                                else if (this.aHomes[m].intOffense == 5)
                                {
                                    this.aAways[m].intDefense = 3;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[2]++;
                                    this.intHOffs[4]++;
                                }
                                else if (this.aHomes[m].intOffense == 6)
                                {
                                    this.aAways[m].intDefense = 2;
                                    this.aAways[m].intDefAdd = new Arrange().GetDefAddByDefense(row4, this.aAways[m].intDefense);
                                    this.intADefs[1]++;
                                    this.intHOffs[5]++;
                                }
                                if (this.aHomes[m].intDefense == 1)
                                {
                                    this.aAways[m].intOffense = 3;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[2]++;
                                    this.intHDefs[0]++;
                                }
                                else if (this.aHomes[m].intDefense == 2)
                                {
                                    this.aAways[m].intOffense = 1;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[0]++;
                                    this.intHDefs[1]++;
                                }
                                else if (this.aHomes[m].intDefense == 3)
                                {
                                    this.aAways[m].intOffense = 4;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[3]++;
                                    this.intHDefs[2]++;
                                }
                                else if (this.aHomes[m].intDefense == 4)
                                {
                                    this.aAways[m].intOffense = 6;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[5]++;
                                    this.intHDefs[3]++;
                                }
                                else if (this.aHomes[m].intDefense == 5)
                                {
                                    this.aAways[m].intOffense = 6;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[5]++;
                                    this.intHDefs[4]++;
                                }
                                else if (this.aHomes[m].intDefense == 6)
                                {
                                    this.aAways[m].intOffense = 2;
                                    this.aAways[m].intOffAdd = new Arrange().GetOffAddByOffense(row4, this.aAways[m].intOffense);
                                    this.intAOffs[1]++;
                                    this.intHDefs[5]++;
                                }
                            }
                        }
                    }
                    else if (((this.intType == 2) && this.blUseStaffH) && this.blUseStaffA)
                    {
                        string str9 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray9 = new string[7];
                        if ((str9.Trim() != "NO") && (str9.Trim() != ""))
                        {
                            strArray9 = str9.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray9 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        string str10 = BTPDevMatchManager.GetDevMatchArrange(intTag, true);
                        string[] strArray10 = new string[7];
                        if ((str10.Trim() != "NO") && (str10.Trim() != ""))
                        {
                            strArray10 = str10.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray10 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                        for (int n = 0; n < 7; n++)
                        {
                            if ((Convert.ToInt32(strArray9[n]) > 0) && (Convert.ToInt32(strArray10[n]) > 0))
                            {
                                if (n == 5)
                                {
                                    this.tHome.intWUse = 0;
                                    this.tAway.intWUse = 0;
                                }
                                if (n == 6)
                                {
                                    this.tHome.intLUse = 0;
                                    this.tAway.intLUse = 0;
                                }
                                Arrange arrange120 = new Arrange();
                                this.aHomes[n] = new Arrange(Convert.ToInt32(strArray9[n]), this.tHome, 2);
                                this.aAways[n] = new Arrange(Convert.ToInt32(strArray10[n]), this.tAway, 2);
                                this.intRand1 = 1 + this.rnd.Next(6);
                                if (this.intRand1 > 6)
                                {
                                    this.intRand1 = 6;
                                }
                                this.aHomes[n].intOffense = this.intRand1;
                                this.aHomes[n].intOffAdd = arrange120.GetOffAddByOffense(dr, this.aHomes[n].intOffense);
                                this.intHOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHOffs[(int) ptr] + 1;
                                this.intRand1 = 1 + this.rnd.Next(6);
                                if (this.intRand1 > 6)
                                {
                                    this.intRand1 = 6;
                                }
                                this.aHomes[n].intDefense = this.intRand1;
                                this.aHomes[n].intDefAdd = arrange120.GetDefAddByDefense(dr, this.aHomes[n].intDefense);
                                this.intHDefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intHDefs[(int) ptr] + 1;
                                this.intRand1 = 1 + this.rnd.Next(6);
                                if (this.intRand1 > 6)
                                {
                                    this.intRand1 = 6;
                                }
                                this.aAways[n].intOffense = this.intRand1;
                                this.aAways[n].intOffAdd = arrange120.GetOffAddByOffense(row4, this.aAways[n].intOffense);
                                this.intAOffs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intAOffs[(int) ptr] + 1;
                                this.intRand1 = 1 + this.rnd.Next(6);
                                if (this.intRand1 > 6)
                                {
                                    this.intRand1 = 6;
                                }
                                this.aAways[n].intDefense = this.intRand1;
                                this.aAways[n].intDefAdd = arrange120.GetDefAddByDefense(row4, this.aAways[n].intDefense);
                                this.intADefs[(int) (ptr = (IntPtr) (this.intRand1 - 1))] = this.intADefs[(int) ptr] + 1;
                            }
                        }
                    }
                    else
                    {
                        string str11 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim();
                        string[] strArray11 = new string[7];
                        strArray11 = str11.Split(new char[] { '|' });
                        for (int num6 = 0; num6 < 7; num6++)
                        {
                            if (Convert.ToInt32(strArray11[num6]) > 0)
                            {
                                if (num6 == 5)
                                {
                                    this.tHome.intWUse = 0;
                                }
                                if (num6 == 6)
                                {
                                    this.tHome.intLUse = 0;
                                }
                                this.aHomes[num6] = new Arrange(Convert.ToInt32(strArray11[num6]), this.tHome, intType);
                                if (this.aHomes[num6].intOffense == 1)
                                {
                                    this.intHOffs[0]++;
                                }
                                else if (this.aHomes[num6].intOffense == 2)
                                {
                                    this.intHOffs[1]++;
                                }
                                else if (this.aHomes[num6].intOffense == 3)
                                {
                                    this.intHOffs[2]++;
                                }
                                else if (this.aHomes[num6].intOffense == 4)
                                {
                                    this.intHOffs[3]++;
                                }
                                else if (this.aHomes[num6].intOffense == 5)
                                {
                                    this.intHOffs[4]++;
                                }
                                else if (this.aHomes[num6].intOffense == 6)
                                {
                                    this.intHOffs[5]++;
                                }
                                if (this.aHomes[num6].intDefense == 1)
                                {
                                    this.intHDefs[0]++;
                                }
                                else if (this.aHomes[num6].intDefense == 2)
                                {
                                    this.intHDefs[1]++;
                                }
                                else if (this.aHomes[num6].intDefense == 3)
                                {
                                    this.intHDefs[2]++;
                                }
                                else if (this.aHomes[num6].intDefense == 4)
                                {
                                    this.intHDefs[3]++;
                                }
                                else if (this.aHomes[num6].intDefense == 5)
                                {
                                    this.intHDefs[4]++;
                                }
                                else if (this.aHomes[num6].intDefense == 6)
                                {
                                    this.intHDefs[5]++;
                                }
                            }
                        }
                        string str12 = row2["ArrangeDev"].ToString().Trim();
                        string[] strArray12 = new string[7];
                        strArray12 = str12.Split(new char[] { '|' });
                        for (int num7 = 0; num7 < 7; num7++)
                        {
                            if (Convert.ToInt32(strArray12[num7]) > 0)
                            {
                                if (num7 == 5)
                                {
                                    this.tAway.intWUse = 0;
                                }
                                if (num7 == 6)
                                {
                                    this.tAway.intLUse = 0;
                                }
                                this.aAways[num7] = new Arrange(Convert.ToInt32(strArray12[num7]), this.tAway, intType);
                                if (this.aAways[num7].intOffense == 1)
                                {
                                    this.intAOffs[0]++;
                                }
                                else if (this.aAways[num7].intOffense == 2)
                                {
                                    this.intAOffs[1]++;
                                }
                                else if (this.aAways[num7].intOffense == 3)
                                {
                                    this.intAOffs[2]++;
                                }
                                else if (this.aAways[num7].intOffense == 4)
                                {
                                    this.intAOffs[3]++;
                                }
                                else if (this.aAways[num7].intOffense == 5)
                                {
                                    this.intAOffs[4]++;
                                }
                                else if (this.aAways[num7].intOffense == 6)
                                {
                                    this.intAOffs[5]++;
                                }
                                if (this.aAways[num7].intDefense == 1)
                                {
                                    this.intADefs[0]++;
                                }
                                else if (this.aAways[num7].intDefense == 2)
                                {
                                    this.intADefs[1]++;
                                }
                                else if (this.aAways[num7].intDefense == 3)
                                {
                                    this.intADefs[2]++;
                                }
                                else if (this.aAways[num7].intDefense == 4)
                                {
                                    this.intADefs[3]++;
                                }
                                else if (this.aAways[num7].intDefense == 5)
                                {
                                    this.intADefs[4]++;
                                }
                                else if (this.aAways[num7].intDefense == 6)
                                {
                                    this.intADefs[5]++;
                                }
                            }
                        }
                    }
                }
                else if (intType == 4)
                {
                    DataRow xGroupMatchRowByID = BTPXGroupMatchManager.GetXGroupMatchRowByID(intTag);
                    string str13 = (string) xGroupMatchRowByID["ArrangeH"];
                    string[] strArray13 = new string[7];
                    if ((str13.Trim() != "NO") && (str13.Trim() != ""))
                    {
                        strArray13 = str13.Split(new char[] { '|' });
                    }
                    else
                    {
                        str13 = (string) cArrangeRowByClubID["ArrangeCup"];
                        if ((str13.Trim() != "NO") && (str13.Trim() != ""))
                        {
                            strArray13 = str13.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray13 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                    }
                    for (int num8 = 0; num8 < 7; num8++)
                    {
                        if (Convert.ToInt32(strArray13[num8]) > 0)
                        {
                            if (num8 == 5)
                            {
                                this.tHome.intWUse = 0;
                            }
                            if (num8 == 6)
                            {
                                this.tHome.intLUse = 0;
                            }
                            this.aHomes[num8] = new Arrange(Convert.ToInt32(strArray13[num8]), this.tHome, intType);
                            if (this.aHomes[num8].intOffense == 1)
                            {
                                this.intHOffs[0]++;
                            }
                            else if (this.aHomes[num8].intOffense == 2)
                            {
                                this.intHOffs[1]++;
                            }
                            else if (this.aHomes[num8].intOffense == 3)
                            {
                                this.intHOffs[2]++;
                            }
                            else if (this.aHomes[num8].intOffense == 4)
                            {
                                this.intHOffs[3]++;
                            }
                            else if (this.aHomes[num8].intOffense == 5)
                            {
                                this.intHOffs[4]++;
                            }
                            else if (this.aHomes[num8].intOffense == 6)
                            {
                                this.intHOffs[5]++;
                            }
                            if (this.aHomes[num8].intDefense == 1)
                            {
                                this.intHDefs[0]++;
                            }
                            else if (this.aHomes[num8].intDefense == 2)
                            {
                                this.intHDefs[1]++;
                            }
                            else if (this.aHomes[num8].intDefense == 3)
                            {
                                this.intHDefs[2]++;
                            }
                            else if (this.aHomes[num8].intDefense == 4)
                            {
                                this.intHDefs[3]++;
                            }
                            else if (this.aHomes[num8].intDefense == 5)
                            {
                                this.intHDefs[4]++;
                            }
                            else if (this.aHomes[num8].intDefense == 6)
                            {
                                this.intHDefs[5]++;
                            }
                        }
                    }
                    string str14 = (string) xGroupMatchRowByID["ArrangeA"];
                    string[] strArray14 = new string[7];
                    if ((str14.Trim() != "NO") && (str14.Trim() != ""))
                    {
                        strArray14 = str14.Split(new char[] { '|' });
                    }
                    else
                    {
                        str14 = (string) row2["ArrangeCup"];
                        if ((str14.Trim() != "NO") && (str14.Trim() != ""))
                        {
                            strArray14 = str14.Split(new char[] { '|' });
                        }
                        else
                        {
                            strArray14 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                        }
                    }
                    for (int num9 = 0; num9 < 7; num9++)
                    {
                        if (Convert.ToInt32(strArray14[num9]) > 0)
                        {
                            if (num9 == 5)
                            {
                                this.tAway.intWUse = 0;
                            }
                            if (num9 == 6)
                            {
                                this.tAway.intLUse = 0;
                            }
                            this.aAways[num9] = new Arrange(Convert.ToInt32(strArray14[num9]), this.tAway, intType);
                            if (this.aAways[num9].intOffense == 1)
                            {
                                this.intAOffs[0]++;
                            }
                            else if (this.aAways[num9].intOffense == 2)
                            {
                                this.intAOffs[1]++;
                            }
                            else if (this.aAways[num9].intOffense == 3)
                            {
                                this.intAOffs[2]++;
                            }
                            else if (this.aAways[num9].intOffense == 4)
                            {
                                this.intAOffs[3]++;
                            }
                            else if (this.aAways[num9].intOffense == 5)
                            {
                                this.intAOffs[4]++;
                            }
                            else if (this.aAways[num9].intOffense == 6)
                            {
                                this.intHOffs[5]++;
                            }
                            if (this.aAways[num9].intDefense == 1)
                            {
                                this.intADefs[0]++;
                            }
                            else if (this.aAways[num9].intDefense == 2)
                            {
                                this.intADefs[1]++;
                            }
                            else if (this.aAways[num9].intDefense == 3)
                            {
                                this.intADefs[2]++;
                            }
                            else if (this.aAways[num9].intDefense == 4)
                            {
                                this.intADefs[3]++;
                            }
                            else if (this.aAways[num9].intDefense == 5)
                            {
                                this.intADefs[4]++;
                            }
                            else if (this.aAways[num9].intDefense == 6)
                            {
                                this.intADefs[5]++;
                            }
                        }
                    }
                }
                else if ((intType == 6) || (intType == 5))
                {
                    string str15 = (string) cArrangeRowByClubID["ArrangeCup"];
                    string[] strArray15 = new string[7];
                    if ((str15.Trim() != "NO") && (str15.Trim() != ""))
                    {
                        strArray15 = str15.Split(new char[] { '|' });
                    }
                    else
                    {
                        strArray15 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                    }
                    for (int num10 = 0; num10 < 7; num10++)
                    {
                        if (Convert.ToInt32(strArray15[num10]) > 0)
                        {
                            if (num10 == 5)
                            {
                                this.tHome.intWUse = 0;
                            }
                            if (num10 == 6)
                            {
                                this.tHome.intLUse = 0;
                            }
                            this.aHomes[num10] = new Arrange(Convert.ToInt32(strArray15[num10]), this.tHome, intType);
                            if (this.aHomes[num10].intOffense == 1)
                            {
                                this.intHOffs[0]++;
                            }
                            else if (this.aHomes[num10].intOffense == 2)
                            {
                                this.intHOffs[1]++;
                            }
                            else if (this.aHomes[num10].intOffense == 3)
                            {
                                this.intHOffs[2]++;
                            }
                            else if (this.aHomes[num10].intOffense == 4)
                            {
                                this.intHOffs[3]++;
                            }
                            else if (this.aHomes[num10].intOffense == 5)
                            {
                                this.intHOffs[4]++;
                            }
                            else if (this.aHomes[num10].intOffense == 6)
                            {
                                this.intHOffs[5]++;
                            }
                            if (this.aHomes[num10].intDefense == 1)
                            {
                                this.intHDefs[0]++;
                            }
                            else if (this.aHomes[num10].intDefense == 2)
                            {
                                this.intHDefs[1]++;
                            }
                            else if (this.aHomes[num10].intDefense == 3)
                            {
                                this.intHDefs[2]++;
                            }
                            else if (this.aHomes[num10].intDefense == 4)
                            {
                                this.intHDefs[3]++;
                            }
                            else if (this.aHomes[num10].intDefense == 5)
                            {
                                this.intHDefs[4]++;
                            }
                            else if (this.aHomes[num10].intDefense == 6)
                            {
                                this.intHDefs[5]++;
                            }
                        }
                    }
                    string str16 = (string) row2["ArrangeCup"];
                    string[] strArray16 = new string[7];
                    if ((str16.Trim() != "NO") && (str16.Trim() != ""))
                    {
                        strArray16 = str16.Split(new char[] { '|' });
                    }
                    else
                    {
                        strArray16 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                    }
                    for (int num11 = 0; num11 < 7; num11++)
                    {
                        if (Convert.ToInt32(strArray16[num11]) > 0)
                        {
                            if (num11 == 5)
                            {
                                this.tAway.intWUse = 0;
                            }
                            if (num11 == 6)
                            {
                                this.tAway.intLUse = 0;
                            }
                            this.aAways[num11] = new Arrange(Convert.ToInt32(strArray16[num11]), this.tAway, intType);
                            if (this.aAways[num11].intOffense == 1)
                            {
                                this.intAOffs[0]++;
                            }
                            else if (this.aAways[num11].intOffense == 2)
                            {
                                this.intAOffs[1]++;
                            }
                            else if (this.aAways[num11].intOffense == 3)
                            {
                                this.intAOffs[2]++;
                            }
                            else if (this.aAways[num11].intOffense == 4)
                            {
                                this.intAOffs[3]++;
                            }
                            else if (this.aAways[num11].intOffense == 5)
                            {
                                this.intAOffs[4]++;
                            }
                            else if (this.aAways[num11].intOffense == 6)
                            {
                                this.intAOffs[5]++;
                            }
                            if (this.aAways[num11].intDefense == 1)
                            {
                                this.intADefs[0]++;
                            }
                            else if (this.aAways[num11].intDefense == 2)
                            {
                                this.intADefs[1]++;
                            }
                            else if (this.aAways[num11].intDefense == 3)
                            {
                                this.intADefs[2]++;
                            }
                            else if (this.aAways[num11].intDefense == 4)
                            {
                                this.intADefs[3]++;
                            }
                            else if (this.aAways[num11].intDefense == 5)
                            {
                                this.intADefs[4]++;
                            }
                            else if (this.aAways[num11].intDefense == 6)
                            {
                                this.intADefs[5]++;
                            }
                        }
                    }
                }
                else
                {
                    string str17 = (string) cArrangeRowByClubID["ArrangeOther"];
                    string[] strArray17 = new string[7];
                    if ((str17.Trim() != "NO") && (str17.Trim() != ""))
                    {
                        strArray17 = str17.Split(new char[] { '|' });
                    }
                    else
                    {
                        strArray17 = cArrangeRowByClubID["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                    }
                    for (int num12 = 0; num12 < 7; num12++)
                    {
                        if (Convert.ToInt32(strArray17[num12]) > 0)
                        {
                            if (num12 == 5)
                            {
                                this.tHome.intWUse = 0;
                            }
                            if (num12 == 6)
                            {
                                this.tHome.intLUse = 0;
                            }
                            this.aHomes[num12] = new Arrange(Convert.ToInt32(strArray17[num12]), this.tHome, intType);
                            if (this.aHomes[num12].intOffense == 1)
                            {
                                this.intHOffs[0]++;
                            }
                            else if (this.aHomes[num12].intOffense == 2)
                            {
                                this.intHOffs[1]++;
                            }
                            else if (this.aHomes[num12].intOffense == 3)
                            {
                                this.intHOffs[2]++;
                            }
                            else if (this.aHomes[num12].intOffense == 4)
                            {
                                this.intHOffs[3]++;
                            }
                            else if (this.aHomes[num12].intOffense == 5)
                            {
                                this.intHOffs[4]++;
                            }
                            else if (this.aHomes[num12].intOffense == 6)
                            {
                                this.intHOffs[5]++;
                            }
                            if (this.aHomes[num12].intDefense == 1)
                            {
                                this.intHDefs[0]++;
                            }
                            else if (this.aHomes[num12].intDefense == 2)
                            {
                                this.intHDefs[1]++;
                            }
                            else if (this.aHomes[num12].intDefense == 3)
                            {
                                this.intHDefs[2]++;
                            }
                            else if (this.aHomes[num12].intDefense == 4)
                            {
                                this.intHDefs[3]++;
                            }
                            else if (this.aHomes[num12].intDefense == 5)
                            {
                                this.intHDefs[4]++;
                            }
                            else if (this.aHomes[num12].intDefense == 6)
                            {
                                this.intHDefs[5]++;
                            }
                        }
                    }
                    string str18 = (string) row2["ArrangeOther"];
                    string[] strArray18 = new string[7];
                    if ((str18.Trim() != "NO") && (str18.Trim() != ""))
                    {
                        strArray18 = str18.Split(new char[] { '|' });
                    }
                    else
                    {
                        strArray18 = row2["ArrangeDev"].ToString().Trim().Split(new char[] { '|' });
                    }
                    for (int num13 = 0; num13 < 7; num13++)
                    {
                        if (Convert.ToInt32(strArray18[num13]) > 0)
                        {
                            if (num13 == 5)
                            {
                                this.tAway.intWUse = 0;
                            }
                            if (num13 == 6)
                            {
                                this.tAway.intLUse = 0;
                            }
                            this.aAways[num13] = new Arrange(Convert.ToInt32(strArray18[num13]), this.tAway, intType);
                            if (this.aAways[num13].intOffense == 1)
                            {
                                this.intAOffs[0]++;
                            }
                            else if (this.aAways[num13].intOffense == 2)
                            {
                                this.intAOffs[1]++;
                            }
                            else if (this.aAways[num13].intOffense == 3)
                            {
                                this.intAOffs[2]++;
                            }
                            else if (this.aAways[num13].intOffense == 4)
                            {
                                this.intAOffs[3]++;
                            }
                            else if (this.aAways[num13].intOffense == 5)
                            {
                                this.intAOffs[4]++;
                            }
                            else if (this.aAways[num13].intOffense == 6)
                            {
                                this.intAOffs[5]++;
                            }
                            if (this.aAways[num13].intDefense == 1)
                            {
                                this.intADefs[0]++;
                            }
                            else if (this.aAways[num13].intDefense == 2)
                            {
                                this.intADefs[1]++;
                            }
                            else if (this.aAways[num13].intDefense == 3)
                            {
                                this.intADefs[2]++;
                            }
                            else if (this.aAways[num13].intDefense == 4)
                            {
                                this.intADefs[3]++;
                            }
                            else if (this.aAways[num13].intDefense == 5)
                            {
                                this.intADefs[4]++;
                            }
                            else if (this.aAways[num13].intDefense == 6)
                            {
                                this.intADefs[5]++;
                            }
                        }
                    }
                }
                if (this.intType == 7)
                {
                    Player player;
                    IDictionaryEnumerator enumerator = this.tHome.players.GetEnumerator();
                    int num14 = 0;
                    int intPower = 100;
                    while (enumerator.MoveNext())
                    {
                        player = (Player) enumerator.Value;
                        num14 += player.intPower;
                        if (intPower > player.intPower)
                        {
                            intPower = player.intPower;
                        }
                    }
                    if (((num14 / this.tHome.players.Count) > 70) && (intPower > 50))
                    {
                        this.blnCanAddH = true;
                    }
                    enumerator = this.tAway.players.GetEnumerator();
                    num14 = 0;
                    intPower = 100;
                    while (enumerator.MoveNext())
                    {
                        player = (Player) enumerator.Value;
                        num14 += player.intPower;
                        if (intPower > player.intPower)
                        {
                            intPower = player.intPower;
                        }
                    }
                    if (((num14 / this.tAway.players.Count) > 70) && (intPower > 50))
                    {
                        this.blnCanAddA = true;
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
            if (this.blnPower && (this.intType == 2))
            {
                BTPArrangeLvlManager.SetArrange5Point(this.tHome.intClubID, this.intHOffs, this.intHDefs);
                BTPArrangeLvlManager.SetArrange5Point(this.tAway.intClubID, this.intAOffs, this.intADefs);
            }
            else if (this.blnPower && (this.intType == 7))
            {
                if (flag)
                {
                    if (this.blnCanAddH)
                    {
                        BTPArrangeLvlManager.SetArrange5Point(this.tHome.intClubID, this.intHOffs, this.intHDefs);
                    }
                }
                else if (this.blnCanAddA)
                {
                    BTPArrangeLvlManager.SetArrange5Point(this.tAway.intClubID, this.intAOffs, this.intADefs);
                }
            }
            bool flag2 = false;
            bool flag3 = false;
            if (((this.intType == 2) || (this.intType == 10)) || (this.intType == 11))
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
                        PlayerItem.ChangePlayerFromArrange5(player.longPlayerID, this.tHome.intClubID);
                        BTPMessageManager.AddMessageByClubID5(this.tHome.intClubID, string.Concat(new object[] { player.strName.Replace("&lt;u&gt;", "").Replace("&lt;/u&gt;", ""), strEvent, "，需停赛", intSuspend, "轮。" }));
                        try
                        {
                            string str2 = BTPClubManager.GetClubNameByClubID(this.tHome.intClubID, 5, "Management", 100);
                            string strLogEvent = string.Concat(new object[] { str2, "中的", player.strName.Replace("&lt;u&gt;", "").Replace("&lt;/u&gt;", ""), strEvent, "，需停赛", intSuspend, "轮。" });
                            BTPDevManager.SetDevLogByClubID5(this.tHome.intClubID, strLogEvent);
                        }
                        catch
                        {
                        }
                    }
                    if (this.intType != 7)
                    {
                        int intTrainPoint = 0;
                        if (((this.intType == 2) || (this.intType == 10)) || (this.intType == 11))
                        {
                            if (player.blnPlayed)
                            {
                                intTrainPoint = ((((((((((player.intScore + (player.intAst * 2)) + (player.intOReb * 2)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * player.intSkillPotential) / 9) + ((30 - player.intAge) * 0x7d0)) + ((player.intSkillPotential * 0x73) / 10)) / 80) + (((((0x21 - player.intAge) * 4) * ((player.intAge / 3) + 0x27)) / 3) / 1);
                                if (intTrainPoint < 0x4b0)
                                {
                                    intTrainPoint = ((this.rnd.Next(0, 100) + 800) * 150) / 100;
                                    if (player.intAge > 30)
                                    {
                                        intTrainPoint = (intTrainPoint * (15 - (player.intAge - 30))) / 15;
                                    }
                                }
                            }
                            else
                            {
                                intTrainPoint = ((this.rnd.Next(0, 100) + 600) * 150) / 100;
                                if (player.intAge > 30)
                                {
                                    intTrainPoint = (intTrainPoint * (15 - (player.intAge - 30))) / 15;
                                }
                            }
                        }
                        if (flag2)
                        {
                            intTrainPoint *= 2;
                        }
                        if (this.intType == 2)
                        {
                            BTPPlayer5Manager.UpdatePlayerStas(player.longPlayerID, player.intScore, player.intOReb + player.intDReb, player.intAst, player.intBlk, player.intStl, player.blnPlayed, player.intHappy, player.intPower, intTrainPoint, intStatus, strEvent, intSuspend);
                        }
                        else if ((this.intType == 10) || (this.intType == 11))
                        {
                            intTrainPoint = (intTrainPoint * 8) / 100;
                            BTPPlayer5Manager.UpdatePlayer5TrainPoint(player.longPlayerID, player.intHappy, player.intPower, intTrainPoint, intStatus, strEvent, intSuspend);
                        }
                    }
                    else
                    {
                        BTPPlayer5Manager.UpdatePlayerStasType7(player.longPlayerID, player.intHappy, player.intPower, intStatus, intSuspend, strEvent);
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
                    int num6 = 1;
                    int num7 = 0;
                    string str4 = "";
                    if (player.blnInjured)
                    {
                        InjuryGenerator generator2 = new InjuryGenerator(player.intPower);
                        generator2.SetEvent();
                        num6 = 2;
                        num7 = generator2.intSuspend;
                        str4 = generator2.strEvent;
                        PlayerItem.ChangePlayerFromArrange5(player.longPlayerID, this.tAway.intClubID);
                        BTPMessageManager.AddMessageByClubID5(this.tAway.intClubID, string.Concat(new object[] { player.strName.Replace("&lt;u&gt;", "").Replace("&lt;/u&gt;", ""), str4, "，需停赛", num7, "轮。" }));
                        try
                        {
                            string str5 = BTPClubManager.GetClubNameByClubID(this.tAway.intClubID, 5, "Management", 100);
                            string str6 = string.Concat(new object[] { str5, "中的", player.strName.Replace("&lt;u&gt;", "").Replace("&lt;/u&gt;", ""), str4, "，需停赛", num7, "轮。" });
                            BTPDevManager.SetDevLogByClubID5(this.tAway.intClubID, str6);
                        }
                        catch
                        {
                        }
                    }
                    if (this.intType != 7)
                    {
                        int num8 = 0;
                        if (((this.intType == 2) || (this.intType == 10)) || (this.intType == 11))
                        {
                            if (player.blnPlayed)
                            {
                                num8 = ((((((((((player.intScore + (player.intAst * 2)) + (player.intOReb * 2)) + (player.intDReb * 2)) + (player.intBlk * 4)) + (player.intStl * 4)) * player.intSkillPotential) / 9) + ((30 - player.intAge) * 0x7d0)) + ((player.intSkillPotential * 0x73) / 10)) / 80) + (((((0x21 - player.intAge) * 4) * ((player.intAge / 3) + 0x27)) / 3) / 1);
                                if (num8 < 0x4b0)
                                {
                                    num8 = ((this.rnd.Next(0, 100) + 800) * 150) / 100;
                                    if (player.intAge > 30)
                                    {
                                        num8 = (num8 * (15 - (player.intAge - 30))) / 15;
                                    }
                                }
                            }
                            else
                            {
                                num8 = ((this.rnd.Next(0, 100) + 600) * 150) / 100;
                                if (player.intAge > 30)
                                {
                                    num8 = (num8 * (15 - (player.intAge - 30))) / 15;
                                }
                            }
                        }
                        if (flag3)
                        {
                            num8 *= 2;
                        }
                        if (this.intType == 2)
                        {
                            BTPPlayer5Manager.UpdatePlayerStas(player.longPlayerID, player.intScore, player.intOReb + player.intDReb, player.intAst, player.intBlk, player.intStl, player.blnPlayed, player.intHappy, player.intPower, num8, num6, str4, num7);
                        }
                        else if ((this.intType == 10) || (this.intType == 11))
                        {
                            num8 = (num8 * 8) / 100;
                            BTPPlayer5Manager.UpdatePlayer5TrainPoint(player.longPlayerID, player.intHappy, player.intPower, num8, num6, str4, num7);
                        }
                    }
                    else
                    {
                        BTPPlayer5Manager.UpdatePlayerStasType7(player.longPlayerID, player.intHappy, player.intPower, num6, num7, str4);
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
            int num9 = ((quarter.GetMethodAddForMatch(this.aAways[0].intOffense, this.aHomes[0].intDefense) + quarter.GetMethodAddForMatch(this.aAways[1].intOffense, this.aHomes[1].intDefense)) + quarter.GetMethodAddForMatch(this.aAways[2].intOffense, this.aHomes[2].intDefense)) + quarter.GetMethodAddForMatch(this.aAways[3].intOffense, this.aHomes[3].intDefense);
            int num10 = ((quarter.GetMethodAddForMatch(this.aHomes[0].intOffense, this.aAways[0].intDefense) + quarter.GetMethodAddForMatch(this.aHomes[1].intOffense, this.aAways[1].intDefense)) + quarter.GetMethodAddForMatch(this.aHomes[2].intOffense, this.aAways[2].intDefense)) + quarter.GetMethodAddForMatch(this.aHomes[3].intOffense, this.aAways[3].intDefense);
            if (flag)
            {
                this.intWinAbility = this.intAbilitySumH / this.intPlayedCountH;
                this.intLoseAbility = this.intAbilitySumA / this.intPlayedCountA;
                if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了经理谨慎的赛前工作,在球队的整体配合与战术的合理安排下," + this.pMVP.strName + "发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "在队友的大力支持与战术的合理安排下,发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbility)) && ((num9 <= num10) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然在战术上没有占得优势,但是由于精心的赛前准备和全队的卓越实力,比赛最终获胜." + this.pMVP.strName + "获得了本场的MVP!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("全队成员在合理的战术安排下赢得了本场比赛," + this.pMVP.strName + "获得了本场的MVP."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("队员们受到经理勤奋的赛前工作的鼓舞,士气大振!超常发挥!赢得了本场比赛的胜利.作为球队核心的" + this.pMVP.strName + "勇夺MVP桂冠!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛没有过多的赛前工作,完全靠球员自身的强大能力获得胜利.这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("尽管赛前部署很周密,但是却没有换得战术的优势,凭借球员自身的能力赢得了胜利." + this.pMVP.strName + "在队友的支持下成为了本场的MVP!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("比赛胜利的关键是经理精心的赛前工作和战术的合理利用,全队打出了120%的实力." + this.pMVP.strName + "成为球队中发挥最好的球员."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然战术设计不占有优势,但是经理细致的赛前工作使得" + this.pMVP.strName + "能发挥出应有水平,成为本场MVP."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员能力又高,又有战术上的优势,获胜应该是理所当然的.观众对于" + this.pMVP.strName + "的超常表现非常满意."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在战术安排得当的情况下," + this.pMVP.strName + "正常发挥出自己的水平,带领球队获得最后的胜利.理所当然的得到了本场的MVP贵冠."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("由于经理费尽心力做好了优秀的赛前工作,才领导球队战胜强敌,本场的MVP不应该属于" + this.pMVP.strName + ",而应属于尽职的经理!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球队在战术上稍占优势,球员们借此拼力一搏,顽强的获得了胜利," + this.pMVP.strName + "能得到MVP不是他一个人的功劳."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在没有任何优势的情况下," + this.pMVP.strName + "力挽狂澜,创造了本场的一个奇迹!他是本场的天皇巨星!永远的MVP!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddH == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场的胜利完全靠球员们综合实力,既没有战术的优势也没有赛前的鼓舞,观众们对" + this.pMVP.strName + "夺走了MVP表示非常惊讶!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddH == 0)))
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
                if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("有了经理谨慎的赛前工作,在球队的整体配合与战术的合理安排下," + this.pMVP.strName + "发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed(this.pMVP.strName + "在队友的大力支持与战术的合理安排下,发挥出自身卓越的才能,以绝对的优势赢得了本场的MVP."));
                }
                else if (((this.intWinAbility > this.intLoseAbility) && (this.pMVP.intAbility == this.intMaxAbility)) && ((num9 >= num10) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然在战术上没有占得优势,但是由于精心的赛前准备和全队的卓越实力,比赛最终获胜." + this.pMVP.strName + "获得了本场的MVP!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("全队成员在战术的合理安排下赢得了本场的MVP," + this.pMVP.strName + "获得了本场的MVP."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("队员们受到经理勤奋的赛前工作的鼓舞,士气大振!超常发挥!赢得了本场比赛的胜利.作为球队核心的" + this.pMVP.strName + "勇夺MVP桂冠!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场比赛没有过多的赛前工作,完全靠球员自身的强大能力获得胜利.这场比赛成为了" + this.pMVP.strName + "的个人秀."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("尽管赛前部署很周密,但是却没有换得战术的优势,凭借球员自身的能力赢得了胜利." + this.pMVP.strName + "在队友的支持下成为了本场的MVP!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("比赛胜利的关键是经理精心的赛前工作和战术的合理利用,全队打出了120%的实力." + this.pMVP.strName + "成为球队中发挥最好的球员."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("虽然战术设计不占有优势,但是经理细致的赛前工作使得" + this.pMVP.strName + "能发挥出应有水平,成为本场MVP."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球员能力又高,又有战术上的优势,获胜应该是理所当然的.观众对于" + this.pMVP.strName + "的超常表现非常满意."));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在战术安排得当的情况下," + this.pMVP.strName + "正常发挥出自己的水平,带领球队获得最后的胜利.理所当然的得到了本场的MVP贵冠."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA > 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("由于经理费尽心力做好了优秀的赛前工作,才领导球队战胜强敌,本场的MVP不应该属于" + this.pMVP.strName + ",而应属于尽职的经理!"));
                }
                else if (((num9 <= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("球队在战术上稍占优势,球员们借此拼力一搏,顽强的获得了胜利," + this.pMVP.strName + "能得到MVP不是他一个人的功劳."));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility == this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("在没有任何优势的情况下," + this.pMVP.strName + "力挽狂澜,创造了本场的一个奇迹!他是本场的天皇巨星!永远的MVP!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility > this.intLoseAbility) && (this.byAllAddA == 0)))
                {
                    this.sbIntro.Append(Language.HighLightRed("本场的胜利完全靠球员们综合实力,既没有战术的优势也没有赛前的鼓舞,观众们对" + this.pMVP.strName + "夺走了MVP表示非常惊讶!"));
                }
                else if (((num9 >= num10) && (this.pMVP.intAbility < this.intMaxAbility)) && ((this.intWinAbility < this.intLoseAbility) && (this.byAllAddA == 0)))
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
                string str7 = StringItem.FormatDate(DateTime.Now, "yyyyMMdd");
                string path = Path.GetFullPath(@"\BestXBA" + MatchItem.GetMatchPath() + @"\MatchXML\VRep\") + str7 + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str10 = string.Concat(new object[] { path, "Rep", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbRepURL.Append("MatchXML/VRep/");
                this.sbRepURL.Append(str7);
                this.sbRepURL.Append("/Rep");
                this.sbRepURL.Append(this.tHome.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.tAway.intClubID);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intType);
                this.sbRepURL.Append("_");
                this.sbRepURL.Append(this.intTag);
                this.sbRepURL.Append(".xml");
                if (File.Exists(str10))
                {
                    File.Delete(str10);
                }
                using (StreamWriter writer = File.CreateText(str10))
                {
                    writer.Write(this.sbRepXml.ToString());
                }
                path = Path.GetFullPath(@"\BestXBA" + MatchItem.GetMatchPath() + @"\MatchXML\VStas\") + str7 + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                str10 = string.Concat(new object[] { path, "Stas", this.tHome.intClubID, "_", this.tAway.intClubID, "_", this.intType, "_", this.intTag, ".xml" });
                this.sbStasURL.Append("MatchXML/VStas/");
                this.sbStasURL.Append(str7);
                this.sbStasURL.Append("/Stas");
                this.sbStasURL.Append(this.tHome.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.tAway.intClubID);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intType);
                this.sbStasURL.Append("_");
                this.sbStasURL.Append(this.intTag);
                this.sbStasURL.Append(".xml");
                if (File.Exists(str10))
                {
                    File.Delete(str10);
                }
                using (StreamWriter writer2 = File.CreateText(str10))
                {
                    writer2.Write(this.sbStasXml.ToString());
                }
            }
            if (this.intType == 7)
            {
                BTPFriMatchManager.AddMoneyForMatch(this.tHome.intUserID, this.tAway.intUserID, this.tHome.intScore, this.tAway.intScore, this.blnCanAddH, this.blnCanAddA);
            }
            else if (this.intType == 11)
            {
                BTPFriMatchManager.TrainCenterMatchEnd5ByFriMatchID(this.intTag);
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
                        Quarter quarter = new Quarter(12, intQNum, this.tHome, this.tAway, this.aHomes, this.aAways, this.blnPower, this.byAllAddH, this.byAllAddA, this.intType);
                        quarter.blnTTurn = this.blnTTurn;
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

