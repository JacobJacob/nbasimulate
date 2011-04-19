namespace Web.MyControls
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class JumpBoardInfo : UserControl
    {
        public string strBoardID;
        public string strBoardInfo;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetBoardInfo();
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.strBoardInfo);
        }

        private void SetBoardInfo()
        {
            string str;
            DataRow newsByBoardID = ROOTBoardManager.GetNewsByBoardID();
            if (newsByBoardID == null)
            {
                str = " 暂时没有公告信息！";
            }
            else
            {
                int num = (int) newsByBoardID["TopicID"];
                string str2 = newsByBoardID["Title"].ToString().Trim();
                str = string.Concat(new object[] { " <a href='FrameTopic.aspx?TopicID=", num, "&BoardID=001001&Page=1'>", str2, "</a>" });
            }
            this.strBoardInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td width='70%'><img src='" + SessionItem.GetImageURL() + "Forum/face1.gif' border=0 align=absmiddle>" + str + "</td><td width='30%' height='35'>论坛跳转：&nbsp;&nbsp;" + BoardItem.GetBoardJump(this.strBoardID) + "</td></tr></table>";
        }
    }
}

