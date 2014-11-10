namespace Web.Helper
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;

    public class MainXmlHelper
    {
        public static string GetNewMainXml(string oldXml, Hashtable info)
        {
            if (oldXml == null)
            {
                oldXml = "<ScoreH></ScoreH><ScoreA>54</ScoreA><ClubNameH></ClubNameH><ClubNameA></ClubNameA><ClubLogoH></ClubLogoH><ClubLogoA></ClubLogoA><ClubSayH></ClubSayH><ClubSayA></ClubSayA><Tickets></Tickets><Income></Income><MVPName></MVPName><MVPStas></MVPStas><NClubNameH></NClubNameH><NClubNameA></NClubNameA><NClubLogoH></NClubLogoH><NClubLogoA></NClubLogoA><NClubSayH></NClubSayH><NClubSayA></NClubSayA>";
            }
            foreach (string str in info.Keys)
            {
                string pattern = "<" + str + ">[^<]*</" + str + ">";
                string replacement = string.Concat(new object[] { "<", str, ">", info[str], "</", str, ">" });
                oldXml = Regex.Replace(oldXml, pattern, replacement);
            }
            return oldXml;
        }
    }
}

