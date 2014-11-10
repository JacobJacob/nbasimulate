namespace Web.AutoCode
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBConnection;

    public class ArrangeSet : Page
    {
        protected Button btnSet;
        public string strMsg = "";

        private void btnSet_Click(object sender, EventArgs e)
        {
            string commandText = "UPDATE Arrange_Set SET Defense1=Defense1-5,Defense2=Defense2-5,Defense3=Defense3-5,Defense4=Defense4-5 WHERE Category=3";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            base.Response.Redirect("ArrangeSet.aspx");
        }

        private void InitializeComponent()
        {
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            int num;
            int num2;
            int num3;
            int num4;
            object strMsg;
            string commandText = "SELECT * FROM Arrange_Set WHERE Category=5 ORDER BY SetID";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            this.strMsg = this.strMsg + "<strong>职业比赛引擎战术参数</strong>：<br>int[,] intPDefs={";
            int count = table.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                num = (int) table.Rows[i]["Defense1"];
                num2 = (int) table.Rows[i]["Defense2"];
                num3 = (int) table.Rows[i]["Defense3"];
                num4 = (int) table.Rows[i]["Defense4"];
                int num7 = (int) table.Rows[i]["Defense5"];
                int num8 = (int) table.Rows[i]["Defense6"];
                if (i > 0)
                {
                    this.strMsg = this.strMsg + ",";
                }
                strMsg = this.strMsg;
                this.strMsg = string.Concat(new object[] { strMsg, "{", num, ",", num2, ",", num3, ",", num4, ",", num7, ",", num8, "}" });
                if (i < (count - 1))
                {
                    this.strMsg = this.strMsg + "<br>";
                }
            }
            this.strMsg = this.strMsg + "};<br><br><br>";
            commandText = "SELECT * FROM Arrange_Set WHERE Category=3 ORDER BY SetID";
            table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            this.strMsg = this.strMsg + "<strong>街球比赛引擎战术参数</strong>：<br>int[,] intPDefs={";
            count = table.Rows.Count;
            for (int j = 0; j < count; j++)
            {
                num = (int) table.Rows[j]["Defense1"];
                num2 = (int) table.Rows[j]["Defense2"];
                num3 = (int) table.Rows[j]["Defense3"];
                num4 = (int) table.Rows[j]["Defense4"];
                if (j > 0)
                {
                    this.strMsg = this.strMsg + ",";
                }
                strMsg = this.strMsg;
                this.strMsg = string.Concat(new object[] { strMsg, "{", num, ",", num2, ",", num3, ",", num4, "}" });
                if (j < (count - 1))
                {
                    this.strMsg = this.strMsg + "<br>";
                }
            }
            this.strMsg = this.strMsg + "};<br><br><br>";
        }
    }
}

