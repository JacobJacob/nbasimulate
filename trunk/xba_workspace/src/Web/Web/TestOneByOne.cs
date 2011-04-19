namespace Web
{
    using System;
    using System.Web.UI;
    using Web.OneByOneEngine;

    public class TestOneByOne : Page
    {
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
            Match match = new Match(0x691e8L, 0x691e9L);
            match.Run();
            base.Response.Write(match.sbScript);
        }
    }
}

