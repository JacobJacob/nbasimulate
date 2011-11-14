namespace Web
{
    using AjaxPro;
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Main : Page
    {
        public bool blnSex;
        private int intClubID;
        private int intMenuCategory;
        private int intMenuCategory1;
        private int intMenuCategoryU = 1;
        private int intMsgFlag;
        private int intPayType;
        public int intUserID;
        private SecretaryItem si;
        public string strAnnounce;
        public string strBtnClubBuild;
        public string strBtnMap3;
        public string strBtnMap5;
        public string strBtnVArrange;
        public string strFace;
        public string strGetInfoList;
        public string strHasUnionField = "";
        public string strMainScript;
        public string strMap3;
        public string strMap5;
        public string strMenuList;
        public string strMsg;
        public string strNickName;
        public string strSecFace;
        public string strServerName;
        public string strServerTime;
        public string strSetting;

        public string GetChildMenu(string strURL, string strText, int intCategory)
        {
            if (intCategory == 1)
            {
                return ("<tr><td height='18' align='center'><a href='" + strURL + "' target='Main'>" + strText + "</a></td></tr>");
            }
            return ("<tr><td height='18' align='center'><font color='#666666'>" + strText + "</font></td></tr>");
        }

        public string GetChildMenu(string strURL, string strText, int intCategory, string strAlt)
        {
            if (intCategory == 1)
            {
                return ("<tr><td height='18' align='center'><a href='" + strURL + "' target='Main' " + strAlt + ">" + strText + "</a></td></tr>");
            }
            return ("<tr><td height='18' align='center'><span style='color:#666666' " + strAlt + ">" + strText + "</span></td></tr>");
        }

        public string GetFieldMenu(string strURL, string strText, int intCategory)
        {
            if (intCategory == 1)
            {
                return ("<tr><td height='18' align='center'><a href='" + strURL + "' target='Main'>" + strText + "</a> <span id='HasField2'></span></td></tr>");
            }
            return ("<tr><td height='18' align='center'><font color='#666666'>" + strText + "</font></td></tr>");
        }

        public string GetInfoList()
        {
            string str7;
            int num11;
            string str = "";
            string dev = "";
            string str6 = "0,0,0,0,0,0,0";
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                return str;
            }
            str6 = accountRowByUserID["AdvanceOp"].ToString().Trim();
            this.Session["Advance" + this.intUserID] = str6;
            string str2 = accountRowByUserID["ClubName"].ToString().Trim();
            DateTime time1 = (DateTime) accountRowByUserID["CreateTime"];
            int num6 = (int) accountRowByUserID["UnionID"];
            string str3 = accountRowByUserID["ShortName"].ToString().Trim();
            if (num6 > 0)
            {
                str3 = str3 + "-";
            }
            else
            {
                str3 = "";
            }
            accountRowByUserID["LogoLink"].ToString();
            str2 = StringItem.GetShortName(str3 + str2, 15, ".");
            string str4 = accountRowByUserID["ClubLogo"].ToString().Trim();
            long num = (long) accountRowByUserID["OnlyPoint"];
            long onlyUnionPoint = (long)accountRowByUserID["OnlyUnionPoint"];
            num = num + onlyUnionPoint;
            int num4 = (int) accountRowByUserID["Score"];
            long num2 = (long) accountRowByUserID["Money"];
            int num3 = (byte) accountRowByUserID["Levels"];
            int num5 = (byte) accountRowByUserID["Category"];
            long num7 = (long) accountRowByUserID["DevIncome"];
            DataRow gameRow = BTPGameManager.GetGameRow();
            int num8 = (int) gameRow["Season"];
            int num9 = (int) gameRow["Turn"];
            int num10 = (int) gameRow["Days"];
            switch (num9)
            {
                case 0x1b:
                    str7 = "第 " + num8 + " 赛季结束";
                    break;

                case 0:
                    str7 = "第 " + num8 + " 赛季";
                    break;

                default:
                    if ((num9 == 14) && (num10 == 15))
                    {
                        str7 = "第 " + num8 + " 赛季  休赛日";
                    }
                    else
                    {
                        int round = num9;
                        if (round > 1)
                        {
                            round = round - 1;
                        }
                        str7 = string.Concat(new object[] { "第 ", num8, " 赛季  第 ", round, " 轮" });
                    }
                    break;
            }
            string str8 = "";
            if (num5 == 5)
            {
                dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(this.intUserID));
                str8 = string.Concat(new object[] { "<a target='Main' href=\"Main_P.aspx?Tag=", this.intUserID, "&Type=DEVISION\">", dev, "</a>" });
            }
            else
            {
                dev = "无";
                str8 = dev;
            }
            switch (num3)
            {
                case 1:
                    num11 = 5;
                    break;

                case 2:
                    num11 = 15;
                    break;

                case 3:
                    num11 = 0x19;
                    break;

                case 4:
                    num11 = 40;
                    break;

                case 5:
                    num11 = 0x37;
                    break;

                case 6:
                    num11 = 70;
                    break;

                case 7:
                    num11 = 90;
                    break;

                case 8:
                    num11 = 110;
                    break;

                case 9:
                    num11 = 0x87;
                    break;

                case 10:
                    num11 = 160;
                    break;

                case 11:
                    num11 = 190;
                    break;

                case 12:
                    num11 = 220;
                    break;

                case 13:
                    num11 = 250;
                    break;

                case 14:
                    num11 = 290;
                    break;

                case 15:
                    num11 = 330;
                    break;

                case 0x10:
                    num11 = 400;
                    break;

                case 0x11:
                    num11 = 500;
                    break;

                case 0x12:
                    num11 = 600;
                    break;

                case 0x13:
                    num11 = 800;
                    break;

                case 20:
                    num11 = 0x3e8;
                    break;

                case 0x15:
                    num11 = 0x5dc;
                    break;

                case 0x16:
                    num11 = 0x7d0;
                    break;

                case 0x17:
                    num11 = 0xbb8;
                    break;

                case 0x18:
                    num11 = 0xfa0;
                    break;

                case 0x19:
                    num11 = 0x1770;
                    break;

                case 0x1a:
                    num11 = 0x1f40;
                    break;

                case 0x1b:
                    num11 = 0x2710;
                    break;

                default:
                    num11 = 5;
                    break;
            }
            string str9 = "<img id=imgLogo border=0 height=46 src='" + str4 + "'width=46>";
            string kFURL = StringItem.GetKFURL();
            bool flag = true;
            if (((num9 == 1) || ((num9 == 14) && (num10 == 15))) || (num9 > 0x1b))
            {
                flag = false;
            }
            if ((DateTime.Now.Hour < 10) && flag)
            {
                num2 -= num7;
            }
            string str11 = string.Concat(new object[] { "<table width='150' border='0' cellspacing='0' cellpadding='0'><tr><td width=\"38\"><strong>资金</strong>：</td><td width=\"112\" valign=\"middle\"><div id=\"divMoney\" style=\"CURSOR:hand\"><a target='Main' href=\"Main_S.aspx?Tag=", this.intUserID, "&Type=FINANCE\">", num2, "</a>&nbsp;&nbsp;<span  title=\"点击更新资金\" onclick=\"RefreshMoney();\">更新</span></div></td></tr></table>" });
            str = string.Concat(new object[] { "<table width='763' style='padding:4px;' border='0' cellspacing='0' cellpadding='0'><tr><td valign='middle' align='center' rowspan='2' width='170' style='line-height:150%'><font style='font-size:14px'><strong>", str7, "</strong></font><br><strong>职业联赛</strong>：", str8, "</td><td rowspan='2' width='55'><a href='Main_P.aspx?Tag=", this.intUserID, "&Type=MODIFYCLUB' target='Main'>", str9, "</a></td><td width='150' height='25' valign='baseline'><strong>球队</strong>：", str2, "</td><td width='168' valign='baseline'><strong>王者积分</strong>：", num, "</td><td width='150' valign='baseline'><strong>客服</strong>：", kFURL, "</td><td width='70' align='right'><a href='" });
            //str = string.Concat(new object[] { "<table width='763' style='padding:4px;' border='0' cellspacing='0' cellpadding='0'><tr><td valign='middle' align='center' rowspan='2' width='170' style='line-height:150%'><font style='font-size:14px'><strong>", str7, "</strong></font><br><strong>职业联赛</strong>：", str8, "</td><td rowspan='2' width='55'><a href='Main_P.aspx?Tag=", this.intUserID, "&Type=MODIFYCLUB' target='Main'>", str9, "</a></td><td width='150' height='25' valign='baseline'><strong>球队</strong>：", str2, "</td><td width='168' valign='baseline'><strong>王者积分</strong>：", num, "</td><td width='150' valign='baseline'>&nbsp;</td><td width='70' align='right'>&nbsp;" });
            /*if (ServerParameter.strCopartner == "CGA")
            {
                str = str + "http://xba.cga.com.cn/service.html";
            }
            else if (ServerParameter.strCopartner == "ZHW")
            {
                str = str + "http://xba.game.china.com/page/service.html";
            }
            else if (ServerParameter.strCopartner == "51WAN")
            {
                str = str + "http://www.51wan.com/xba/200801/20080124181751.shtml";
            }
            else if (ServerParameter.strCopartner == "17173")
            {
                str = str + "http://web.17173.com/xba/xinren/xinren3.html";
            }
            else if (ServerParameter.strCopartner == "DUNIU")
            {
                str = str + "http://cn.mmoabc.com/c_44.htm";
            }
            else if (ServerParameter.strCopartner == "DW")
            {
                str = str + "http://xba.duowan.com/0803/70460489439.html";
            }
            else
            {
                str = str + "http://www.xba.com.cn/page/service.html";
            }*/

            str += "http://bbs.113388.net/forum.php?mod=viewthread&tid=324&fromuid=1";

            object obj2 = str;
            //str = string.Concat(new object[] { obj2, "' target='_blank'><img border=0 src='Images/xinshou.gif'  height='21px' width='60' /></a></td></tr><tr><td height='25' valign='baseline'>", str11, "</td><td valign='baseline'><strong>街球等级</strong>：", num3, "&nbsp;[", num4, "/", num11, "]</td><td valign='baseline'><a href='javascript:;'onclick=window.open('ShowClubIFrom.aspx?UserID=-99','XBA','height=392,width=224,status=no,toolbar=no,menubar=no,location=no');><font color='red'>在线聊天</font></a>" });
            str = string.Concat(new object[] { obj2, "' target='_blank'><img border=0 src='Images/xinshou.gif'  height='21px' width='60' /></a></td></tr><tr><td height='25' valign='baseline'>", str11, "</td><td valign='baseline'><strong>街球等级</strong>：", num3, "&nbsp;[", num4, "/", num11, "]</td><td valign='baseline'><strong>官方群</strong>:4269274" });
            //str = string.Concat(new object[] { obj2, "</td></tr><tr><td height='25' valign='baseline'>", str11, "</td><td valign='baseline'><strong>街球等级</strong>：", num3, "&nbsp;[", num4, "/", num11, "]</td><td valign='baseline'>&nbsp;</a>" });
            if (ServerParameter.strCopartner == "CGA")
            {
                str = str + " <a style='cursor:hand;' href=\"http://bbs.cga.com.cn/list/list_285.asp?bid=285\" target=\"_blank\">论坛</a>";
            }
            else if (ServerParameter.strCopartner == "ZHW")
            {
                str = str + "";
            }
            else
            {
                str = str + " ";
            }
            //return (str + " <a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\">经理手册</a></td><td align='right'><a href='" + StringItem.GetBuyCoinURL() + "' target='_blank'><img border=0 src='Images/chongzhi.gif'  height='21px' width='60' /></a></td></tr></table>");
            return (str + "</td><td align='right'><a href='" + StringItem.GetBuyCoinURL() + "' target='_blank'>购买游戏币</a></td></tr></table>");
            //return (str + " &nbsp;</td><td align='right'>&nbsp;</td></tr></table>");
        }

        [AjaxMethod]
        public string GetMoney()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(SessionItem.CheckLogin(0));
            long num = (long) accountRowByUserID["Money"];
            long num2 = (long) accountRowByUserID["DevIncome"];
            DataRow gameRow = BTPGameManager.GetGameRow();
            int num3 = (int) gameRow["Turn"];
            int num4 = (int) gameRow["Days"];
            bool flag = true;
            if (((num3 == 1) || ((num3 == 14) && (num4 == 15))) || (num3 > 0x1b))
            {
                flag = false;
            }
            if ((DateTime.Now.Hour < 10) && flag)
            {
                num -= num2;
            }
            return num.ToString().Trim();
        }

        [AjaxMethod]
        public string HasUnionField()
        {
            int intUserID = SessionItem.CheckLogin(0);
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
            if (onlineRowByUserID != null)
            {
                int intUnionID = (int) onlineRowByUserID["UnionID"];
                if (intUnionID < 1)
                {
                    return "";
                }
                DataTable table = BTPUnionFieldManager.GegACTFieldTableByUnionID(intUnionID);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    return ("<a href='Main_P.aspx?Tag=" + intUserID + "&Type=UNIONFIELD' target='Main'><img title='有经理向您的联盟发出了挑战' border='0' src=\"Images/alertm.gif\" width=\"14\" height=\"14\" align=\"absmiddle\" /></a>");
                }
            }
            return "";
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        public void MenuList()
        {

            /*this.strMenuList = string.Concat(new object[] { 
                "<tr><td onclick='ShowMenu(4)' style='height:28px; width:176px;'><a style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg2.gif) no-repeat 0px -27px' id='Img4' title='比赛约战、训练中心、自建杯赛；可以在这里训练球员、演练战术。' ></a></td></tr><tr id='Menu4' style='display:;'><td algin='center'><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLINELIST", "在 线 经 理", 1, "title='你可以和正在游戏的经理交流发送约战。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FMATCHMSG", "我 的 比 赛", 1, "title='查看自己的比赛记录。'"), "<td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FRIMATCHCENTER", "训 练 中 心", 1, "title='可以在这里报名等待别人向您发送训练赛。'"), "<tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FINDNICK", "同 城 交 友", 1, "title='您可以查找到很多来自同一个城市的朋友。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLYONEMATCH", "胜 者 为 王", 1, "title='体验一夫当千的快感。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XBATOP", "实 力 排 行", 1, "title='排行榜。'"), "</tr></table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(1)' style='height:28px; width:176px;'><a id='Img1' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -54px' title='球员管理、训练；职员管理、球队财政、球场升级、悬挂广告、更改球票、查看荣誉。' ></a></td></tr><tr id='Menu1' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=PLAYERCENTER", "球 员 管 理", 1, "title='出售球员、让球员参加选秀、下放、续约、从街球队向职业队提拔球员。'"), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_S.aspx?Type=STAFF", "职 员 管 理", 1, "title='好的职员帮助你的球员有更好的状态。'"), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=TRAINPLAYERCENTER", "球 员 训 练", 1, "title='可训练街球队员和职业队员，两种训练方式不同。'"), 
                "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_S.aspx?Type=FINANCE", "球 队 财 政", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=CLUBBUILD", "球 队 建 设", this.intMenuCategory), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=HONOUR", "球 队 荣 誉", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XGUESS", "冠 军 竞 猜", 1), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(2)' style='height:28px; width:176px;'><a id='Img2' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -82px' title='职业联赛赛程、排名；街球杯赛、职业XBA杯、联盟至尊杯。'></a></td></tr><tr id='Menu2' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=DEVISION", "职 业 联 赛", this.intMenuCategory), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=CUPLIST", "街 球 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP", "冠 军 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNIONCUP", "至 尊 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=DEVCUP", "自 建 杯 赛", 1, "title='一些没有加密的自建杯赛可以报名。'"), 
                "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLYONEMATCH", "胜 者 为 王", 1, "title='体验一夫当千的快感。'"), "</tr></table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(3)' style='height:28px; width:176px;'><a id='Img3' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -109px' title='职业战术、街球战术；恰当的战术是赢球的关键。' ></a></td></tr><tr id='Menu3' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=VARRANGE", "职 业 战 术", this.intMenuCategory1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=SARRANGE", "街 球 战 术", 1), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td onclick='ShowMenu(6)' id='Img6'  style='height:28px; width:176px;cursor:hand; background:url(Images/menubg1.gif) no-repeat 0px -193px'><a  style='height:28px; width:124px; display:block; float:left;' title='加入或组建自己的联盟，和朋友一起玩游戏。'></a><div style='margin:6px 22px 0 0' id='HasField1'></div></td></tr><tr id='Menu6' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Type=UNIONLIST", "联 盟 列 表", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNION&amp;Kind=VIEWUNION", "我 的 联 盟", this.intMenuCategoryU), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetFieldMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNIONFIELD", "联 盟 战 争 ", this.intMenuCategoryU), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -137px'><A style='display:block; height:28px; width:176px;' href=\"Main_P.aspx?Tag=", this.intUserID, "&Type=TRANSFERMAEKET\" target=\"Main\" title='这里有很多不同的球员市场，可以购买到更好的球员。' ></A></td></tr><tr><td style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -165px'><a style='display:block; height:28px; width:176px;' href='Main_P.aspx?Tag=", this.intUserID, 
                "&Type=TOOLS' target='Main' title='' ></a></td></tr><tr id='Menu5' style='display:none;'><td><table width='138' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td width='5' height='5'><img src='Images/corner_1.gif' width='5' height='5'></td><td background='Images/top.gif'></td><td width='7'><img src='Images/corner_2.gif' width='7' height='5'></td></tr><tr><td height='7'><img src='Images/corner_4.gif' width='5' height='7'></td><td background='Images/down.gif'></td><td><img src='Images/corner_3.gif' width='7' height='7'></td></tr></table></td></tr>"
             });*/

            this.strMenuList = string.Concat(new object[] { 
                "<tr><td onclick='ShowMenu(4)' style='height:28px; width:176px;'><a style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg2.gif) no-repeat 0px -27px' id='Img4' title='比赛约战、训练中心、自建杯赛；可以在这里训练球员、演练战术。' ></a></td></tr><tr id='Menu4' style='display:;'><td algin='center'><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLINELIST", "在 线 经 理", 1, "title='你可以和正在游戏的经理交流发送约战。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FMATCHMSG", "我 的 比 赛", 1, "title='查看自己的比赛记录。'"), "<td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FRIMATCHCENTER", "训 练 中 心", 1, "title='可以在这里报名等待别人向您发送训练赛。'"), "<tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=FINDNICK", "同 城 交 友", 1, "title='您可以查找到很多来自同一个城市的朋友。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLYONEMATCH", "胜 者 为 王", 1, "title='体验一夫当千的快感。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XBATOP", "实 力 排 行", 1, "title='排行榜。'"), "</tr></table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(1)' style='height:28px; width:176px;'><a id='Img1' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -54px' title='球员管理、训练；职员管理、球队财政、球场升级、悬挂广告、更改球票、查看荣誉。' ></a></td></tr><tr id='Menu1' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=PLAYERCENTER", "球 员 管 理", 1, "title='出售球员、让球员参加选秀、下放、续约、从街球队向职业队提拔球员。'"), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_S.aspx?Type=STAFF", "职 员 管 理", 1, "title='好的职员帮助你的球员有更好的状态。'"), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=TRAINPLAYERCENTER", "球 员 训 练", 1, "title='可训练街球队员和职业队员，两种训练方式不同。'"), 
                "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_S.aspx?Type=FINANCE", "球 队 财 政", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=CLUBBUILD", "球 队 建 设", this.intMenuCategory), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=HONOUR", "球 队 荣 誉", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XGUESS", "冠 军 竞 猜", 1), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(2)' style='height:28px; width:176px;'><a id='Img2' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -82px' title='职业联赛赛程、排名；街球杯赛、职业XBA杯、联盟至尊杯。'></a></td></tr><tr id='Menu2' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=DEVISION", "职 业 联 赛", this.intMenuCategory), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=CUPLIST", "街 球 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP", "冠 军 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNIONCUP", "至 尊 杯 赛", 1), "<tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=DEVCUP", "自 建 杯 赛", 1, "title='一些没有加密的自建杯赛可以报名。'"), 
                "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=ONLYONEMATCH", "胜 者 为 王", 1, "title='体验一夫当千的快感。'"), "</tr><tr><td background='Images/line.gif' height='1'></td></tr><tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=STARMATCH", "全 明 星 赛", 1, "title='全明星球员云集的赛事。'"), "</tr></table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style='cursor:hand;' onclick='ShowMenu(3)' style='height:28px; width:176px;'><a id='Img3' style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -109px' title='职业战术、街球战术；恰当的战术是赢球的关键。' ></a></td></tr><tr id='Menu3' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=VARRANGE", "职 业 战 术", this.intMenuCategory1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=SARRANGE", "街 球 战 术", 1), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td onclick='ShowMenu(6)' id='Img6'  style='height:28px; width:176px;cursor:hand; background:url(Images/menubg1.gif) no-repeat 0px -193px'><a  style='height:28px; width:124px; display:block; float:left;' title='加入或组建自己的联盟，和朋友一起玩游戏。'></a><div style='margin:6px 22px 0 0' id='HasField1'></div></td></tr><tr id='Menu6' style='display:none;'><td><table width='164' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='width:5px;height:5px;background:url(Images/xbg1.gif) no-repeat -168px 0px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -29px'></td><td style='width:6px;height:5px;background:url(Images/xbg1.gif) no-repeat -173px 0px'></td></tr><tr><td style='background:url(Images/xbg1.gif) repeat-y -180px 0px; height:37px;'></td><td align='left' valign='top' bgcolor='#FBF2EB'><table cellpadding='0' cellspacing='0' width='153' bgcolor='#FBF2EB'>", this.GetChildMenu("Main_P.aspx?Type=UNIONLIST", "联 盟 列 表", 1), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetChildMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNION&amp;Kind=VIEWUNION", "我 的 联 盟", this.intMenuCategoryU), "<tr><td background='Images/line.gif' height='1'></td></tr>", this.GetFieldMenu("Main_P.aspx?Tag=" + this.intUserID + "&Type=UNIONFIELD", "联 盟 战 争 ", this.intMenuCategoryU), "</table></td><td style='background:url(Images/xbg1.gif) repeat-y -185px 0px'></td></tr><tr><td style='width:5px;height:7px;background:url(Images/xbg1.gif) no-repeat -168px -5px'></td><td style='background:url(Images/xbg2.gif) repeat-x 0px -113px'></td><td style='width:6px;height:7px;background:url(Images/xbg1.gif) no-repeat -173px -5px'></td></tr></table></td></tr><tr><td style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -137px'><A style='display:block; height:28px; width:176px;' href=\"Main_P.aspx?Tag=", this.intUserID, "&Type=TRANSFERMAEKET\" target=\"Main\" title='这里有很多不同的球员市场，可以购买到更好的球员。' ></A></td></tr><tr><td style=' cursor:hand; height:28px; width:176px; display:block; background:url(Images/menubg1.gif) no-repeat 0px -165px'><a style='display:block; height:28px; width:176px;' href='Main_P.aspx?Tag=", this.intUserID, 
                "&Type=TOOLS' target='Main' title='' ></a></td></tr><tr id='Menu5' style='display:none;'><td><table width='138' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td width='5' height='5'><img src='Images/corner_1.gif' width='5' height='5'></td><td background='Images/top.gif'></td><td width='7'><img src='Images/corner_2.gif' width='7' height='5'></td></tr><tr><td height='7'><img src='Images/corner_4.gif' width='5' height='7'></td><td background='Images/down.gif'></td><td><img src='Images/corner_3.gif' width='7' height='7'></td></tr></table></td></tr>"
             });
             
           
        }

        protected override void OnInit(EventArgs e)
        {
            this.strServerName = DBLogin.GameNameChinese(ServerParameter.intGameCategory);
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                string strAnnounce;
                this.strMainScript = StringItem.Get51laURL();
                this.strAnnounce = "";
                DataTable announceTable = BTPAnnounceManager.GetAnnounceTable();
                if (announceTable != null)
                {
                    foreach (DataRow row in announceTable.Rows)
                    {
                        strAnnounce = this.strAnnounce;
                        this.strAnnounce = strAnnounce + row["Title"].ToString() + "&nbsp;[ <font color='#660066'>" + StringItem.FormatDate((DateTime) row["CreateTime"], "yyyy-MM-dd hh:mm") + "</font> ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                else
                {
                    this.strAnnounce = this.strAnnounce + "暂无公告&nbsp;[ <font color='#660066'>" + StringItem.FormatDate(DateTime.Now, "yyyy-MM-dd hh:mm") + "</font> ]&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                string str = onlineRowByUserID["DiskURL"].ToString();
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                this.strFace = DBLogin.URLString(0) + str + "Face.png";
                this.strFace = string.Concat(new object[] { "<div style='position:absolute;top:114px;left:82px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", this.strFace, "?RndID=", RandomItem.rnd.Next(0, 10), ");width:37px;height:40px'></div>" });
                int num = Convert.ToInt32(onlineRowByUserID["Category"]);
                this.intMsgFlag = Convert.ToInt32(onlineRowByUserID["HasMsg"]);
                this.intPayType = Convert.ToInt32(onlineRowByUserID["PayType"]);
                this.blnSex = (bool) onlineRowByUserID["Sex"];
                int num2 = (int) onlineRowByUserID["UnionID"];
                if (num2 < 1)
                {
                    this.intMenuCategoryU = 0;
                }
                else
                {
                    this.strHasUnionField = "HasUnionField()";
                    this.intMenuCategoryU = 1;
                }
                onlineRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                int num3 = 0;
                num3 = (int) onlineRowByUserID["AutoTrainDev"];
                int num4 = (int) onlineRowByUserID["AutoTrain"];
                if ((num4 > 0) || (num3 > 0))
                {
                    base.Response.Redirect("AutoPlayerTrain.aspx");
                }
                this.si = new SecretaryItem(this.intUserID, this.blnSex);
                this.strSecFace = this.si.GetImgFace();
                if (((num != 1) && (num != 2)) && (num != 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    if (num == 5)
                    {
                        this.intMenuCategory = 1;
                        this.intMenuCategory1 = 1;
                    }
                    else
                    {
                        if (num == 2)
                        {
                            this.intMenuCategory1 = 1;
                        }
                        else
                        {
                            this.intMenuCategory1 = 0;
                        }
                        this.intMenuCategory = 0;
                    }
                    string str2 = "";
                    if (this.intPayType == 1)
                    {
                        str2 = "<font color=red >&nbsp;&nbsp;[会员]</font>";
                    }
                    this.strMsg = "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='40' align='center' width='100%'></td><tr><td height='20' align='center' colspan=2><font class='ForumTime'>" + this.strNickName + str2 + "</font></td></tr><tr><td height='27' align='center' colspan='2'>";
                    if (ServerParameter.strCopartner == "DUNIU")
                    {
                        this.strMsg = this.strMsg + "<a target=\"_blank\" href='http://bbs.mmoabc.com/forums/18'><img src='" + SessionItem.GetImageURL() + "dnluntan.gif' width='51' height='24' border='0'></a>&nbsp;";
                    }
                    else
                    {
                        strAnnounce = this.strMsg;
                        this.strMsg = strAnnounce + "<a href='" + Config.GetDomain() + "MemberCenter.aspx'><img src='" + SessionItem.GetImageURL() + "passport.gif' width='51' height='24' border='0'></a>&nbsp;";
                    }
                    object strMsg = this.strMsg;
                    this.strMsg = string.Concat(new object[] { strMsg, "<a href='http://bbs.113388.net' target='_blank'><img src='", SessionItem.GetImageURL(), "offlinetrain.gif' width='66' height='24' border='0'></a>&nbsp;" });
                    strAnnounce = this.strMsg;
                    this.strMsg = strAnnounce + "<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "quit.gif' width='40' height='24' border='0'></a></td></tr></table>";
                    strMsg = this.strSetting;
                    this.strSetting = string.Concat(new object[] { strMsg, "<a href='Main_P.aspx?Tag=", this.intUserID, "&Type=MODIFYCLUB' target='Main' title='俱乐部的基本设置，可以更改俱乐部名称、标志、队服等。' style='display:block; width:40px; height:25px;'></a>" });
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main));
            this.strServerTime = DateTime.Now.ToString();
            this.strGetInfoList = this.GetInfoList();
            this.MenuList();
        }
    }
}

