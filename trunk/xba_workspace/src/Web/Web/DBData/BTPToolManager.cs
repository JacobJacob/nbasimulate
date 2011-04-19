namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPToolManager
    {
        public static void AddTool(string strToolName, string strToolImage, string strToolIntroduction, int intCategory, int intCornCost, int intAmountInStock, int intTicketCategory)
        {
            string commandText = "Exec NewBTP.dbo.AddTools @strToolName,@strToolImage,@strToolIntroduction,@intCategory,@intCornCost,@intAmountInStock,@intTicketCategory";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strToolName", SqlDbType.NVarChar, 20), new SqlParameter("@strToolImage", SqlDbType.NVarChar, 20), new SqlParameter("@strToolIntroduction", SqlDbType.NVarChar, 50), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intCornCost", SqlDbType.Int, 4), new SqlParameter("@intAmountInStock", SqlDbType.Int, 4), new SqlParameter("@intTicketCategory", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = strToolName;
            commandParameters[1].Value = strToolImage;
            commandParameters[2].Value = strToolIntroduction;
            commandParameters[3].Value = intCategory;
            commandParameters[4].Value = intCornCost;
            commandParameters[5].Value = intAmountInStock;
            commandParameters[6].Value = intTicketCategory;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetToolCount()
        {
            string commandText = "Exec NewBTP.dbo.GetToolCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetToolList(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetToolList ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetToolRowByID(int intToolID)
        {
            string commandText = "Exec NewBTP.dbo.GetToolRowByID " + intToolID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetToolTable()
        {
            string commandText = "Exec NewBTP.dbo.GetToolTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetToolTableInSale()
        {
            string commandText = "Exec NewBTP.dbo.GetToolTableInSale";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}

