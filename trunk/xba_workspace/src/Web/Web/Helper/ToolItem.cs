namespace Web.Helper
{
    using System;
    using System.Data;
    using Web.DBData;

    public class ToolItem
    {
        public static int HasTool(int intUserID, int intCategory, int intTicketCategory)
        {
            DataRow row = BTPToolLinkManager.GetToolByUserIDTCategory(intUserID, intCategory, intTicketCategory);
            if (row == null)
            {
                return 0;
            }
            return (int) row["Amount"];
        }

        public static int HasWealthTool(int intUserID, int intCategory, int intTicketCategory)
        {
            DataRow row = BTPToolLinkManager.GetWealthToolByUserID(intUserID, intCategory, intTicketCategory);
            if (row == null)
            {
                return 0;
            }
            return (int) row["Amount"];
        }
    }
}

