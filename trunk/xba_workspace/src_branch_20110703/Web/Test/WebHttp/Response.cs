namespace Test.WebHttp
{
    using System;
    using System.Web.UI;

    public class Response : Page
    {
        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        public string OName(string name)
        {
            return name;
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string name = null;
            string str2 = null;
            if (base.Request.QueryString["name"] != null)
            {
                name = base.Request.QueryString["name"].Trim();
                str2 = this.OName(name);
                base.Response.Output.Write(str2);
                base.Response.End();
                base.Response.Output.Write(str2);
                base.Response.End();
                base.Response.Output.Write(str2);
                base.Response.End();
            }
            else
            {
                base.Response.Output.Write("ERROR!");
                base.Response.End();
            }
        }
    }
}

