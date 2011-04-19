namespace Web.Helper
{
    using System;
    using System.Data;
    using Web.DBData;

    public class SecretaryItem
    {
        public bool blnSex;
        public string strFace;
        public string strName;

        public SecretaryItem(int intUserID, bool blnSex)
        {
            DataRow secRowByUserID = BTPAccountManager.GetSecRowByUserID(intUserID);
            this.strName = secRowByUserID["SecName"].ToString().Trim();
            this.strFace = secRowByUserID["SecFace"].ToString().Trim();
            this.blnSex = blnSex;
        }

        public string GetImgFace()
        {
            if (this.blnSex)
            {
                this.strFace = "Boy/" + this.strFace;
            }
            else
            {
                this.strFace = "Girl/" + this.strFace;
            }
            return ("<img src='" + SessionItem.GetImageURL() + "Secretary/" + this.strFace + ".gif'  alt='" + this.strName + "' width='37' height='40'>");
        }
    }
}

