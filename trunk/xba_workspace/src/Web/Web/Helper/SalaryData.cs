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
            int num2;
            string str;
            string str2;
            int num3;
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
                if (num5 < 0x4e21)
                {
                    num2 = 110;
                    num3 = 1;
                }
                else if (num5 < 0x3a98)
                {
                    num2 = 0x6d;
                    num3 = 2;
                }
                else if (num5 <= 0x5208)
                {
                    num2 = 0x6c;
                    num3 = 3;
                }
                else if (num5 <= 0x61a8)
                {
                    num2 = 0x6b;
                    num3 = 4;
                }
                else if (num5 <= 0x7148)
                {
                    num2 = 0x6a;
                    num3 = 5;
                }
                else if (num5 <= 0x80e8)
                {
                    num2 = 0x69;
                    num3 = 6;
                }
                else if (num5 <= 0x9088)
                {
                    num2 = 0x68;
                    num3 = 7;
                }
                else if (num5 <= 0xa028)
                {
                    num2 = 0x67;
                    num3 = 8;
                }
                else if (num5 <= 0xafc8)
                {
                    num2 = 0x66;
                    num3 = 9;
                }
                else if (num5 <= 0xbb80)
                {
                    num2 = 0x65;
                    num3 = 10;
                }
                else if (num5 <= 0xc738)
                {
                    num2 = 100;
                    num3 = 11;
                }
                else if (num5 <= 0xcf08)
                {
                    num2 = 0x63;
                    num3 = 12;
                }
                else if (num5 <= 0xdac0)
                {
                    num2 = 0x62;
                    num3 = 13;
                }
                else if (num5 <= 0xe290)
                {
                    num2 = 0x61;
                    num3 = 14;
                }
                else if (num5 <= 0xee48)
                {
                    num2 = 0x60;
                    num3 = 15;
                }
                else if (num5 <= 0xf618)
                {
                    num2 = 0x5f;
                    num3 = 0x10;
                }
                else if (num5 <= 0x101d0)
                {
                    num2 = 0x5e;
                    num3 = 0x11;
                }
                else if (num5 <= 0x109a0)
                {
                    num2 = 0x5d;
                    num3 = 0x12;
                }
                else if (num5 <= 0x11940)
                {
                    num2 = 0x5c;
                    num3 = 0x13;
                }
                else if (num5 <= 0x12110)
                {
                    num2 = 0x5b;
                    num3 = 20;
                }
                else
                {
                    num2 = 90;
                    num3 = 0x15;
                }
                str = string.Concat(new object[] { "<font color=\"red\">阵容状态", num2 - 10, "%</font><img id=\"AlertA\" width=\"16\" height=\"16\" src=\"Images/alert.gif\" border=\"0\" alt=\"您的俱乐部超出工资帽", num5, "，<br/>当前阵容状态为", num2 - 10, "%\">" });
                str2 = string.Concat(new object[] { "<font color=\"red\">阵容状态", num2 - 10, "%</font><img id=\"AlertB\" width=\"16\" height=\"16\" src=\"Images/alert.gif\" border=\"0\" alt=\"您的俱乐部超出工资帽", num5, "，<br/>当前阵容状态为", num2 - 10, "%\">" });
            }
            else
            {
                num3 = 1;
                num2 = 110;
                str2 = "<font color=\"green\">阵容状态100%</font>";
                str = "<font color=\"green\">阵容状态100% </font>";
            }
            this.strOffStrong = str2;
            this.strDefStrong = str;
            this.intStrong = num2;
            this.intSalaryC = num5;
            this.intStrongType = num3;
        }

        public SalaryData(int intClubID, long lngPlayerIDC, long lngPlayerIDPF, long lngPlayerIDSF, long lngPlayerIDSG, long lngPlayerIDPG)
        {
            int salaryTop;
            int num2;
            string str;
            string str2;
            int num3;
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(intClubID);
            int num4 = BTPPlayer5Manager.GetClubSalaryByPlayerID(lngPlayerIDC, lngPlayerIDPF, lngPlayerIDSF, lngPlayerIDSG, lngPlayerIDPG);
            if ((devCodeByClubID != null) && (devCodeByClubID != ""))
            {
                salaryTop = DevCalculator.GetSalaryTop(devCodeByClubID);
            }
            else
            {
                salaryTop = 0xc350;
            }
            int num5 = num4 - salaryTop;
            if (num5 > 0)
            {
                if (num5 < 0x4e21)
                {
                    num2 = 110;
                    num3 = 1;
                }
                else if (num5 < 0x3a98)
                {
                    num2 = 0x6d;
                    num3 = 2;
                }
                else if (num5 <= 0x5208)
                {
                    num2 = 0x6c;
                    num3 = 3;
                }
                else if (num5 <= 0x61a8)
                {
                    num2 = 0x6b;
                    num3 = 4;
                }
                else if (num5 <= 0x7148)
                {
                    num2 = 0x6a;
                    num3 = 5;
                }
                else if (num5 <= 0x80e8)
                {
                    num2 = 0x69;
                    num3 = 6;
                }
                else if (num5 <= 0x9088)
                {
                    num2 = 0x68;
                    num3 = 7;
                }
                else if (num5 <= 0xa028)
                {
                    num2 = 0x67;
                    num3 = 8;
                }
                else if (num5 <= 0xafc8)
                {
                    num2 = 0x66;
                    num3 = 9;
                }
                else if (num5 <= 0xbb80)
                {
                    num2 = 0x65;
                    num3 = 10;
                }
                else if (num5 <= 0xc738)
                {
                    num2 = 100;
                    num3 = 11;
                }
                else if (num5 <= 0xcf08)
                {
                    num2 = 0x63;
                    num3 = 12;
                }
                else if (num5 <= 0xdac0)
                {
                    num2 = 0x62;
                    num3 = 13;
                }
                else if (num5 <= 0xe290)
                {
                    num2 = 0x61;
                    num3 = 14;
                }
                else if (num5 <= 0xee48)
                {
                    num2 = 0x60;
                    num3 = 15;
                }
                else if (num5 <= 0xf618)
                {
                    num2 = 0x5f;
                    num3 = 0x10;
                }
                else if (num5 <= 0x101d0)
                {
                    num2 = 0x5e;
                    num3 = 0x11;
                }
                else if (num5 <= 0x109a0)
                {
                    num2 = 0x5d;
                    num3 = 0x12;
                }
                else if (num5 <= 0x11940)
                {
                    num2 = 0x5c;
                    num3 = 0x13;
                }
                else if (num5 <= 0x12110)
                {
                    num2 = 0x5b;
                    num3 = 20;
                }
                else
                {
                    num2 = 90;
                    num3 = 0x15;
                }
                str = string.Concat(new object[] { "<font color=\"green\">工资帽<br>", salaryTop, "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">", num2 - 10, "%</font>" });
                str2 = string.Concat(new object[] { "<font color=\"green\">工资帽<br>", salaryTop, "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">", num2 - 10, "%</font>" });
            }
            else
            {
                num3 = 1;
                num2 = 110;
                str2 = "<font color=\"green\">工资帽<br>" + salaryTop + "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">100%</font>";
                str = "<font color=\"green\">工资帽<br>" + salaryTop + "<br>当前阵容状态<br></font><font color=\"red\" style=\"font-size:18px\">100%</font>";
            }
            this.strOffStrong = str2;
            this.strDefStrong = str;
            this.intStrong = num2;
            this.intSalaryC = num5;
            this.intStrongType = num3;
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

