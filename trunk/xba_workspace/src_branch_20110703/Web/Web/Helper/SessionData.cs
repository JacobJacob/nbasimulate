namespace Web.Helper
{
    using System;
    using System.Data;
    using Web.DBData;

    public class SessionData
    {
        public int intClubID3;
        public int intClubID5;
        public int intUserID;
        public string strClubName;
        public string strNickName;
        public string strUserName;

        public SessionData(int intUserID)
        {
            this.intUserID = intUserID;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                this.intClubID3 = (int) accountRowByUserID["ClubID3"];
                this.intClubID5 = (int) accountRowByUserID["ClubID5"];
                this.strUserName = accountRowByUserID["UserName"].ToString();
                this.strNickName = accountRowByUserID["NickName"].ToString();
                this.strClubName = accountRowByUserID["ClubName"].ToString();
            }
            else
            {
                this.intClubID3 = 0;
                this.intClubID5 = 0;
                this.strUserName = "";
                this.strNickName = "";
                this.strClubName = "";
            }
        }
    }
}

