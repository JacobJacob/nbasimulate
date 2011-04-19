namespace AjaxPro
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Web;
    using System.Web.Caching;

    public class BitmapConverter : IJavaScriptConverter
    {
        private string clientType = "Ajax.Web.Bitmap";
        private string mimeType = "image/jpeg";
        private long quality = 100L;

        public BitmapConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(Bitmap) };
        }

        public override string GetClientScript()
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath != "/")
            {
                applicationPath = applicationPath + "/";
            }
            return (JavaScriptUtil.GetClientNamespaceRepresentation(this.clientType) + "\r\n" + this.clientType + " = function(id) {\r\n\tthis.src = '" + applicationPath + "ajaximage/' + id + '.ashx';\r\n}\r\n\r\nObject.extend(" + this.clientType + ".prototype,  {\r\n\tgetImage: function() {\r\n\t\tvar i = new Image();\r\n\t\ti.src = this.src;\r\n\t\treturn i;\r\n\t}\r\n}, false);\r\n");
        }

        public override void Initialize(StringDictionary d)
        {
            if (d.ContainsKey("mimeType"))
            {
                this.mimeType = d["mimeType"];
                if (d.ContainsKey("quality"))
                {
                    long num = 0L;
                    try
                    {
                        num = long.Parse(d["quality"]);
                        this.quality = num;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public static void RemoveBitmapFromCache(string key, object o, CacheItemRemovedReason reason)
        {
            if (HttpContext.Current.Cache[key] != null)
            {
                try
                {
                    AjaxBitmap bitmap = HttpContext.Current.Cache[key] as AjaxBitmap;
                    if ((bitmap != null) && (bitmap.bmp != null))
                    {
                        bitmap.bmp.Dispose();
                    }
                }
                catch (Exception)
                {
                }
                HttpContext.Current.Cache.Remove(key);
            }
        }

        public override string Serialize(object o)
        {
            string key = Guid.NewGuid().ToString();
            AjaxBitmap bitmap = new AjaxBitmap();
            bitmap.bmp = o as Bitmap;
            bitmap.mimeType = this.mimeType;
            bitmap.quality = this.quality;
            HttpContext.Current.Cache.Add(key, bitmap, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(30.0), CacheItemPriority.BelowNormal, new CacheItemRemovedCallback(BitmapConverter.RemoveBitmapFromCache));
            return ("new " + this.clientType + "('" + key + "')");
        }
    }
}

