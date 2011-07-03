namespace AjaxPro
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    public class AjaxBitmapHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(context.Request.FilePath);
            AjaxBitmap bitmap = null;
            try
            {
                Guid guid = new Guid(fileNameWithoutExtension);
                bitmap = context.Cache[fileNameWithoutExtension] as AjaxBitmap;
            }
            catch (Exception)
            {
            }
            if ((bitmap == null) || (bitmap.bmp == null))
            {
                bitmap.bmp = new Bitmap(10, 20);
                Graphics graphics = Graphics.FromImage(bitmap.bmp);
                graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, 10, 20);
                graphics.DrawString("?", new Font("Arial", 10f), new SolidBrush(Color.Red), (float) 0f, (float) 0f);
            }
            context.Response.ContentType = bitmap.mimeType;
            switch (bitmap.mimeType.ToLower())
            {
                case "image/gif":
                    bitmap.bmp.Save(context.Response.OutputStream, ImageFormat.Gif);
                    break;

                case "image/png":
                    bitmap.bmp.Save(context.Response.OutputStream, ImageFormat.Png);
                    break;

                case "image/jpeg":
                {
                    ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, bitmap.quality);
                    bitmap.bmp.Save(context.Response.OutputStream, imageEncoders[1], encoderParams);
                    break;
                }
                default:
                    throw new NotSupportedException("'" + bitmap.mimeType + "' is not supported.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

