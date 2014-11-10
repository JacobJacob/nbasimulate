namespace Web.WebService
{
    using LoginParameter;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.Services;
    using Web.DBData;
    using Web.Helper;

    public class GameService : WebService
    {
        private IContainer components;

        public GameService()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [WebMethod]
        public string GetGameInfo(int intCategory, int intUserID)
        {
            string str;
            string str2;
            string str3;
            string str4;
            int num;
            bool flag = ROOTUserGameManager.HasUserGame(intUserID, intCategory);
            DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(intUserID);
            if (userRowByUserID != null)
            {
                str = userRowByUserID["UserName"].ToString().Trim();
                str2 = userRowByUserID["Password"].ToString().Trim();
            }
            else
            {
                str = "";
                str2 = "";
            }
            DBLogin.GameNameChinese(intCategory);
            string str5 = ServerItem.GameImgName(intCategory);
            BTPClubManager.GetClubCountByCategory(intCategory, 3);
            BTPClubManager.GetClubCountByCategory(intCategory, 5);
            int devBlank = BTPDevManager.GetDevBlank(intCategory);
            bool flag2 = false;
            string str6 = "";
            if (!DBLogin.CanConn(intCategory))
            {
                return "服务器暂时无法连通！";
            }
            DataRow gameRow = BTPGameManager.GetGameRow(DBLogin.ConnString(intCategory));
            if (gameRow != null)
            {
                flag2 = (bool) gameRow["CantReg"];
                num = (byte) gameRow["Status"];
            }
            else
            {
                flag2 = true;
                num = 0;
            }
            if (num == 1)
            {
                str6 = "[ <font color='#8E8E8E'>更新中...</font> ]";
            }
            else
            {
                int num3 = 0;
                try
                {
                    num3 = Convert.ToInt16(HttpRequestItem.GetString(DBLogin.URLString(intCategory) + "GetOnlineCount.aspx"));
                }
                catch
                {
                    num3 = 0;
                }
                str6 = "[ 在线：" + num3 + " ]";
            }
            if (!flag)
            {
                if (!flag2)
                {
                    str3 = "您可以组建一支街头篮球队。";
                    str4 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "/web/button_04.gif' height='19' width='62' border='0'></a>";
                }
                else
                {
                    str3 = "已停止注册。";
                    str4 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
                }
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID, DBLogin.ConnString(intCategory));
                if (accountRowByUserID == null)
                {
                    if (!flag2)
                    {
                        str3 = "您可以组建一支街头篮球队。";
                        str4 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_04.gif' height='19' width='62' border='0'></a>";
                    }
                    else
                    {
                        str3 = "已停止注册。";
                        str4 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
                    }
                }
                else
                {
                    switch (((byte) accountRowByUserID["Category"]))
                    {
                        case 0:
                        case 4:
                            if (flag2)
                            {
                                str3 = "已停止注册。";
                                str4 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
                            }
                            else
                            {
                                str3 = "您可以组建一支街头篮球队。";
                                str4 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_04.gif' height='19' width='62' border='0'></a>";
                            }
                            goto Label_035C;

                        case 3:
                            str3 = "您可以为您的球队招募球员。";
                            str4 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=AssMain.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_05.gif' height='19' width='62' border='0'></a>";
                            goto Label_035C;
                    }
                    str3 = "您在此赛区拥有一支球队。";
                    str4 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=WebAD.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_07.gif' height='19' width='62' border='0'></a>";
                }
            }
        Label_035C:;
            return string.Concat(new object[] { "<table width='100%'  border='0' cellspacing='0' cellpadding='0' height='46'><tr><td rowspan='2' align='center' width='165'><img src='", SessionItem.GetImageURL(), "web/", str5, "' width='165' height='46'></td><td height='26' style='padding-left:5px' colspan='2'>联赛空位 [ ", devBlank, " ] ", str6, " </td></tr><tr><td height='20'><font style='line-height:150%;padding-left:5px'>", str3, "</font></td><td align='right'>", str4, "</td></tr></table>" });
        }

        private void InitializeComponent()
        {
        }
    }
}

