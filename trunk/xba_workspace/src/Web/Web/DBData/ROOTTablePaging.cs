namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class ROOTTablePaging
    {
        public static string GetSQLString(string strIn)
        {
            if (strIn == "NULL")
            {
                return "NULL";
            }
            return ("'" + strIn + "'");
        }

        public static DataTable PagingRowCount(string strTables, string strPK, string strSort, int intPageNumber, int intPageSize, string strFields, string strFilter, string strGroup, int intIsCount)
        {
            strTables = GetSQLString(strTables);
            strPK = GetSQLString(strPK);
            strSort = GetSQLString(strSort);
            strFields = GetSQLString(strFields);
            strFilter = GetSQLString(strFilter);
            strGroup = GetSQLString(strGroup);
            string commandText = string.Concat(new object[] { 
                "Exec ROOT_Data.dbo.PagingRowCount ", strTables, ",", strPK, ",", strSort, ",", intPageNumber, ",", intPageSize, ",", strFields, ",", strFilter, ",", strGroup, 
                ",", intIsCount
             });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int PagingRowCountNum(string strTables, string strPK, string strFilter)
        {
            strTables = GetSQLString(strTables);
            strPK = GetSQLString(strPK);
            strFilter = GetSQLString(strFilter);
            string commandText = "Exec ROOT_Data.dbo.PagingRowCount " + strTables + "," + strPK + ",NULL,0,0,'*'," + strFilter + ",NULL,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}

