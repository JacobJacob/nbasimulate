using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace Web.Helper
{
	public class MainXmlHelper
	{
        public static String GetNewMainXml(String oldXml, Hashtable info)
        {

            if(oldXml==null)
            {
                oldXml =  "<ScoreH></ScoreH><ScoreA>54</ScoreA><ClubNameH></ClubNameH>" + 
                 "<ClubNameA></ClubNameA><ClubLogoH></ClubLogoH><ClubLogoA>" +
                 "</ClubLogoA><ClubSayH></ClubSayH><ClubSayA></ClubSayA>" +
                 "<Tickets></Tickets><Income></Income><MVPName></MVPName><MVPStas>" +
                 "</MVPStas><NClubNameH></NClubNameH><NClubNameA></NClubNameA><NClubLogoH>" +
                 "</NClubLogoH><NClubLogoA></NClubLogoA><NClubSayH></NClubSayH><NClubSayA></NClubSayA>";
            }
            foreach(String key in info.Keys)
            {
               string pattern = "<" + key + ">[^<]*</" + key + ">";
               string newValue = "<" + key + ">" + info[key] + "</" + key + ">";
               oldXml = Regex.Replace(oldXml, pattern, newValue);

            }

            return oldXml;

        }



	}
}
