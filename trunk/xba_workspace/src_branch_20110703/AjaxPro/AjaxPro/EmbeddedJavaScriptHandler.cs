namespace AjaxPro
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Caching;

    internal class EmbeddedJavaScriptHandler : IHttpHandler
    {
        private string fileName;

        internal EmbeddedJavaScriptHandler(string fileName)
        {
            this.fileName = fileName;
        }

        public void ProcessRequest(HttpContext context)
        {
            string etag = context.Request.Headers["If-None-Match"];
            string str2 = context.Request.Headers["If-Modified-Since"];
            if (context.Cache["AjaxPro." + this.fileName] != null)
            {
                CacheInfo info = (CacheInfo) context.Cache["AjaxPro." + this.fileName];
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
            etag = MD5Helper.GetHash(Encoding.Default.GetBytes(this.fileName));
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            context.Response.AddHeader("Content-Type", "application/x-javascript");
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetETag(etag);
            context.Response.Cache.SetLastModified(date);
            string[] strArray = this.fileName.Split(new char[] { ',' });
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            for (int i = 0; i < strArray.Length; i++)
            {
                Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("AjaxPro." + strArray[i] + ".js");
                if (manifestResourceStream != null)
                {
                    StreamReader reader = new StreamReader(manifestResourceStream);
                    context.Response.Write(reader.ReadToEnd());
                    context.Response.Write("\r\n");
                    reader.Close();
                    if ((strArray[i] == "prototype") && Utility.Settings.OldStyle.Contains("objectExtendPrototype"))
                    {
                        context.Response.Write("\r\nObject.prototype.extend = function(o, override) {\r\n\treturn Object.extend.apply(this, [this, o, override != false]);\r\n}\r\n");
                    }
                }
            }
            context.Cache.Add("AjaxPro." + this.fileName, new CacheInfo(etag, date), null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
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

