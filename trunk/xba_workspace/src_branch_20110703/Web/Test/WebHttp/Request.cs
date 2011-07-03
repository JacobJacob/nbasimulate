namespace Test.WebHttp
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Request : Page
    {
        protected Button btnSubmit;
        protected Label lblResponseDesc;
        protected TextBox txtRequestDesc;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            Encoding encoding = null;
            StreamReader reader = null;
            char[] buffer = null;
            int length = 0;
            string str = null;
            string requestUriString = null;
            requestUriString = "http://newgame.17173.com/count.php?work=codepass&id=488&number=1385&code=1385";
            request = (HttpWebRequest) WebRequest.Create(requestUriString);
            response = (HttpWebResponse) request.GetResponse();
            responseStream = response.GetResponseStream();
            encoding = Encoding.GetEncoding("GB2312");
            reader = new StreamReader(responseStream, encoding);
            buffer = new char[0x100];
            for (length = reader.Read(buffer, 0, 0x100); length > 0; length = reader.Read(buffer, 0, 0x100))
            {
                str = new string(buffer, 0, length);
            }
            response.Close();
            reader.Close();
            this.lblResponseDesc.Text = str.Replace("<", "..").Replace(">", "..");
        }

        private void InitializeComponent()
        {
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
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

