namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class AfreshArrange : Page
    {
        protected ImageButton btnOK;
        public int intAllMoney = 0;
        private int intCategory;
        private int intClubID;
        private int[] intDefs = new int[6];
        private int[] intOffs = new int[6];
        private int intUserID;
        public long lngAllPiont;
        public StringBuilder sb = new StringBuilder("");
        public string strAllMoney;
        public string strBtnOKH = "";
        private string strNickName;
        public string strPageIntro;
        protected HtmlTable tblArrangeLvl;
        protected HtmlInputHidden VDArea212H;
        protected HtmlInputHidden VDArea23H;
        protected HtmlInputHidden VDArea32H;
        protected HtmlInputHidden VDOneH;
        protected HtmlInputHidden VDOneInsideH;
        protected HtmlInputHidden VDOneOutsideH;
        protected HtmlInputHidden VOAllH;
        protected HtmlInputHidden VOBlockH;
        protected HtmlInputHidden VOCHelpH;
        protected HtmlInputHidden VOInsideH;
        protected HtmlInputHidden VOOutsideH;
        protected HtmlInputHidden VOSpeedH;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (BTPArrangeLvlManager.GetArrange5(this.intUserID) != null)
            {
                this.SetArrangePoint(this.intOffs, this.intDefs);
                int num = BTPArrangeLvlManager.UpdateArrangeLvlByUserID(this.intUserID, this.intOffs, this.intDefs);
                base.Response.Redirect("SecretaryPage.aspx?Type=AFRESHARRANGE&Status=" + num);
            }
        }

        private void GetArrangeList()
        {
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            if (parameterRow != null)
            {
                this.intAllMoney = (int) parameterRow["AfreshArrangeWealth"];
                int num = (int) parameterRow["AfreshArrangeLowWealth"];
                if (num < this.intAllMoney)
                {
                    this.strAllMoney = string.Concat(new object[] { this.intAllMoney, "游戏币/次 优惠价：<font color='red'>", num, "</font>游戏币/次" });
                }
                else
                {
                    this.strAllMoney = this.intAllMoney + "游戏币/次";
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            this.lngAllPiont = BTPToolLinkManager.AfreshArrangeLvlByUserID(this.intUserID);
            int intLevel = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = 0;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>内线强攻</td>");
            this.sb.Append("<td id='VOInside'>" + intLevel + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOInsideP'>", num14, "/", BTPArrangeLvlManager.Get5ArrangeNeed(intLevel), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOInside')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOInside')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>中锋策应</td>");
            this.sb.Append("<td id='VOCHelp'>" + num3 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOCHelpP'>", num15, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num3), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOCHelp')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOCHelp')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>外线投篮</td>");
            this.sb.Append("<td id='VOOutside'>" + num4 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOOutsideP'>", num16, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num4), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOOutside')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOOutside')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>快速进攻</td>");
            this.sb.Append("<td id='VOSpeed'>" + num5 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOSpeedP'>", num17, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num5), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOSpeed')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOSpeed')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>整体配合</td>");
            this.sb.Append("<td id='VOAll'>" + num6 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOAllP'>", num18, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num6), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOAll')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOAll')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#00cc00'>进攻</font></td>");
            this.sb.Append("<td>掩护挡拆</td>");
            this.sb.Append("<td id='VOBlock'>" + num7 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VOBlockP'>", num19, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num7), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VOBlock')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VOBlock')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>2-3联防</td>");
            this.sb.Append("<td id='VDArea23'>" + num8 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDArea23P'>", num20, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num8), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDArea23')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDArea23')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>3-2联防</td>");
            this.sb.Append("<td id='VDArea32'>" + num9 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDArea32P'>", num21, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num9), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDArea32')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDArea32')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>2-1-2联防</td>");
            this.sb.Append("<td id='VDArea212'>" + num10 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDArea212P'>", num22, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num10), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDArea212')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDArea212')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>盯人防守</td>");
            this.sb.Append("<td id='VDOne'>" + num11 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDOneP'>", num23, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num11), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDOne')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDOne')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>盯人内线</td>");
            this.sb.Append("<td id='VDOneInside'>" + num12 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDOneInsideP'>", num24, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num12), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDOneInside')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDOneInside')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td></td>");
            this.sb.Append("<td height='25'><font color='#FF0000'>防守</font></td>");
            this.sb.Append("<td>盯人外线</td>");
            this.sb.Append("<td id='VDOneOutside'>" + num13 + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td id='VDOneOutsideP'>", num25, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num13), "</td>" }));
            this.sb.Append("<td><img onclick=\"AddLvl('VDOneOutside')\" style=\"cursor:pointer\"  width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "addz.gif\"> <img onclick=\"DecLvl('VDOneOutside')\" style=\"cursor:pointer\" width=\"19\" height=\"20\" src=\"" + SessionItem.GetImageURL() + "dcz.gif\"></td>");
            this.sb.Append("</tr>");
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                if (onlineRowByUserID != null)
                {
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    this.intClubID = (int) onlineRowByUserID["ClubID5"];
                    this.intCategory = (int) onlineRowByUserID["Category"];
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.strBtnOKH = "<img id=\"btnOKH\" src=\"" + SessionItem.GetImageURL() + "button_11h.gif\" Width=\"40\" Height=\"24\" >";
                    this.strPageIntro = "<a href='VArrange.aspx?Type=0'><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_C_01.GIF' border='0' height='24' width='99'></a><a href='VArrange.aspx?Type=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_C_02.GIF' border='0' height='24' width='86'></a><a href='VArrange.aspx?Type=2'><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_C_03.GIF' border='0' height='24' width='86'></a><a href='VArrange.aspx?Type=3'><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_C_04.GIF' border='0' height='24' width='86'></a><a href='VArrange.aspx?Type=4'><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_C_05.GIF' border='0' height='24' width='86'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_06.GIF' border='0' height='24' width='72'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF'align='absmiddle' border='0' height='24' width='19'></a>";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=12");
                    return;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.GetArrangeList();
        }

        private void SetArrangePoint(int[] intOffs, int[] intDefs)
        {
            intOffs[0] = Convert.ToInt32(this.VOInsideH.Value);
            intOffs[1] = Convert.ToInt32(this.VOCHelpH.Value);
            intOffs[2] = Convert.ToInt32(this.VOOutsideH.Value);
            intOffs[3] = Convert.ToInt32(this.VOSpeedH.Value);
            intOffs[4] = Convert.ToInt32(this.VOAllH.Value);
            intOffs[5] = Convert.ToInt32(this.VOBlockH.Value);
            intDefs[0] = Convert.ToInt32(this.VDArea23H.Value);
            intDefs[1] = Convert.ToInt32(this.VDArea32H.Value);
            intDefs[2] = Convert.ToInt32(this.VDArea212H.Value);
            intDefs[3] = Convert.ToInt32(this.VDOneH.Value);
            intDefs[4] = Convert.ToInt32(this.VDOneInsideH.Value);
            intDefs[5] = Convert.ToInt32(this.VDOneOutsideH.Value);
        }
    }
}

