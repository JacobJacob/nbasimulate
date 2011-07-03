namespace Web.Helper
{
    using System;
    using Web.DBData;

    public class UserGuide
    {
        public static void CompleteGuide(int intUserID, string strGuideCode, int intGuideIndex)
        {
            Cuter cuter = new Cuter(strGuideCode);
            if (cuter.GetCuter(intGuideIndex) != "2")
            {
                cuter.SetCuter(intGuideIndex, "2");
                BTPAccountManager.SetGuideCode(intUserID, cuter.ToString());
                DTOnlineManager.UpdateGuideCode(intUserID, cuter.ToString());
            }
        }

        public static void GuideUser(int intUserID, string strGuideCode)
        {
            if ((strGuideCode.IndexOf("0") != -1) && (strGuideCode.IndexOf("1") == -1))
            {
                Cuter cuter = new Cuter(strGuideCode);
                int index = cuter.GetIndex("0");
                cuter.SetCuter(index, "1");
                strGuideCode = cuter.ToString();
                BTPAccountManager.SetGuideCode(intUserID, strGuideCode);
                DTOnlineManager.UpdateGuideCode(intUserID, strGuideCode);
                BTPMessageManager.AddRookieMessage(intUserID, index);
            }
        }

        public static void NoteCompleteGuide(int intUserID, string strGuideCode, int intGuideIndex)
        {
            Cuter cuter = new Cuter(strGuideCode);
            if (cuter.GetCuter(intGuideIndex) != "1")
            {
                cuter.SetCuter(intGuideIndex, "1");
                BTPAccountManager.SetGuideCode(intUserID, cuter.ToString());
                DTOnlineManager.UpdateGuideCode(intUserID, cuter.ToString());
            }
        }

        public static string NoteGetGuide(string strGuideCode, int intGuideIndex)
        {
            Cuter cuter = new Cuter(strGuideCode);
            return cuter.GetCuter(intGuideIndex);
        }
    }
}

