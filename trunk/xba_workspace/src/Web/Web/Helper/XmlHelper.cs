namespace Web.Helper
{
    using System;
    using System.Data;

    public class XmlHelper
    {
        public static string GetOneScript(DataRow drScript)
        {
            string str = drScript["Time"].ToString();
            string str2 = drScript["Content"].ToString();
            string str3 = drScript["Score"].ToString();
            if (str != "")
            {
                return ("[" + str + "]&nbsp;" + str2 + "&nbsp;[" + str3 + "]<br>");
            }
            if (str3 == "")
            {
                return ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str2 + "<br>");
            }
            return ("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str2 + "&nbsp;[" + str3 + "]<br>");
        }

        public static string GetQuarterArrange(TOnlineReportData vd)
        {
            string str;
            DataRow row = GetRow(vd.ds.Tables["Club"], "Type=1", "");
            string str2 = row["ClubName"].ToString();
            string str3 = row["ClubID"].ToString();
            DataRow row2 = GetRow(vd.ds.Tables["Club"], "Type=2", "");
            string str4 = row2["ClubName"].ToString();
            string str5 = row2["ClubID"].ToString();
            DataRow row3 = GetRow(vd.ds.Tables["Arrange"], string.Concat(new object[] { "QuarterID=", vd.intQNum, " AND ClubID=", str3 }), "");
            DataRow row4 = GetRow(vd.ds.Tables["Arrange"], string.Concat(new object[] { "QuarterID=", vd.intQNum, " AND ClubID=", str5 }), "");
            DataView view = GetView(vd.ds.Tables["Player"], "ArrangeID=" + row3["ArrangeID"].ToString(), "");
            DataView view2 = GetView(vd.ds.Tables["Player"], "ArrangeID=" + row4["ArrangeID"].ToString(), "");
            string str6 = "<table width=\"534\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">    <tr align=\"center\">        <td height=\"25\" colspan=\"2\"><strong>" + MatchItem.GetQName(vd.intQNum, 5) + "</strong></td>    </tr>    <tr bgcolor=\"#FCC6A4\">        <td height=\"25\" style=\"padding-left:4px;\">" + str2 + "&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row3["Offense"]) + "&nbsp;" + MatchItem.GetVDefName((int) row3["Defense"]) + "</td>        <td style=\"padding-left:4px;\">" + str4 + "&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row4["Offense"]) + "&nbsp;" + MatchItem.GetVDefName((int) row4["Defense"]) + "</td>    </tr>    <tr>        <td width=\"50%\"><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">        <tr align=\"center\">            <td width=\"10%\" height=\"25\">&nbsp;</td>            <td width=\"30%\">球员</td>            <td width=\"10%\">号</td>            <td width=\"12%\">身高</td>            <td width=\"12%\">体重</td>            <td width=\"13%\">体力</td>            <td width=\"13%\">综合</td>        </tr>";
            for (int i = 0; i < view.Count; i++)
            {
                str = str6;
                str6 = str + "<tr align=\"center\">\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition((int) view[i].Row["Pos"]) + "</td>\t\t\t<td>" + view[i].Row["Name"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Number"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Height"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Weight"].ToString() + "</td>";
                try
                {
                    str6 = str6 + "\t\t\t<td>" + view[i].Row["Power"].ToString() + "</td>";
                }
                catch
                {
                    str6 = str6 + "\t\t\t<td>--</td>";
                }
                str6 = str6 + "\t\t\t<td>" + view[i].Row["Ability"].ToString() + "</td>\t\t</tr>";
            }
            str6 = str6 + "</table></td>        <td width=\"50%\"><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">        <tr align=\"center\">            <td width=\"10%\" height=\"25\">&nbsp;</td>            <td width=\"30%\">球员</td>            <td width=\"10%\">号</td>            <td width=\"12%\">身高</td>            <td width=\"12%\">体重</td>            <td width=\"13%\">体力</td>            <td width=\"13%\">综合</td>        </tr>";
            for (int j = 0; j < view2.Count; j++)
            {
                str = str6;
                str6 = str + "<tr align=\"center\">\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition((int) view2[j].Row["Pos"]) + "</td>\t\t\t<td>" + view2[j].Row["Name"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Number"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Height"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Weight"].ToString() + "</td>";
                try
                {
                    str6 = str6 + "\t\t\t<td>" + view2[j].Row["Power"].ToString() + "</td>";
                }
                catch
                {
                    str6 = str6 + "\t\t\t<td>--</td>";
                }
                str6 = str6 + "\t\t\t<td>" + view2[j].Row["Ability"].ToString() + "</td>\t\t</tr>";
            }
            return (str6 + "</table></td>    </tr></table><br>");
        }

        public static string GetQuarterArrange(VOnlineReportData vd)
        {
            string str;
            DataRow row = GetRow(vd.ds.Tables["Club"], "Type=1", "");
            string str2 = row["ClubName"].ToString();
            string str3 = row["ClubID"].ToString();
            DataRow row2 = GetRow(vd.ds.Tables["Club"], "Type=2", "");
            string str4 = row2["ClubName"].ToString();
            string str5 = row2["ClubID"].ToString();
            DataRow row3 = GetRow(vd.ds.Tables["Arrange"], string.Concat(new object[] { "QuarterID=", vd.intQNum, " AND ClubID=", str3 }), "");
            DataRow row4 = GetRow(vd.ds.Tables["Arrange"], string.Concat(new object[] { "QuarterID=", vd.intQNum, " AND ClubID=", str5 }), "");
            DataView view = GetView(vd.ds.Tables["Player"], "ArrangeID=" + row3["ArrangeID"].ToString(), "");
            DataView view2 = GetView(vd.ds.Tables["Player"], "ArrangeID=" + row4["ArrangeID"].ToString(), "");
            string str6 = "<table width=\"534\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">    <tr align=\"center\">        <td height=\"25\" colspan=\"2\"><strong>" + MatchItem.GetQName(vd.intQNum, 5) + "</strong></td>    </tr>    <tr bgcolor=\"#FCC6A4\">        <td height=\"25\" style=\"padding-left:4px;\">" + str2 + "&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row3["Offense"]) + "&nbsp;" + MatchItem.GetVDefName((int) row3["Defense"]) + "</td>        <td style=\"padding-left:4px;\">" + str4 + "&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row4["Offense"]) + "&nbsp;" + MatchItem.GetVDefName((int) row4["Defense"]) + "</td>    </tr>    <tr>        <td width=\"50%\"><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">        <tr align=\"center\">            <td width=\"10%\" height=\"25\">&nbsp;</td>            <td width=\"30%\">球员</td>            <td width=\"10%\">号</td>            <td width=\"12%\">身高</td>            <td width=\"12%\">体重</td>            <td width=\"13%\">体力</td>            <td width=\"13%\">综合</td>        </tr>";
            for (int i = 0; i < view.Count; i++)
            {
                str = str6;
                str6 = str + "<tr align=\"center\">\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition((int) view[i].Row["Pos"]) + "</td>\t\t\t<td>" + view[i].Row["Name"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Number"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Height"].ToString() + "</td>\t\t\t<td>" + view[i].Row["Weight"].ToString() + "</td>";
                try
                {
                    str6 = str6 + "\t\t\t<td>" + view[i].Row["Power"].ToString() + "</td>";
                }
                catch
                {
                    str6 = str6 + "\t\t\t<td>--</td>";
                }
                str6 = str6 + "\t\t\t<td>" + view[i].Row["Ability"].ToString() + "</td>\t\t</tr>";
            }
            str6 = str6 + "</table></td>        <td width=\"50%\"><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">        <tr align=\"center\">            <td width=\"10%\" height=\"25\">&nbsp;</td>            <td width=\"30%\">球员</td>            <td width=\"10%\">号</td>            <td width=\"12%\">身高</td>            <td width=\"12%\">体重</td>            <td width=\"13%\">体力</td>            <td width=\"13%\">综合</td>        </tr>";
            for (int j = 0; j < view2.Count; j++)
            {
                str = str6;
                str6 = str + "<tr align=\"center\">\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition((int) view2[j].Row["Pos"]) + "</td>\t\t\t<td>" + view2[j].Row["Name"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Number"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Height"].ToString() + "</td>\t\t\t<td>" + view2[j].Row["Weight"].ToString() + "</td>";
                try
                {
                    str6 = str6 + "\t\t\t<td>" + view2[j].Row["Power"].ToString() + "</td>";
                }
                catch
                {
                    str6 = str6 + "\t\t\t<td>--</td>";
                }
                str6 = str6 + "\t\t\t<td>" + view2[j].Row["Ability"].ToString() + "</td>\t\t</tr>";
            }
            return (str6 + "</table></td>    </tr></table><br>");
        }

        public static DataRow GetRow(DataTable dt, string RowFilter, string Sort)
        {
            if (dt == null)
            {
                return null;
            }
            DataView view = new DataView(dt, RowFilter, Sort, DataViewRowState.CurrentRows);
            return view[0].Row;
        }

        public static DataView GetView(DataTable dt, string RowFilter, string Sort)
        {
            if (dt == null)
            {
                return null;
            }
            return new DataView(dt, RowFilter, Sort, DataViewRowState.CurrentRows);
        }
    }
}

