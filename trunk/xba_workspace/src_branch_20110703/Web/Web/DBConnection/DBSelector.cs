namespace Web.DBConnection
{
    using LoginParameter;
    using ServerManage;
    using System;

    public class DBSelector
    {
        public static string GetConnection(string strServerName)
        {
            strServerName = strServerName.ToLower();
            switch (strServerName)
            {
                case "root":
                    return DBLogin.ConnString(0);

                case "btp01":
                    return DBLogin.ConnString(ServerParameter.intGameCategory);

                case "main40":
                    return DBLogin.ConnString(40);
            }
            return null;
        }

        public static string GetConnection(int intGameCategory, string strServerName)
        {
            strServerName = strServerName.ToLower();
            switch (strServerName)
            {
                case "root":
                    return DBLogin.ConnString(0);

                case "btp01":
                    return DBLogin.ConnString(intGameCategory);
            }
            return null;
        }
    }
}

