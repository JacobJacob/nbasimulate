namespace Web.Helper
{
    using System;

    [Serializable]
    public class DevCupData
    {
        public DateTime datEndTime;
        public int intCreateCharge;
        public int intCupSize;
        public int intHortationAll;
        public int intMedalCharge;
        public int intRegCharge;
        public int intRegClub;
        public int intUnionID;
        public string strCupLadder;
        public string strCupName;
        public string strDevCupIntro;
        public string strLogo;
        public string strPassword;
        public string strRequirementXML;
        public string strRewardXML;

        public DevCupData(int intUnionID, int intRegClub, string strCupName, string strPassword, string strDevCupIntro, int intRegCharge, string strLogo, string strRequirementXML, string strRewardXML, int intCupSize, DateTime datEndTime, int intCreateCharge, int intMedalCharge, int intHortationAll, string strCupLadder)
        {
            this.strCupName = strCupName;
            this.strPassword = strPassword;
            this.strDevCupIntro = strDevCupIntro;
            this.strLogo = strLogo;
            this.strRequirementXML = strRequirementXML;
            this.strRewardXML = strRewardXML;
            this.strCupLadder = strCupLadder;
            this.intUnionID = intUnionID;
            this.intRegClub = intRegClub;
            this.intRegCharge = intRegCharge;
            this.intCupSize = intCupSize;
            this.intCreateCharge = intCreateCharge;
            this.intMedalCharge = intMedalCharge;
            this.intHortationAll = intHortationAll;
            this.datEndTime = datEndTime;
        }
    }
}

