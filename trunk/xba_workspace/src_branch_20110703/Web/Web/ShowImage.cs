namespace Web
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    public class ShowImage : Page
    {
        public MemoryStream GetPic(string strParameter)
        {
            string[] strArray = strParameter.Split(new char[] { '|' });
            HttpContext current = HttpContext.Current;
            string[] strArray2 = new string[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray2[i] = current.Server.MapPath(strArray[i]);
            }
            Bitmap image = new Bitmap(strArray2[0]);
            Graphics graphics = Graphics.FromImage(image);
            for (int j = 1; j < strArray.Length; j++)
            {
                Bitmap bitmap2;
                try
                {
                    bitmap2 = new Bitmap(strArray2[j]);
                }
                catch
                {
                    bitmap2 = new Bitmap(strArray2[j]);
                }
                graphics.DrawImage(bitmap2, 0, 0);
            }
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            return stream;
        }

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
            base.Response.ClearContent();
            base.Response.ContentType = "image/Png";
            UnicodeEncoding encoding = new UnicodeEncoding();
            base.Response.ContentEncoding = encoding;
            string strParameter = base.Request.QueryString["Parameter"].ToString();
            base.Response.BinaryWrite(this.GetPic(strParameter).ToArray());
        }
    }
}

