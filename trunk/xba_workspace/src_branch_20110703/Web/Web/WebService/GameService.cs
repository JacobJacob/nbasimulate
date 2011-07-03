namespace Web.WebService
{
    using LoginParameter;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Services;
    using Web.DBData;
    using Web.Helper;

    public class GameService : WebService
    {
        private IContainer components = null;

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
            string str5;
            string str6;
            int num2;
            bool flag = ROOTUserGameManager.HasUserGame(intUserID, intCategory);
            DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(intUserID);
            if (userRowByUserID != null)
            {
                str = userRowByUserID["UserName"].ToString().Trim();
                str2 = userRowByUserID["Password"].ToString().Trim();
                //userRowByUserID.Close();
            }
            else
            {
                str = "";
                str2 = "";
                //userRowByUserID.Close();
            }
            DBLogin.GameNameChinese(intCategory);
            string str3 = ServerItem.GameImgName(intCategory);
            BTPClubManager.GetClubCountByCategory(intCategory, 3);
            BTPClubManager.GetClubCountByCategory(intCategory, 5);
            int devBlank = BTPDevManager.GetDevBlank(intCategory);
            bool flag2 = false;
            string str7 = "";
            if (!DBLogin.CanConn(intCategory))
            {
                return "服务器暂时无法连通！";
            }
            DataRow gameRow = BTPGameManager.GetGameRow(DBLogin.ConnString(intCategory));
            if (gameRow != null)
            {
                flag2 = (bool) gameRow["CantReg"];
                num2 = (byte) gameRow["Status"];
            }
            else
            {
                flag2 = true;
                num2 = 0;
            }
            if (num2 == 1)
            {
                str7 = "[ <font color='#8E8E8E'>更新中...</font> ]";
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
                str7 = "[ 在线：" + num3 + " ]";
            }
            if (flag)
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID, DBLogin.ConnString(intCategory));
                if (accountRowByUserID != null)
                {
                    switch (((byte) accountRowByUserID["Category"]))
                    {
                        case 3:
                            str5 = "您可以为您的球队招募球员。";
                            str6 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=AssMain.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_05.gif' height='19' width='62' border='0'></a>";
                            goto Label_0365;

                        case 4:
                        case 0:
                            if (!flag2)
                            {
                                str5 = "您可以组建一支街头篮球队。";
                                str6 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_04.gif' height='19' width='62' border='0'></a>";
                            }
                            else
                            {
                                str5 = "已停止注册。";
                                str6 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
                            }
                            goto Label_0365;
                    }
                    str5 = "您在此赛区拥有一支球队。";
                    str6 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=WebAD.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_07.gif' height='19' width='62' border='0'></a>";
                }
                else if (!flag2)
                {
                    str5 = "您可以组建一支街头篮球队。";
                    str6 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "web/button_04.gif' height='19' width='62' border='0'></a>";
                }
                else
                {
                    str5 = "已停止注册。";
                    str6 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
                }
            }
            else if (!flag2)
            {
                str5 = "您可以组建一支街头篮球队。";
                str6 = "<a href=\"" + ServerItem.ToOtherServerURL(intCategory, str, str2, "URL=RegClub.aspx") + "\"><img src='" + SessionItem.GetImageURL() + "/web/button_04.gif' height='19' width='62' border='0'></a>";
            }
            else
            {
                str5 = "已停止注册。";
                str6 = "<img src='" + SessionItem.GetImageURL() + "web/button_G_04.gif' height='19' width='62' border='0'>";
            }
        Label_0365:;
            return string.Concat(new object[] { "<table width='100%'  border='0' cellspacing='0' cellpadding='0' height='46'><tr><td rowspan='2' align='center' width='165'><img src='", SessionItem.GetImageURL(), "web/", str3, "' width='165' height='46'></td><td height='26' style='padding-left:5px' colspan='2'>联赛空位 [ ", devBlank, " ] ", str7, " </td></tr><tr><td height='20'><font style='line-height:150%;padding-left:5px'>", str5, "</font></td><td align='right'>", str6, "</td></tr></table>" });
        }

        private void InitializeComponent()
        {
        }
    }
}

