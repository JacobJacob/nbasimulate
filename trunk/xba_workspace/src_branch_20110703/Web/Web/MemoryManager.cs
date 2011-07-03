namespace Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.Helper;

    public class MemoryManager : Page
    {
        protected Button btnGCCheck;
        protected Button btnGCOK;
        private string strPassword = "dryard";
        protected TextBox tbPassword;

        private void btnGCCheck_Click(object sender, EventArgs e)
        {
            if (this.tbPassword.Text == this.strPassword)
            {
                base.Response.Write(StringItem.GetEngNum(Convert.ToInt32(GC.GetTotalMemory(false))) + "Byte");
            }
            else
            {
                base.Response.Write("您的验证码错误！");
            }
        }

        private void btnGCOK_Click(object sender, EventArgs e)
        {
            if (this.tbPassword.Text == this.strPassword)
            {
                GC.Collect();
                base.Response.Write("垃圾回收成功！");
            }
            else
            {
                base.Response.Write("您的验证码错误！");
            }
        }

        private void InitializeComponent()
        {
            this.btnGCOK.Click += new EventHandler(this.btnGCOK_Click);
            this.btnGCCheck.Click += new EventHandler(this.btnGCCheck_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

