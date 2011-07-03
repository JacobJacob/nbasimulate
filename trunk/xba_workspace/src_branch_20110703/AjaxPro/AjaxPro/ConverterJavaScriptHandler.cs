namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;
    using System.Web.Caching;

    internal class ConverterJavaScriptHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fullName;
            IJavaScriptConverter current;
            string etag = context.Request.Headers["If-None-Match"];
            string str2 = context.Request.Headers["If-Modified-Since"];
            if (context.Cache["AjaxPro.converter"] != null)
            {
                CacheInfo info = (CacheInfo) context.Cache["AjaxPro.converter"];
                if ((etag != null) && (etag == info.ETag))
                {
                    context.Response.StatusCode = 0x130;
                    return;
                }
                if (str2 != null)
                {
                    if (str2.IndexOf(";") > 0)
                    {
                        str2 = str2.Split(new char[] { ';' })[0];
                    }
                    try
                    {
                        if (DateTime.Compare(Convert.ToDateTime(str2.ToString()).ToUniversalTime(), info.LastModified.ToUniversalTime()) >= 0)
                        {
                            context.Response.StatusCode = 0x130;
                            return;
                        }
                    }
                    catch (FormatException)
                    {
                        if (context.Trace.IsEnabled)
                        {
                            context.Trace.Write("AjaxPro", "The header value for If-Modified-Since = " + str2 + " could not be converted to a System.DateTime.");
                        }
                    }
                }
            }
            etag = MD5Helper.GetHash(Encoding.Default.GetBytes("converter"));
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            context.Response.AddHeader("Content-Type", "application/x-javascript");
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetETag(etag);
            context.Response.Cache.SetLastModified(date);
            if ((Utility.Settings != null) && (Utility.Settings.Encryption != null))
            {
                context.Response.Write(Utility.Settings.Encryption.CryptProvider.ClientScript);
                context.Response.Write("\r\n");
                context.Response.Write(Utility.Settings.Encryption.KeyProvider.ClientScript);
                context.Response.Write("\r\n");
            }
            StringCollection strings = new StringCollection();
            IEnumerator enumerator = Utility.Settings.SerializableConverters.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                current = (IJavaScriptConverter) enumerator.Current;
                fullName = current.GetType().FullName;
                if (!strings.Contains(fullName))
                {
                    if (current.GetClientScript().Length > 0)
                    {
                        if ((current.ConverterName != null) && (current.ConverterName.Length > 0))
                        {
                            context.Response.Write("// " + current.ConverterName + "\r\n");
                        }
                        context.Response.Write(current.GetClientScript());
                        context.Response.Write("\r\n");
                    }
                    strings.Add(fullName);
                }
            }
            IEnumerator enumerator2 = Utility.Settings.DeserializableConverters.Values.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                current = (IJavaScriptConverter) enumerator2.Current;
                fullName = current.GetType().FullName;
                if (!strings.Contains(fullName))
                {
                    if (current.GetClientScript().Length > 0)
                    {
                        if ((current.ConverterName != null) && (current.ConverterName.Length > 0))
                        {
                            context.Response.Write("// " + current.ConverterName + "\r\n");
                        }
                        context.Response.Write(current.GetClientScript());
                        context.Response.Write("\r\n");
                    }
                    strings.Add(fullName);
                }
            }
            context.Cache.Add("AjaxPro.converter", new CacheInfo(etag, date), null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
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

