namespace Web.Helper
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class HttpRequestItem
    {
        public static string GetString(string strRequestURL)
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
            requestUriString = strRequestURL;
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
            return str;
        }
    }
}

