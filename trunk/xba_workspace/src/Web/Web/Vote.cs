namespace Web
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Vote : Page
    {
        private int intTopicID;
        private int intUserID;
        private string strBoardID;
        private string strParameter;

        private void InitializeComponent()
        {
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
                this.strParameter = (string) SessionItem.GetRequest("Parameter", 1);
                this.strBoardID = (string) SessionItem.GetRequest("BoardID", 1);
                this.intTopicID = (int) SessionItem.GetRequest("TopicID", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            DataRow topicRowByID = ROOTTopicManager.GetTopicRowByID(this.intTopicID);
            string line = topicRowByID["Content"].ToString();
            if (!((bool) topicRowByID["IsVote"]))
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                TagReader reader = new TagReader(line);
                this.strParameter = this.strParameter.Replace("|", ",");
                Cuter cuter = new Cuter(this.strParameter);
                ArrayList items = reader.GetItems("Vote");
                string strContent = "";
                for (int i = 0; i < items.Count; i++)
                {
                    strContent = strContent + "<Vote>";
                    string str2 = (string) items[i];
                    TagReader reader2 = new TagReader(str2);
                    string str3 = (string) reader2.GetItems("Title")[0];
                    object obj2 = line;
                    line = string.Concat(new object[] { obj2, "<b>", i + 1, ". ", str3, "</b><br>" });
                    ArrayList list2 = reader2.GetItems("Item");
                    strContent = strContent + "<Title>" + str3 + "</Title>";
                    for (int j = 0; j < list2.Count; j++)
                    {
                        strContent = strContent + "<Item>";
                        string str5 = (string) list2[j];
                        TagReader reader3 = new TagReader(str5);
                        string str4 = (string) reader3.GetItems("Content")[0];
                        strContent = strContent + "<Content>" + str4 + "</Content>";
                        int num = Convert.ToInt32(reader3.GetItems("VoteCount")[0]);
                        if (cuter.GetCuter(i) == BoardItem.GetRadioID(i, j))
                        {
                            num++;
                        }
                        obj2 = strContent;
                        strContent = string.Concat(new object[] { obj2, "<VoteCount>", num, "</VoteCount>" }) + "</Item>";
                    }
                    strContent = strContent + "</Vote>";
                }
                strContent = strContent + "<Introduction>" + ((string) reader.GetItems("Introduction")[0]) + "</Introduction>";
                if (ROOTTopicManager.HasVote(this.intUserID, this.intTopicID))
                {
                    base.Response.Redirect("Report.aspx?Parameter=4010");
                }
                else
                {
                    ROOTTopicManager.AddReply(this.strBoardID, this.intUserID, "", "", "", strContent, this.intTopicID, "", "", true);
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=4011!BoardID.", this.strBoardID, "^TopicID.", this.intTopicID, "^Page.1" }));
                }
            }
        }
    }
}

