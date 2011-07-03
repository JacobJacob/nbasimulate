namespace Web.Helper
{
    using System;

    public class ClubItem
    {
        public static string GetClubNameInfo(int intUserID, string strClubName, string ShortName, string strTarget, int intLength)
        {
            if (ShortName.Trim() == "")
            {
                return string.Concat(new object[] { "<a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" title=\"", strClubName, "\" target=\"", strTarget, "\">", StringItem.GetShortString(strClubName, intLength, "."), "</a>" });
            }
            return string.Concat(new object[] { "<a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" title=\"", strClubName, "\" target=\"", strTarget, "\">", StringItem.GetShortString(ShortName + "-" + strClubName, intLength, "."), "</a>" });
        }
    }
}

