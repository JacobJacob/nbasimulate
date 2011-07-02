namespace Web.Helper
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Web;
    using Web;
    using Web.DBData;

    public class SessionItem
    {
        public static void BeginReadReport()
        {
            HttpContext current = HttpContext.Current;
            current.Application.Lock();
            ((DataTable) current.Application["ParameterTable"]).Select("ParameterID=1")[0]["LatestReadTime"] = DateTime.Now;
            current.Application.UnLock();
        }

        public static bool CanReadReport()
        {
            int num = 0x1388;
            HttpContext current = HttpContext.Current;
            current.Application.Lock();
            DataRow[] rowArray = ((DataTable) current.Application["ParameterTable"]).Select("ParameterID=1");
            current.Application.UnLock();
            if (DateTime.Now.AddMilliseconds((double) -num) >= ((DateTime) rowArray[0]["LatestReadTime"]))
            {
                return true;
            }
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep((int) ((num * 4) / 5));
                if (DateTime.Now.AddMilliseconds((double) -num) >= ((DateTime) rowArray[0]["LatestReadTime"]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CanUseAfterUpdate()
        {
            if ((DateTime.Now >= DateTime.Today.AddHours((double) Global.intStartUpdate)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
            {
                return false;
            }
            return true;
        }

        public static int Check51Login()
        {
            HttpContext current = HttpContext.Current;
            if (IsLogin(current.Session[Global.strSessionName]))
            {
                return (int) current.Session[Global.strSessionName];
            }
            return -1;
        }

        public static void CheckCanUseAfterUpdate(int intCagegory)
        {
            if ((intCagegory == 5) && !CanUseAfterUpdate())
            {
                HttpContext.Current.Response.Redirect("Report.aspx?Parameter=1001");
            }
        }

        public static int CheckLogin(int intType)
        {
            HttpContext current = HttpContext.Current;
            if (!IsLogin(current.Request.Cookies[Global.strSessionName]))
            {
                return -1;
            }
            int intUserID = Convert.ToInt32(StringItem.MD5Decrypt(current.Request.Cookies[Global.strSessionName].Value.ToString().Trim(), Global.strMainMD5Key));
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
            if (onlineRowByUserID == null)
            {
                return -1;
            }
            int num2 = (byte) onlineRowByUserID["Ischild"];
            num2 = 8;
            if ((num2 != 8) && (BTPAccountManager.IschildGame(intUserID) == 1))
            {
                current.Response.Redirect("Report.aspx?Parameter=19d");
                return -1;
            }
            if (intType == 1)
            {
                switch (BTPGameManager.GetStatus())
                {
                    case 1:
                        SetLogout(intUserID);
                        current.Response.Redirect("Report.aspx?Parameter=1003");
                        return -1;

                    case 2:
                        SetLogout(intUserID);
                        current.Response.Redirect("Report.aspx?Parameter=1004");
                        return -1;
                }
            }
            return intUserID;
        }

        public static string GetAllURL()
        {
            HttpContext current = HttpContext.Current;
            string str = current.Request.ServerVariables["URL"];
            int count = current.Request.QueryString.Count;
            if (count > 0)
            {
                str = str + "?";
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        str = str + current.Request.QueryString.AllKeys[i] + "=" + current.Request.QueryString[i];
                    }
                    else
                    {
                        string str2 = str;
                        str = str2 + "&" + current.Request.QueryString.AllKeys[i] + "=" + current.Request.QueryString[i];
                    }
                }
            }
            return str;
        }

        public static string GetBBSURL(string strURL)
        {
            int index = strURL.IndexOf("?");
            string str = strURL.Substring(0, index + 1);
            strURL = strURL.Substring(index + 1);
            if (str.ToLower().IndexOf("bbs.xba.com.cn") >= 0)
            {
                int num3 = 0;
                string[] strArray = new string[10];
                string[] strArray2 = new string[10];
                while (strURL != "")
                {
                    int length = strURL.IndexOf("=");
                    strArray[num3] = strURL.Substring(0, length);
                    strURL = strURL.Substring(length + 1);
                    length = strURL.IndexOf("&");
                    if (length > 0)
                    {
                        strArray2[num3] = strURL.Substring(0, length);
                        strURL = strURL.Substring(length + 1);
                    }
                    else
                    {
                        strArray2[num3] = strURL;
                        strURL = "";
                    }
                    num3++;
                }
                num3--;
                strURL = "Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Topic.aspx!" + strArray[num3] + "." + strArray2[num3];
                num3--;
                while (num3 >= 0)
                {
                    strURL = strURL + "^" + strArray[num3];
                    strURL = strURL + "." + strArray2[num3];
                    num3--;
                }
                return strURL;
            }
            strURL = str + strURL;
            return strURL;
        }

        public static string GetDomainUserID()
        {
            string pToDecrypt = string.Empty;
            try
            {
                pToDecrypt = HttpContext.Current.Request.Cookies["DomainUserID"].Value;
            }
            catch
            {
                pToDecrypt = string.Empty;
            }
            return StringItem.MD5Decrypt(pToDecrypt, Global.strMainMD5Key);
        }

        public static string GetImageURL()
        {
            HttpContext current = HttpContext.Current;
            if (current.Request.Cookies["ImageURL"] == null)
            {
                return "Images/";
            }
            if ((current.Request.Cookies["ImageURL"].Value.IndexOf(@"\") == -1) && (current.Request.Cookies["ImageURL"].Value.IndexOf("/") == -1))
            {
                return "Images/";
            }
            return current.Request.Cookies["ImageURL"].Value;
        }

        public static object GetRequest(string strIndex, int intType)
        {
            object obj2 = HttpContext.Current.Request.QueryString[strIndex];
            if (obj2 == null)
            {
                if (intType == 0)
                {
                    return 0;
                }
                if (intType == 1)
                {
                    return "";
                }
                if (intType == 2)
                {
                    return Convert.ToInt16(0);
                }
                return Convert.ToInt64(0);
            }
            try
            {
                if (intType == 0)
                {
                    return Convert.ToInt32(obj2.ToString().Trim());
                }
                if (intType == 1)
                {
                    return obj2.ToString().Trim();
                }
                if (intType == 2)
                {
                    return Convert.ToInt16(obj2.ToString().Trim());
                }
                return Convert.ToInt64(obj2.ToString().Trim());
            }
            catch
            {
                if (intType == 0)
                {
                    return 0;
                }
                if (intType == 1)
                {
                    return "";
                }
                if (intType == 2)
                {
                    return Convert.ToInt16(0);
                }
                return Convert.ToInt64(0);
            }
        }

        public static bool IsLogin(object obj)
        {
            HttpContext current = HttpContext.Current;
            if (obj == null)
            {
                return false;
            }
            string pToDecrypt = current.Request.Cookies[Global.strSessionName].Value.ToString();
            int num = -1;
            if (pToDecrypt != "")
            {
                num = Convert.ToInt32(StringItem.MD5Decrypt(pToDecrypt, Global.strMainMD5Key));
            }
            else
            {
                num = -1;
            }
            if (num <= 0)
            {
                return false;
            }
            return true;
        }

        public static void JumpToRequestCookiePage(int intNeedLogin)
        {
            string str = GetAllURL().ToLower().Replace("?", "!").Replace("&", "^").Replace("=", ".");
            HttpContext current = HttpContext.Current;
            str = "http://" + current.Request.ServerVariables["HTTP_HOST"] + str;
            current.Response.Redirect(string.Concat(new object[] { "http://www.xba.com.cn/RequestCookiePage.aspx?NeedLogin=", intNeedLogin, "&JumpPage=", str }));
        }

        public static void JumpToTargetPage()
        {
            string request = (string) GetRequest("JumpPage", 1);
            if (request.IndexOf("!") != -1)
            {
                Cuter cuter = new Cuter(request.Replace("!", ","));
                string str2 = cuter.GetCuter(0);
                string str3 = cuter.GetCuter(1).Replace("^", "&").Replace(".", "=");
                request = str2 + "?" + str3;
            }
            request = request.Replace("/", "");
            HttpContext.Current.Response.Redirect(request);
        }

        public static void SetLoginCookies(int intUserID, int intExpireSec)
        {
            string str;
            if (ServerParameter.intGameCategory == 0)
            {
                str = "LoginS";
            }
            else
            {
                str = "LoginG";
            }
            HttpContext current = HttpContext.Current;
            HttpCookie cookie = new HttpCookie(str);
            cookie.Expires = DateTime.Now.AddHours((double) intExpireSec);
            cookie.Value = intUserID.ToString();
            current.Response.Cookies.Add(cookie);
            cookie = null;
            current = null;
        }

        public static bool SetLogout(int intUserID)
        {
            HttpContext current = HttpContext.Current;
            try
            {
                DTOnlineManager.DeleteOnlineRow(intUserID);
                current.Response.Cookies[Global.strSessionName].Expires = DateTime.Now.AddDays(-10.0);
                current.Response.Cookies["LoginS"].Expires = DateTime.Now.AddDays(-10.0);
                current.Response.Cookies["LoginG"].Expires = DateTime.Now.AddDays(-10.0);
                current.Session["RepSession"] = null;
                return true;
            }
            catch (Exception exception)
            {
                current.Response.Cookies["LoginS"].Expires = DateTime.Now.AddDays(-10.0);
                current.Response.Cookies["LoginG"].Expires = DateTime.Now.AddDays(-10.0);
                current.Session["RepSession"] = null;
                exception.ToString();
                return false;
            }
        }

        public static void SetMainLogin(DataRow dr, bool blnNeedCheck)
        {
            HttpContext current = HttpContext.Current;
            if (dr != null)
            {
                int intUserID = (int) dr["UserID"];
                string strUserName = dr["UserName"].ToString().Trim();
                string strPassword = dr["Password"].ToString().Trim();
                if (intUserID > 0)
                {
                    ROOTUserManager.UpdateActiveTimeByID40(intUserID);
                    string strNickName = dr["NickName"].ToString().Trim();
                    int intPayType = 0;
                    int num3 = (byte) dr["LockTime"];
                    if (num3 > 0)
                    {
                        //dr.Close();
                        current.Response.Redirect("Report.aspx?Parameter=19a");
                        return;
                    }
                    bool blnSex = (bool) dr["Gender"];
                    string strDiskURL = dr["DiskURL"].ToString().Trim();
                    int intCategory = 0;
                    int num5 = 0;
                    int num6 = 0;
                    string str5 = "";
                    string str6 = "";
                    string strClubLogo = "";
                    int intMsgFlag = 0;
                    int intLevel = 0;
                    int intUnionID = 0;
                    string strShortName = "";
                    string strGuideCode = "";
                    string strQQ = dr["QQ"].ToString().Trim();
                    int intWealth = (int) dr["Wealth"];
                    int intIschild = 8;
                    SetOnlineInfo(current, intUserID, ServerParameter.intGameCategory, intCategory, intPayType, blnSex, strDiskURL, strUserName, strPassword, strNickName, num5, num6, str5, str6, strClubLogo, intMsgFlag, intLevel, intUnionID, strShortName, strGuideCode, strQQ, blnNeedCheck, "无", intWealth, intIschild);
                }
                //dr.Close();
            }
            else
            {
                //dr.Close();
                current.Response.Redirect("Report.aspx?Parameter=10");
            }
        }

        public static int SetMainLoginWithPara(SqlDataReader dr, bool blnNeedCheck)
        {
            HttpContext current = HttpContext.Current;
            if (dr.Read())
            {
                int intUserID = (int) dr["UserID"];
                string strUserName = dr["UserName"].ToString().Trim();
                string strPassword = dr["Password"].ToString().Trim();
                if (intUserID > 0)
                {
                    ROOTUserManager.UpdateActiveTimeByID40(intUserID);
                    string strNickName = dr["NickName"].ToString().Trim();
                    int intPayType = (byte) dr["PayType"];
                    int num3 = (byte) dr["LockTime"];
                    if (num3 > 0)
                    {
                        dr.Close();
                        return 0x13;
                    }
                    bool blnSex = (bool) dr["Sex"];
                    string strDiskURL = dr["DiskURL"].ToString().Trim();
                    int intCategory = 0;
                    int num5 = 0;
                    int num6 = 0;
                    string str5 = "";
                    string str6 = "";
                    string strClubLogo = "";
                    int intMsgFlag = 0;
                    int intLevel = 0;
                    int intUnionID = 0;
                    string strShortName = "";
                    string strGuideCode = "";
                    string strQQ = dr["QQ"].ToString().Trim();
                    int intWealth = (int) dr["Wealth"];
                    int intIschild = (int) dr["Ischild"];
                    SetOnlineInfo(current, intUserID, ServerParameter.intGameCategory, intCategory, intPayType, blnSex, strDiskURL, strUserName, strPassword, strNickName, num5, num6, str5, str6, strClubLogo, intMsgFlag, intLevel, intUnionID, strShortName, strGuideCode, strQQ, blnNeedCheck, "无", intWealth, intIschild);
                    dr.Close();
                    return 1;
                }
                dr.Close();
                return 10;
            }
            dr.Close();
            return 10;
        }

        public static void SetOnlineInfo(HttpContext hc, int intUserID, int intGameCategory, int intCategory, int intPayType, bool blnSex, string strDiskURL, string strUserName, string strPassword, string strNickName, int intClubID3, int intClubID5, string strClubName3, string strClubName5, string strClubLogo, int intMsgFlag, int intLevel, int intUnionID, string strShortName, string strGuideCode, string strQQ, bool blnNeedCheck, string strDevCode, int intWealth, int intIschild)
        {
            if (blnNeedCheck)
            {
                int num;
                try
                {
                    if (intGameCategory == 0)
                    {
                        num = Convert.ToInt32(hc.Request.Cookies["LoginS"].Value);
                    }
                    else
                    {
                        num = Convert.ToInt32(hc.Request.Cookies["LoginG"].Value);
                    }
                }
                catch
                {
                    num = 0;
                }
                if (num > 0)
                {
                    hc.Response.Redirect("Report.aspx?Parameter=1007");
                    return;
                }
            }
            string str = StringItem.MD5Encrypt(intUserID.ToString(), Global.strMainMD5Key);
            HttpCookie cookie = new HttpCookie(Global.strSessionName);
            cookie.Value = str;
            hc.Response.Cookies.Add(cookie);
            cookie = null;
            hc = null;
            SetLoginCookies(intUserID, 30);
            DataTable onlineTableWithoutLimit = DTOnlineManager.GetOnlineTableWithoutLimit();
            if (DTOnlineManager.GetOnlineRowByUserID(intUserID) != null)
            {
                DTOnlineManager.DeleteOnlineRow(intUserID);
            }
            DTOnlineManager.InsertOnlineInfo(intUserID, intGameCategory, intCategory, intPayType, blnSex, strDiskURL, strUserName, strPassword, strNickName, intClubID3, intClubID5, strClubName3, strClubName5, strClubLogo, DateTime.Now, intMsgFlag, intLevel, intUnionID, strShortName, strGuideCode, strQQ, strDevCode, intWealth, intIschild);
            int num2 = 30;
            if (onlineTableWithoutLimit != null)
            {
                DataRow[] rowArray = onlineTableWithoutLimit.Select("LatestActiveTime<'" + DateTime.Now.AddMinutes((double) -num2).ToString() + "'");
                if (rowArray.Length > 0)
                {
                    int num3 = 0;
                    foreach (DataRow row2 in rowArray)
                    {
                        num3 = (int) row2["UserID"];
                        DTOnlineManager.DeleteOnlineRow(num3);
                    }
                }
            }
        }

        public static int SetSelfLogin(string strUserName, string strPassword, bool blnNeedCheck)
        {
            int num;
            strUserName = StringItem.MD5Decrypt(strUserName, Global.strMD5Key);
            strPassword = StringItem.MD5Decrypt(strPassword, Global.strMD5Key);
            HttpContext current = HttpContext.Current;
            DataTable infoByUserNamePassword = BTPAccountManager.GetInfoByUserNamePassword(strUserName, strPassword);
            if(infoByUserNamePassword!=null){
                num = -1;
                foreach (DataRow row in infoByUserNamePassword.Rows)
                {
                    num = (int)row["UserID"];
                    if (num > 0)
                    {
                        string strNickName = row["NickName"].ToString().Trim();
                        int intPayType = (byte)row["PayType"];
                        if ((DTOnlineManager.GetOnlineCount() >= Global.intOnlineLimit) && (intPayType != 1))
                        {
                            current.Response.Redirect("Report.aspx?Parameter=10121");
                            return -3;
                        }
                        bool blnSex = (bool)row["Sex"];
                        string strDiskURL = row["DiskURL"].ToString().Trim();
                        int intCategory = (byte)row["Category"];
                        int num5 = (byte)row["LockTime"];
                        int num6 = (int)row["ClubID3"];
                        int num7 = (int)row["ClubID5"];
                        string str3 = row["ClubName3"].ToString().Trim();
                        string str4 = row["ClubName5"].ToString().Trim();
                        string strClubLogo = row["ClubLogo"].ToString().Trim();
                        int intMsgFlag = (int)row["MsgFlag"];
                        int intLevel = (int)row["Levels"];
                        int intUnionID = (int)row["UnionID"];
                        string strShortName = row["ShortName"].ToString().Trim();
                        string strGuideCode = row["GuideCode"].ToString().Trim();
                        string strQQ = row["QQ"].ToString().Trim();
                        string devCodeByUserID = BTPDevManager.GetDevCodeByUserID(num);
                        int intWealth = (int)row["Wealth"];
                        int intIschild = (byte)row["Ischild"];
                        DateTime dtActiveTime = (DateTime)row["ActiveTime"];
                        DateTime dtOldTime = (DateTime)Global.drParameter["OldTime"];
                        intIschild = 8;
                        if ((intIschild == 1) && (BTPAccountManager.IschildLogin(num, dtActiveTime) == 0))
                        {
                            current.Response.Redirect("Report.aspx?Parameter=19c");
                            return 0;
                        }
                        if (dtActiveTime < dtOldTime)
                        {
                            BTPAccountManager.OldUserLogin(num, dtActiveTime, dtOldTime);
                        }
                        if (num5 > 0)
                        {
                            current.Response.Redirect("Report.aspx?Parameter=19");
                            return 0;
                        }
                        BTPAccountManager.AddLoginIP(num, current.Request.ServerVariables["REMOTE_ADDR"]);
                        SetOnlineInfo(current, num, ServerParameter.intGameCategory, intCategory, intPayType, blnSex, strDiskURL, strUserName, strPassword, strNickName, num6, num7, str3, str4, strClubLogo, intMsgFlag, intLevel, intUnionID, strShortName, strGuideCode, strQQ, blnNeedCheck, devCodeByUserID, intWealth, intIschild);
                    }
                }
            }
            else
            {
                num = -1;
            }
            //infoByUserNamePassword.Close();
            return num;
        }

        public static int SetSomebodyGameLogin(int intUserID)
        {
            HttpContext current = HttpContext.Current;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
            if (accountRowByUserID != null)
            {
                string strUserName = StringItem.MD5Encrypt(accountRowByUserID["UserName"].ToString().Trim(), Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(accountRowByUserID["Password"].ToString().Trim(), Global.strMD5Key);
                return SetSelfLogin(strUserName, strPassword, false);
            }
            DataRow dr = ROOTUserManager.Get40UserRowByUserID(intUserID);
            if (dr != null)
            {
                SetMainLogin(dr, false);
                return intUserID;
            }
            //dr.Close();
            current.Response.Redirect("Report.aspx?Parameter=12");
            return -1;
        }

        public static int SetSomebodyLogin(int intUserID)
        {
            DataRow row = ROOTUserManager.GetUPByUserID40(intUserID);
            string pToEncrypt = row["UserName"].ToString().Trim();
            string str2 = row["Password"].ToString().Trim();
            pToEncrypt = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
            str2 = StringItem.MD5Encrypt(str2, Global.strMD5Key);
            return SetSelfLogin(pToEncrypt, str2, false);
        }

        public static void Validate(HttpContext context, object data, ref HttpValidationStatus status)
        {
            if (context.Request.QueryString["Valid"] == "false")
            {
                status = HttpValidationStatus.Invalid;
            }
            else if (context.Request.QueryString["Valid"] == "ignore")
            {
                status = HttpValidationStatus.IgnoreThisRequest;
            }
            else
            {
                status = HttpValidationStatus.Valid;
            }
        }

        public static string GetMainUrl()
        {
            return "http://localhost:34591/xbaweb/";

        }
    }
}

