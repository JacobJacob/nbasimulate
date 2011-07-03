namespace Web.Helper
{
    using System;
    using System.Data;
    using Web.DBData;

    public class PlayerItem
    {
        public static bool CanChangeName(string strPlayerName)
        {
            string str = "SB,毛泽东";
            Cuter cuter = new Cuter(str);
            for (int i = 0; i < cuter.GetSize(); i++)
            {
                string str2 = cuter.GetCuter(i);
                if (strPlayerName.IndexOf(str2) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static void ChangePlayerFromArrange3(long longPlayerID, int intClubID)
        {
            DataTable arrTableByClubID = BTPArrange3Manager.GetArrTableByClubID(intClubID);
            bool flag = false;
            foreach (DataRow row in arrTableByClubID.Rows)
            {
                int intArrangeID = (int) row["Arrange3ID"];
                long[] longIDs = new long[] { (long) row["CID"], (long) row["FID"], (long) row["GID"] };
                for (int i = 0; i < longIDs.Length; i++)
                {
                    if (longIDs[i] == longPlayerID)
                    {
                        longIDs[i] = BTPPlayer3Manager.GetCanPlay3ID(longIDs, intClubID);
                        flag = true;
                    }
                }
                if (flag)
                {
                    BTPArrange3Manager.ReSetArrange3(intArrangeID, longIDs);
                }
                flag = false;
            }
        }

        public static void ChangePlayerFromArrange5(long longPlayerID, int intClubID)
        {
            DataTable arrTableByClubID = BTPArrange5Manager.GetArrTableByClubID(intClubID);
            bool flag = false;
            foreach (DataRow row in arrTableByClubID.Rows)
            {
                int intArrangeID = (int) row["Arrange5ID"];
                long[] longIDs = new long[] { (long) row["CID"], (long) row["PFID"], (long) row["SFID"], (long) row["SGID"], (long) row["PGID"] };
                for (int i = 0; i < longIDs.Length; i++)
                {
                    if (longIDs[i] == longPlayerID)
                    {
                        longIDs[i] = BTPPlayer5Manager.GetCanPlay5ID(longIDs, intClubID);
                        flag = true;
                    }
                }
                if (flag)
                {
                    BTPArrange5Manager.ReSetArrange5(intArrangeID, longIDs);
                }
                flag = false;
            }
        }

        public static string GetAbilityColor(int intAbility)
        {
            float num = ((float) intAbility) / 10f;
            if (intAbility < 300)
            {
                return ("<font color='#666666'>" + num + "</font>");
            }
            if ((intAbility >= 300) && (intAbility < 500))
            {
                return ("<font color='#333333'>" + num + "</font>");
            }
            if ((intAbility >= 500) && (intAbility < 600))
            {
                return ("<font color='#00d034'>" + num + "</font>");
            }
            if ((intAbility >= 600) && (intAbility < 750))
            {
                return ("<font color='#0070DD'>" + num + "</font>");
            }
            if ((intAbility >= 750) && (intAbility < 900))
            {
                return ("<font color='#A335EE'>" + num + "</font>");
            }
            return ("<font color='#FF8000'>" + num + "</font>");
        }

        public static string GetAwarenessTrainSelect(int intTrainAType, long longPlayerID)
        {
            string[] strArray = new string[] { "进攻意识", "防守意识", "团队意识" };
            string str = string.Concat(new object[] { "<select id='sltTrain", longPlayerID, "' name='sltTrain", longPlayerID, "' onchange='ChangeTrain()'>" });
            for (int i = 12; i < 15; i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "<option value='", i, "'" });
                if (intTrainAType == i)
                {
                    str = str + " selected";
                }
                str = str + ">" + strArray[i - 12] + "</option>";
            }
            return (str + "</select>");
        }

        public static string GetBidLeftTime(DateTime dtNow, DateTime dtBidTime)
        {
            TimeSpan span = dtBidTime.Subtract(dtNow);
            string str = span.ToString();
            if (str.IndexOf("-") == -1)
            {
                try
                {
                    if (span.Days > 0)
                    {
                        str = str.Replace(".", "天");
                        return (str.Substring(0, str.IndexOf(":")) + "时");
                    }
                    if (span.Hours > 0)
                    {
                        string str3 = str.Substring(0, str.IndexOf(":"));
                        string str4 = str.Substring(str.IndexOf(":") + 1, 2);
                        return (str3 + "时" + str4 + "分");
                    }
                    string str5 = str.Substring(str.IndexOf(":") + 1, 2);
                    string str6 = str.Substring(str.LastIndexOf(":") + 1, 2);
                    str = str5 + "分" + str6 + "秒";
                }
                catch (Exception exception)
                {
                    exception.ToString();
                    GetBidLeftTime(dtNow, dtBidTime);
                }
                return str;
            }
            return "已截止";
        }

        public static DataTable GetEndBidTime()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Hours", typeof(int)));
            int hour = DateTime.Now.Hour;
            for (int i = 1; i < 0x19; i += 3)
            {
                if (((((hour + i) + 12) >= 9) && (((hour + i) + 12) < 0x16)) || ((((hour + i) - 12) >= 9) && (((hour + i) - 12) < 0x16)))
                {
                    DataRow row = table.NewRow();
                    row[0] = i + 12;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static string GetEvent()
        {
            string[] strArray = new string[] { "外出度假", "出国旅游", "患病请假", "犯罪被捕", "违禁药被禁赛", "无故缺席比赛", "打架被禁赛" };
            int index = RandomItem.rnd.Next(0, 7);
            return strArray[index];
        }

        public static int GetGoodNum3(int intClubID)
        {
            if (intClubID == 0)
            {
                return 0;
            }
            DataTable table = BTPPlayer3Manager.GetPlayer3NumByClubID(intClubID);
            int num = 0;
            bool flag = false;
        Label_007A:
            while (!flag)
            {
                num = RandomItem.rnd.Next(0, 50);
                foreach (DataRow row in table.Rows)
                {
                    int num2 = (byte) row["Number"];
                    if (num == num2)
                    {
                        flag = false;
                        goto Label_007A;
                    }
                    flag = true;
                }
            }
            return num;
        }

        public static int GetGoodNum5(int intClubID)
        {
            if (intClubID <= 0)
            {
                return 0;
            }
            DataTable table = BTPPlayer5Manager.GetPlayer5NumByClubID(intClubID);
            int num = 0;
            bool flag = false;
            if (table == null)
            {
                return num;
            }
        Label_007E:
            while (!flag)
            {
                num = RandomItem.rnd.Next(0, 50);
                foreach (DataRow row in table.Rows)
                {
                    int num2 = (byte) row["Number"];
                    if (num == num2)
                    {
                        flag = false;
                        goto Label_007E;
                    }
                    flag = true;
                }
            }
            return num;
        }

        public static string GetOrderInfo(string strType, int intPos, int intOrder, string strObjName)
        {
            return string.Concat(new object[] { "<a href='TransferMarket.aspx?Type=", strType, "&Pos=", intPos, "&Order=", intOrder, "&Page=1'>", strObjName, "</a>" });
        }

        public static string GetPlayerChsPosition(int intPosition)
        {
            switch (intPosition)
            {
                case 1:
                    return "中锋";

                case 2:
                    return "大前锋";

                case 3:
                    return "小前锋";

                case 4:
                    return "得分后卫";

                case 5:
                    return "组织后卫";
            }
            return "--";
        }

        public static string GetPlayerChsTrainType(int intTrainType)
        {
            switch (intTrainType)
            {
                case 1:
                    return "速度";

                case 2:
                    return "弹跳";

                case 3:
                    return "强壮";

                case 4:
                    return "耐力";

                case 5:
                    return "投篮";

                case 6:
                    return "三分";

                case 7:
                    return "运球";

                case 8:
                    return "传球";

                case 9:
                    return "篮板";

                case 10:
                    return "抢断";

                case 11:
                    return "封盖";

                case 12:
                    return "进攻";

                case 13:
                    return "防守";

                case 14:
                    return "团队";
            }
            return "--";
        }

        public static string GetPlayerEngPosition(int intPosition)
        {
            switch (intPosition)
            {
                case 1:
                    return "C";

                case 2:
                    return "PF";

                case 3:
                    return "SF";

                case 4:
                    return "SG";

                case 5:
                    return "PG";
            }
            return "--";
        }

        public static string GetPlayerNameInfo(long longPlayerID, string strName, int intType, int intKind, int intCheck)
        {
            return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowPlayer.aspx?Type=", intType, "&Kind=", intKind, "&Check=", intCheck, "&PlayerID=", longPlayerID, "\" target=\"Right\">", strName, "</a></div>" });
        }

        public static string GetPlayerNameInfo(long longPlayerID, string strName, int intType, int intKind, int intCheck, bool blnSellAss)
        {
            if (blnSellAss)
            {
                return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowPlayer.aspx?Type=", intType, "&Kind=", intKind, "&Check=", intCheck, "&PlayerID=", longPlayerID, "\" target=\"Right\"><font color='red'>", strName, "</font></a></div>" });
            }
            return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowPlayer.aspx?Type=", intType, "&Kind=", intKind, "&Check=", intCheck, "&PlayerID=", longPlayerID, "\" target=\"Right\">", strName, "</a></div>" });
        }

        public static string GetPlayerNameInfoNew(long longPlayerID, string strName, int intType, int intKind, int intCheck)
        {
            return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a onclick='<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", intType, "&Kind=", intKind, "&Check=", intCheck, "&PlayerID=", longPlayerID, "\";</script>' >", strName, "</a></div>" });
        }

        public static DataTable GetPlayerNumItem()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Number", typeof(int)));
            for (int i = 0; i <= 50; i++)
            {
                DataRow row = table.NewRow();
                row[0] = i;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable GetPlayerPositionItem()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("PositionID", typeof(string)));
            table.Columns.Add(new DataColumn("PositionName", typeof(string)));
            DataRow row = table.NewRow();
            row = table.NewRow();
            row[0] = "1";
            row[1] = "中锋(C)";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "2";
            row[1] = "大前锋(PF)";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "3";
            row[1] = "小前锋(SF)";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "4";
            row[1] = "得分后卫(SG)";
            table.Rows.Add(row);
            row = table.NewRow();
            row[0] = "5";
            row[1] = "组织后卫(PG)";
            table.Rows.Add(row);
            return table;
        }

        public static string GetPlayerPotentialStar(DataRow dr)
        {
            string str = "";
            int num = (int) dr["Speed"];
            int num2 = (int) dr["Jump"];
            int num3 = (int) dr["Strength"];
            int num4 = (int) dr["Stamina"];
            int num5 = (int) dr["Shot"];
            int num6 = (int) dr["Point3"];
            int num7 = (int) dr["Dribble"];
            int num8 = (int) dr["Pass"];
            int num9 = (int) dr["Rebound"];
            int num10 = (int) dr["Steal"];
            int num11 = (int) dr["Block"];
            int num12 = (int) dr["Attack"];
            int num13 = (int) dr["Defense"];
            int num14 = (int) dr["Team"];
            int num15 = (int) dr["SpeedMax"];
            int num16 = (int) dr["JumpMax"];
            int num17 = (int) dr["StrengthMax"];
            int num18 = (int) dr["StaminaMax"];
            int num19 = (int) dr["ShotMax"];
            int num20 = (int) dr["Point3Max"];
            int num21 = (int) dr["DribbleMax"];
            int num22 = (int) dr["PassMax"];
            int num23 = (int) dr["ReboundMax"];
            int num24 = (int) dr["StealMax"];
            int num25 = (int) dr["BlockMax"];
            int num26 = (int) dr["AttackMax"];
            int num27 = (int) dr["DefenseMax"];
            int num28 = (int) dr["TeamMax"];
            int num29 = (((((((((((((num + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9) + num10) + num11) + num12) + num13) + num14) / 14;
            int num30 = (((((((((((((num15 + num16) + num17) + num18) + num19) + num20) + num21) + num22) + num23) + num24) + num25) + num26) + num27) + num28) / 14;
            int num31 = (num30 - num29) / 10;
            int num32 = 0;
            if (num31 < 2)
            {
                num32 = 1;
            }
            else if ((num31 >= 2) && (num31 < 4))
            {
                num32 = 2;
            }
            else if ((num31 >= 4) && (num31 < 7))
            {
                num32 = 3;
            }
            else if ((num31 >= 7) && (num31 < 10))
            {
                num32 = 4;
            }
            else if ((num31 >= 10) && (num31 < 13))
            {
                num32 = 5;
            }
            else if ((num31 >= 13) && (num31 < 0x11))
            {
                num32 = 6;
            }
            else if ((num31 >= 0x11) && (num31 < 0x15))
            {
                num32 = 7;
            }
            else if ((num31 >= 0x15) && (num31 < 0x19))
            {
                num32 = 8;
            }
            else if ((num31 >= 0x19) && (num31 < 30))
            {
                num32 = 9;
            }
            else
            {
                num32 = 10;
            }
            if (num32 < 1)
            {
                num32 = 1;
            }
            if (num32 > 10)
            {
                num32 = 10;
            }
            object obj2 = str;
            return string.Concat(new object[] { obj2, "<img src='", SessionItem.GetImageURL(), "Player/Potential/Potential_", num32, ".gif' border='0' width='63' height='11' align='absmiddle'>" });
        }

        public static string GetPlayerSay(long longPlayerID, string strPlayerName, int intAge, string strClubName, string strNickName, SecretaryItem si, bool blnSex, int intStatus, bool blnArrange, int intHappy, int intPower)
        {
            string str = "";
            int draw = (int) (longPlayerID % 5L);
            str = str + GetPlayerSayStatus(draw, intStatus, si);
            if (intStatus == 1)
            {
                str = str + GetPlayerSayPower(draw, intPower) + GetPlayerSayHappy(draw, intPower, intHappy, strClubName, blnArrange);
            }
            return str;
        }

        public static string GetPlayerSayHappy(int draw, int intPower, int intHappy, string clubName, bool blnArrange)
        {
            string str = "";
            string str2 = "";
            if (intHappy > 90)
            {
                if (intPower < 60)
                {
                    str = "不过";
                }
                switch (draw)
                {
                    case 0:
                        return (str + "很高兴能在" + clubName + "打球！");

                    case 1:
                        return (str + "我对自己近来的表现很满意。");

                    case 2:
                        return (str + "我喜欢每天的比赛，看我怎么蹂躏对手！");

                    case 3:
                        return (str + "我的球技给我来无穷快乐！");

                    case 4:
                        return (str + "今天心情不错，不哭了。");
                }
                return (str + "我对自己近来的表现很满意。");
            }
            if ((intHappy < 0x4b) && (intHappy >= 60))
            {
                if (intPower > 90)
                {
                    str = "可是，";
                }
                switch (draw)
                {
                    case 0:
                        return (str + "感觉最近在" + clubName + "打球挺郁闷。");

                    case 1:
                        return (str + "以现在的成绩，怎么也高兴不起来！");

                    case 2:
                        return (str + "请多给我点上场机会！");

                    case 3:
                        return (str + "在" + clubName + "打球够郁闷的，每天上场时间就那么少！");

                    case 4:
                        return (str + "以我的表现，你是不是不会和我续签合同了？");
                }
                return (str + "以现在的成绩，怎么也高兴不起来！");
            }
            if (intHappy >= 60)
            {
                return "";
            }
            if (intPower > 60)
            {
                str = "但是";
            }
            if (blnArrange)
            {
                str2 = "在这之前先把我从阵容中替换出来吧。";
            }
            switch (draw)
            {
                case 0:
                    return (str + "最近球打的真郁闷，明天想度假调整一下心情。" + str2);

                case 1:
                    return (str + "心情不好，今晚陪我喝点酒吧？明天想度假休息。" + str2);

                case 2:
                    return (str + "在这支球队打球，让我很沮丧，明天不想来了，休息一下！" + str2);

                case 3:
                    return (str + "明天不来了，出去郊游，傻叉才给你打球呢！" + str2);

                case 4:
                    return (str + "(擦了擦眼泪，哽咽着)明天能不能给我一天假期啊……" + str2);
            }
            return (str + "最近球打的真郁闷，明天想度假调整一下心情。" + str2);
        }

        public static string GetPlayerSayPower(int draw, int intPower)
        {
            if (intPower > 90)
            {
                switch (draw)
                {
                    case 0:
                        return "现在自我感觉体力很充沛。";

                    case 1:
                        return "最近身体状态保持的很不错。";

                    case 2:
                        return "最近感觉浑身是劲！";

                    case 3:
                        return "以我现在的体力，打完48分钟还能跑个万米！";

                    case 4:
                        return "现在感觉能跑能跳的，不知道投篮状态怎么样。";
                }
                return "现在自我感觉体力很充沛。";
            }
            if ((intPower < 80) && (intPower >= 60))
            {
                switch (draw)
                {
                    case 0:
                        return "感觉最近比赛挺疲劳的。";

                    case 1:
                        return "今天胳膊腿都酸疼酸疼的。";

                    case 2:
                        return "累了，我还是希望先发。";

                    case 3:
                        return "有点疲劳；看来比赛中没我不行！";

                    case 4:
                        return "累了，我看再不休息，我就爬不起来了。";
                }
                return "感觉最近比赛挺疲劳的。";
            }
            if (intPower >= 60)
            {
                return "";
            }
            switch (draw)
            {
                case 0:
                    return "明天别派我上场了，累死了。";

                case 1:
                    return "胳膊腿都快掉下来了。";

                case 2:
                    return "累死我了！";

                case 3:
                    return "经理，想练死我？下场我要休息！";

                case 4:
                    return "(哭)呜呜呜。。。明天再打，我看就要受伤了！";
            }
            return "明天别派我上场了，累死了。";
        }

        public static string GetPlayerSayStatus(int draw, int intStatus, SecretaryItem si)
        {
            if (intStatus == 2)
            {
                switch (draw)
                {
                    case 0:
                        return "受伤期间不能为球队效力，真是很遗憾！";

                    case 1:
                        return "最近感觉身体恢复很快，希望马上我就可以归队了。";

                    case 2:
                        return "等我好起来，打几个三双给你瞧瞧！";

                    case 3:
                        return "我不在，球队赢个比赛都很困难啦！";

                    case 4:
                        return "(哭)我看我是好不了了，好了也难恢复以前的状态了！";
                }
                return "受伤的日子真是难熬呀！";
            }
            if (intStatus != 3)
            {
                return "";
            }
            switch (draw)
            {
                case 0:
                    return (si.strName + "报告：今天没有到球队报道！");

                case 1:
                    return (si.strName + "报告：不知道他去哪了！");

                case 2:
                    return (si.strName + "报告：没和我说一声就走了。");

                case 3:
                    return (si.strName + "报告：他离开的时候，我问他去哪，他都没理我！");

                case 4:
                    return (si.strName + "报告：那天看他眼圈红红的离开球场后，就再也没见他。");
            }
            return (si.strName + "报告：今天没有到球队报道！");
        }

        public static string GetPlayerStatus(int intStatus, string strEvent, int intSuspend)
        {
            string str2;
            if ((intStatus > 1) && (intSuspend > 0))
            {
                if (intStatus == 2)
                {
                    str2 = "需" + intSuspend + "轮恢复";
                }
                else
                {
                    str2 = "停赛" + intSuspend + "轮";
                }
            }
            else
            {
                str2 = "";
            }
            switch (intStatus)
            {
                case 1:
                    return ("<img src=\"" + SessionItem.GetImageURL() + "Player/Event/1.gif\" alt=\"正常\">");

                case 2:
                    return ("<img src=\"" + SessionItem.GetImageURL() + "Player/Event/2.gif\" alt=\"受伤: " + strEvent + "，" + str2 + "\">");

                case 3:
                    return ("<img src=\"" + SessionItem.GetImageURL() + "Player/Event/3.gif\" alt=\"事件: " + strEvent + "，" + str2 + "\">");
            }
            return "事件";
        }

        public static int GetPlayerValue(int intPV, int intAbility, int intHeight)
        {
            int[,] numArray = new int[,] { { 10, 10, 10, 10, 10, 10, 10, 10, 11, 12, 13 }, { 10, 10, 10, 10, 10, 10, 10, 11, 12, 13, 14 }, { 10, 10, 10, 10, 10, 10, 11, 12, 13, 15, 0x11 }, { 10, 10, 11, 12, 13, 14, 15, 0x10, 0x12, 20, 0x19 }, { 11, 12, 13, 14, 0x10, 0x12, 20, 0x19, 30, 0x23, 70 } };
            if (intAbility < 300)
            {
                intAbility = 0;
            }
            else if ((intAbility >= 300) && (intAbility < 400))
            {
                intAbility = 1;
            }
            else if ((intAbility >= 400) && (intAbility < 500))
            {
                intAbility = 2;
            }
            else if ((intAbility >= 500) && (intAbility < 600))
            {
                intAbility = 3;
            }
            else
            {
                intAbility = 4;
            }
            if (intHeight < 170)
            {
                intHeight = 0;
            }
            else if ((intHeight >= 170) && (intHeight < 180))
            {
                intHeight = 1;
            }
            else if ((intHeight >= 180) && (intHeight < 190))
            {
                intHeight = 2;
            }
            else if ((intHeight >= 190) && (intHeight < 200))
            {
                intHeight = 3;
            }
            else if ((intHeight >= 200) && (intHeight < 0xcd))
            {
                intHeight = 4;
            }
            else if ((intHeight >= 0xcd) && (intHeight < 210))
            {
                intHeight = 5;
            }
            else if ((intHeight >= 210) && (intHeight < 0xd5))
            {
                intHeight = 6;
            }
            else if ((intHeight >= 0xd5) && (intHeight < 0xd7))
            {
                intHeight = 7;
            }
            else if ((intHeight >= 0xd7) && (intHeight < 0xd8))
            {
                intHeight = 8;
            }
            else if ((intHeight >= 0xd8) && (intHeight < 0xd9))
            {
                intHeight = 9;
            }
            else
            {
                intHeight = 10;
            }
            return (intPV * numArray[intAbility, intHeight]);
        }

        public static string GetPowerColor(int intPower)
        {
            if (intPower >= 90)
            {
                return ("<font color='#00d034'>" + intPower + "</font>");
            }
            if ((intPower < 90) && (intPower >= 70))
            {
                return ("<font color='#9eba00'>" + intPower + "</font>");
            }
            if ((intPower < 70) && (intPower >= 50))
            {
                return ("<font color='#d0ad00'>" + intPower + "</font>");
            }
            return ("<font color='#d31300'>" + intPower + "</font>");
        }

        public static string GetSRepPosName(int intPos)
        {
            if (intPos == 1)
            {
                return "C";
            }
            if (intPos == 2)
            {
                return "F";
            }
            return "G";
        }

        public static string GetStaffChsPosition(int intPosition)
        {
            switch (intPosition)
            {
                case 1:
                    return "训练员";

                case 2:
                    return "营养师";

                case 3:
                    return "队医";
            }
            return "--";
        }

        public static string GetTrainSelect(int intTrainType, long longPlayerID, int intType)
        {
            string[] strArray = new string[] { "速度", "弹跳", "强壮", "耐力", "投篮", "三分", "运球", "传球", "篮板", "抢断", "封盖" };
            string str = string.Concat(new object[] { "<select id='sltTrain", longPlayerID, "' name='sltTrain", longPlayerID, "'>" });
            for (int i = 0; i < strArray.Length; i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "<option value='", i + 1, "'" });
                if (intTrainType == (i + 1))
                {
                    str = str + " selected";
                }
                str = str + ">" + strArray[i] + "</option>";
            }
            return (str + "</select>");
        }

        public static bool IsValidNumber(DataTable dt, int intNumber)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (intNumber == ((byte) row["Number"]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidNumber(int intCategory, int intClubID, long longPlayerID, int intNumber)
        {
            DataTable playerTableByClubID;
            if (intCategory == 3)
            {
                playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(intClubID);
            }
            else
            {
                playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(intClubID);
            }
            foreach (DataRow row in playerTableByClubID.Rows)
            {
                if ((intNumber == ((byte) row["Number"])) && (longPlayerID != ((long) row["PlayerID"])))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

