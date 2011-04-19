namespace AjaxPro
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal class MD5Helper
    {
        internal static string GetHash(string data)
        {
            return GetHash(Encoding.Default.GetBytes(data));
        }

        internal static string GetHash(byte[] data)
        {
            string str = "";
            string[] strArray = new string[0x10];
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(data);
            for (int i = 0; i < buffer.Length; i++)
            {
                strArray[i] = buffer[i].ToString("x");
                str = str + strArray[i];
            }
            return str;
        }
    }
}

