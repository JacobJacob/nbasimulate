namespace Web.Helper
{
    using System;

    public class MarketZone
    {
        public static string GetZoneCode(string strDevCode)
        {
            if (strDevCode == null)
            {
                return "";
            }
            string str = strDevCode.Trim();
            if (str.Length < 3)
            {
                return "";
            }
            return str.Substring(0, 3);
        }
    }
}

