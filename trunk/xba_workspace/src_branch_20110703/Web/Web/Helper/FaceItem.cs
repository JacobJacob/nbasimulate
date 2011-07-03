namespace Web.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class FaceItem
    {
        public static void CreateFace(string strFacePath, string strDiskURL, string strFace)
        {
            string[] strArray = strFace.Split(new char[] { '|' });
            string[] strArray2 = new string[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray2[i] = string.Concat(new object[] { strFacePath, i, @"\", strArray[i], ".png" });
            }
            Bitmap image = new Bitmap(strArray2[0]);
            Graphics graphics = Graphics.FromImage(image);
            for (int j = 1; j < strArray.Length; j++)
            {
                Bitmap bitmap2 = new Bitmap(strArray2[j]);
                graphics.DrawImage(bitmap2, 0, 0);
            }
            string path = strDiskURL + "Face.png";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            image.Save(path, ImageFormat.Png);
        }
    }
}

