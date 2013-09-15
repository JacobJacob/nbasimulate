namespace Web.Helper
{
    using System;
    using Web.DBData;

    public class SalaryData
    {
        public int intSalaryC;
        public int intStrong;
        public int intStrongType;
        public string strDefStrong;
        public string strOffStrong;

        public SalaryData(int intClubID)
        {
            int salaryTop;
            int num3;
            string str2;
            string str3;
            int num4;
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(intClubID);
            int clubSalaryByClubID = BTPPlayer5Manager.GetClubSalaryByClubID(intClubID);
            if ((devCodeByClubID != null) && (devCodeByClubID != ""))
            {
                salaryTop = DevCalculator.GetSalaryTop(devCodeByClubID);
            }
            else
            {
                salaryTop = 0xc350;
            }
            int num5 = clubSalaryByClubID - salaryTop;
            if (num5 > 0)
            {
                /*20001*/
                if (num5 < 0x4e21)
                {
                    num3 = 110;
                    num4 = 1;
                }
                /*15000*/
                else if (num5 < 0x3a98)
                {
                    num3 = 0x6d;
                    num4 = 2;
                }
                /*21000*/
                else if (num5 <= 0x5208)
                {
                    num3 = 0x6c;
                    num4 = 3;
                }
                /*25000*/
                else if (num5 <= 0x61a8)
                {
                    num3 = 0x6b;
                    num4 = 4;
                }
                /*29000*/
                else if (num5 <= 0x7148)
                {
                    num3 = 0x6a;
                    num4 = 5;
                }
                /*33000*/
                else if (num5 <= 0x80e8)
                {
                    num3 = 0x69;
                    num4 = 6;
                }
                /*37000*/
                else if (num5 <= 0x9088)
                {
                    num3 = 0x68;
                    num4 = 7;
                }
                /*41000*/
                else if (num5 <= 0xa028)
                {
                    num3 = 0x67;
                    num4 = 8;
                }
                else if (num5 <= 0xafc8)
                {
                    num3 = 0x66;
                    num4 = 9;
                }
                else if (num5 <= 0xbb80)
                {
                    num3 = 0x65;
                    num4 = 10;
                }
                else if (num5 <= 0xc738)
                {
                    num3 = 100;
                    num4 = 11;
                }
                else if (num5 <= 0xcf08)
                {
                    num3 = 0x63;
                    num4 = 12;
                }
                else if (num5 <= 0xdac0)
                {
                    num3 = 0x62;
                    num4 = 13;
                }
                else if (num5 <= 0xe290)
                {
                    num3 = 0x61;
                    num4 = 14;
                }
                else if (num5 <= 0xee48)
                {
                    num3 = 0x60;
                    num4 = 15;
                }
                else if (num5 <= 0xf618)
                {
                    num3 = 0x5f;
                    num4 = 0x10;
                }
                else if (num5 <= 0x101d0)
                {
                    num3 = 0x5e;
                    num4 = 0x11;
                }
                else if (num5 <= 0x109a0)
                {
                    num3 = 0x5d;
                    num4 = 0x12;
                }
                else if (num5 <= 0x11940)
                {
                    num3 = 0x5c;
                    num4 = 0x13;
                }
                else if (num5 <= 0x12110)
                {
                    num3 = 0x5b;
                    num4 = 20;
                }
                else
                {
                    num3 = 90;
                    num4 = 0x15;
                }
                str2 = string.Concat(new object[] { "<font color=\"red\">阵容状态", num3 - 10, "%</font><img id=\"AlertA\" width=\"16\" height=\"16\" src=\"Images/alert.gif\" border=\"0\" alt=\"您的俱乐部超出工资帽", num5, "，<br/>当前阵容状态为", num3 - 10, "%\">" });
                str3 = string.Concat(new object[] { "<font color=\"red\">阵容状态", num3 - 10, "%</font><img id=\"AlertB\" width=\"16\" height=\"16\" src=\"Images/alert.gif\" border=\"0\" alt=\"您的俱乐部超出工资帽", num5, "，<br/>当前阵容状态为", num3 - 10, "%\">" });
            }
            else
            {
                num4 = 1;
                num3 = 110;
                str3 = "<font color=\"green\">阵容状态100%</font>";
                str2 = "<font color=\"green\">阵容状态100% </font>";
            }
            this.strOffStrong = str3;
            this.strDefStrong = str2;
            this.intStrong = num3;
            this.intSalaryC = num5;
            this.intStrongType = num4;
        }

        public SalaryData(int intClubID, long lngPlayerIDC, long lngPlayerIDPF, long lngPlayerIDSF, long lngPlayerIDSG, long lngPlayerIDPG)
        {
            int salaryTop;
            int num3;
            string str2;
            string str3;
            int num4;
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(intClubID);
            int num = BTPPlayer5Manager.GetClubSalaryByPlayerID(lngPlayerIDC, lngPlayerIDPF, lngPlayerIDSF, lngPlayerIDSG, lngPlayerIDPG);
            if ((devCodeByClubID != null) && (devCodeByClubID != ""))
            {
                salaryTop = DevCalculator.GetSalaryTop(devCodeByClubID);
            }
            else
            {
                salaryTop = 0xc350;
            }
            int num5 = num - salaryTop;
            if (num5 > 0)
            {
                if (num5 < 0x4e21)
                {
                    num3 = 110;
                    num4 = 1;
                }
                else if (num5 < 0x3a98)
                {
                    num3 = 0x6d;
                    num4 = 2;
                }
                else if (num5 <= 0x5208)
                {
                    num3 = 0x6c;
                    num4 = 3;
                }
                else if (num5 <= 0x61a8)
                {
                    num3 = 0x6b;
                    num4 = 4;
                }
                else if (num5 <= 0x7148)
                {
                    num3 = 0x6a;
                    num4 = 5;
                }
                else if (num5 <= 0x80e8)
                {
                    num3 = 0x69;
                    num4 = 6;
                }
                else if (num5 <= 0x9088)
                {
                    num3 = 0x68;
                    num4 = 7;
                }
                else if (num5 <= 0xa028)
                {
                    num3 = 0x67;
                    num4 = 8;
                }
                else if (num5 <= 0xafc8)
                {
                    num3 = 0x66;
                    num4 = 9;
                }
                else if (num5 <= 0xbb80)
                {
                    num3 = 0x65;
                    num4 = 10;
                }
                else if (num5 <= 0xc738)
                {
                    num3 = 100;
                    num4 = 11;
                }
                else if (num5 <= 0xcf08)
                {
                    num3 = 0x63;
                    num4 = 12;
                }
                else if (num5 <= 0xdac0)
                {
                    num3 = 0x62;
                    num4 = 13;
                }
                else if (num5 <= 0xe290)
                {
                    num3 = 0x61;
                    num4 = 14;
                }
                else if (num5 <= 0xee48)
                {
                    num3 = 0x60;
                    num4 = 15;
                }
                else if (num5 <= 0xf618)
                {
                    num3 = 0x5f;
                    num4 = 0x10;
                }
                else if (num5 <= 0x101d0)
                {
                    num3 = 0x5e;
                    num4 = 0x11;
                }
                else if (num5 <= 0x109a0)
                {
                    num3 = 0x5d;
                    num4 = 0x12;
                }
                else if (num5 <= 0x11940)
                {
                    num3 = 0x5c;
                    num4 = 0x13;
                }
                else if (num5 <= 0x12110)
                {
                    num3 = 0x5b;
                    num4 = 20;
                }
                else
                {
                    num3 = 90;
                    num4 = 0x15;
                }
                str2 = string.Concat(new object[] { "<font color=\"green\">工资帽<br>", salaryTop, "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">", num3 - 10, "%</font>" });
                str3 = string.Concat(new object[] { "<font color=\"green\">工资帽<br>", salaryTop, "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">", num3 - 10, "%</font>" });
            }
            else
            {
                num4 = 1;
                num3 = 110;
                str3 = "<font color=\"green\">工资帽<br>" + salaryTop + "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">100%</font>";
                str2 = "<font color=\"green\">工资帽<br>" + salaryTop + "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">100%</font>";
            }
            this.strOffStrong = str3;
            this.strDefStrong = str2;
            this.intStrong = num3;
            this.intSalaryC = num5;
            this.intStrongType = num4;
        }

        public string DefStrong
        {
            get
            {
                return this.strDefStrong;
            }
            set
            {
                this.strDefStrong = value;
            }
        }

        public string OffStrong
        {
            get
            {
                return this.strOffStrong;
            }
            set
            {
                this.strOffStrong = value;
            }
        }

        public int SalaryC
        {
            get
            {
                return this.intSalaryC;
            }
            set
            {
                this.intSalaryC = value;
            }
        }

        public int Strong
        {
            get
            {
                return this.intStrong;
            }
            set
            {
                this.intStrong = value;
            }
        }

        public int StrongType
        {
            get
            {
                return this.intStrongType;
            }
            set
            {
                this.intStrongType = value;
            }
        }
    }
}

