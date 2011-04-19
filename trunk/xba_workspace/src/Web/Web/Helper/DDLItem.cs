namespace Web.Helper
{
    using LoginParameter;
    using System;
    using System.Data;
    using Web;
    using Web.DBData;

    public class DDLItem
    {
        public static DataTable GetAddPointItem(bool blnIsMaster, bool blnIsMainMaster)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Category", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            for (int i = 2; i < 6; i++)
            {
                DataRow row = table.NewRow();
                row[0] = i;
                row[1] = i + "";
                table.Rows.Add(row);
            }
            if (blnIsMaster || blnIsMainMaster)
            {
                for (int j = 1; j < 6; j++)
                {
                    DataRow row2 = table.NewRow();
                    row2[0] = j * 10;
                    row2[1] = (j * 10) + "";
                    table.Rows.Add(row2);
                }
            }
            return table;
        }

        public static string[] GetClothes()
        {
            return new string[] { 
                "10000", "10001", "10002", "10003", "10004", "10005", "10006", "10007", "10008", "10009", "10010", "10011", "10012", "10013", "10014", "10015", 
                "10016", "10017", "10018", "10019"
             };
        }

        public static string[] GetClubLogo(int intLevel)
        {
            if (intLevel != 1)
            {
                if (intLevel == 2)
                {
                    return new string[] { "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010" };
                }
                if (intLevel == 3)
                {
                    return new string[] { "3001", "3002", "3003", "3004", "3005", "3006", "3007", "3008", "3009", "3010" };
                }
                if (intLevel == 4)
                {
                    return new string[] { "4001", "4002", "4003", "4004", "4005", "4006", "4007", "4008", "4009", "4010" };
                }
                if (intLevel == 5)
                {
                    return new string[] { "5001", "5002", "5003", "5004", "5005", "5006", "5007", "5008", "5009", "5010" };
                }
                if (intLevel == 6)
                {
                    return new string[] { "6001", "6002", "6003", "6004", "6005", "6006", "6007", "6008", "6009", "6010" };
                }
                if (intLevel == 7)
                {
                    return new string[] { "7001", "7002", "7003", "7004", "7005", "7006", "7007", "7008", "7009", "7010" };
                }
                if (intLevel == 8)
                {
                    return new string[] { "8001", "8002", "8003", "8004", "8005", "8006", "8007", "8008", "8009", "8010" };
                }
                if (intLevel == 9)
                {
                    return new string[] { "9001", "9002", "9003", "9004", "9005", "9006", "9007", "9008", "9009", "9010" };
                }
            }
            return new string[] { "1001", "1002", "1003", "1004", "1005", "1006", "1007", "1008", "1009", "1010" };
        }

        public static DataTable GetDevLevelItem()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Category", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            int num = (int) BTPGameManager.GetGameRow()["DevLevelSum"];
            for (int i = 0; i <= num; i++)
            {
                string str;
                if (i == 0)
                {
                    str = "不限";
                }
                else
                {
                    str = "第" + i + "级联赛以下";
                }
                DataRow row = table.NewRow();
                row[0] = i;
                row[1] = str;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable GetFriMatchItem(int intType, int intCategory)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Category", typeof(string)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            DataRow row = table.NewRow();
            row[0] = "1";
            row[1] = "友谊赛";
            table.Rows.Add(row);
            if (intType == 1)
            {
                row = table.NewRow();
                row[0] = "2";
                row[1] = "训练赛";
                table.Rows.Add(row);
            }
            else if (intType == 2)
            {
                row = table.NewRow();
                row[0] = "7";
                row[1] = "表演赛";
                table.Rows.Add(row);
            }
            if ((bool) Global.drParameter["CanBet"])
            {
                row = table.NewRow();
                row[0] = "3";
                row[1] = "挑战赛";
                table.Rows.Add(row);
                row = table.NewRow();
                row[0] = "4";
                row[1] = "让分赛";
                table.Rows.Add(row);
            }
            return table;
        }

        public static int[] GetMonth()
        {
            return new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        }

        public static DataTable GetPlayerBodyItem()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            DataRow row = table.NewRow();
            row[0] = "11000";
            row[1] = "背景";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "12000";
            row[1] = "身材";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "13000";
            row[1] = "眼睛";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "14000";
            row[1] = "眉毛";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "15000";
            row[1] = "胡须";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "16000";
            row[1] = "鼻子";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "17000";
            row[1] = "眼镜";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "18000";
            row[1] = "头发";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "19000";
            row[1] = "发带";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "22000";
            row[1] = "球袜";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "23000";
            row[1] = "球鞋";
            table.Rows.Add(row);
            return table;
        }

        public static string[] GetProvince()
        {
            return new string[] { 
                "北京市", "上海市", "天津市", "重庆市", "河北省", "山西省", "内蒙古", "辽宁省", "吉林省", "黑龙江", "江苏省", "浙江省", "安徽省", "福建省", "江西省", "山东省", 
                "河南省", "湖北省", "湖南省", "广东省", "广西", "海南省", "四川省", "贵州省", "云南省", "西藏", "陕西省", "甘肃省", "青海省", "宁夏", "新疆", "台湾省", 
                "香港", "澳门", "其他"
             };
        }

        public static DataTable GetServerItem(int intUserID)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Category", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            DataTable userGameTableByUserID = ROOTUserGameManager.GetUserGameTableByUserID(intUserID);
            if (userGameTableByUserID != null)
            {
                foreach (DataRow row in userGameTableByUserID.Rows)
                {
                    int intCategory = (int) row["Category"];
                    string str = DBLogin.GameNameChinese(intCategory);
                    DataRow row2 = table.NewRow();
                    row2[0] = intCategory;
                    row2[1] = str;
                    table.Rows.Add(row2);
                }
            }
            return table;
        }

        public static DataTable GetUserFaceItem()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("ID", typeof(string)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            DataRow row = table.NewRow();
            row[0] = "0";
            row[1] = "背景";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "1";
            row[1] = "脸型";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "2";
            row[1] = "嘴巴";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "3";
            row[1] = "鼻子";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "4";
            row[1] = "眼睛";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "5";
            row[1] = "眉毛";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "6";
            row[1] = "眼镜";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "7";
            row[1] = "头发";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "8";
            row[1] = "发带";
            table.Rows.Add(row);
            return table;
        }

        public static int[] GetYear()
        {
            return new int[] { 
                0x79e, 0x79f, 0x7a0, 0x7a1, 0x7a2, 0x7a3, 0x7a4, 0x7a5, 0x7a6, 0x7a7, 0x7a8, 0x7a9, 0x7aa, 0x7ab, 0x7ac, 0x7ad, 
                0x7ae, 0x7af, 0x7b0, 0x7b1, 0x7b2, 0x7b3, 0x7b4, 0x7b5, 0x7b6, 0x7b7, 0x7b8, 0x7b9, 0x7ba, 0x7bb, 0x7bc, 0x7bd, 
                0x7be, 0x7bf, 0x7c0, 0x7c1, 0x7c2, 0x7c3, 0x7c4, 0x7c5, 0x7c6, 0x7c7, 0x7c8, 0x7c9, 0x7ca, 0x7cb, 0x7cc, 0x7cd, 
                0x7ce, 0x7cf
             };
        }
    }
}

