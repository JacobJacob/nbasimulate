namespace AjaxPro
{
    using System;
    using System.Reflection;
    using System.Text;
    using System.Web;

    internal class IFrameProcessor : IAjaxProcessor
    {
        private int hashCode;

        internal IFrameProcessor(HttpContext context, Type type) : base(context, type)
        {
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }

        public override object[] RetreiveParameters()
        {
            ParameterInfo[] parameters = base.method.GetParameters();
            object[] objArray = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                objArray[i] = parameters[i].DefaultValue;
            }
            string s = base.context.Request["data"];
            if (((s != null) && (parameters.Length != 0)) && (s != "{}"))
            {
                if (base.context.Request.ContentEncoding != Encoding.UTF8)
                {
                    s = Encoding.UTF8.GetString(base.context.Request.ContentEncoding.GetBytes(s));
                }
                this.hashCode = s.GetHashCode();
                if ((Utility.Settings != null) && (Utility.Settings.Encryption != null))
                {
                    s = Utility.Settings.Encryption.CryptProvider.Decrypt(s);
                }
                base.context.Items.Add("AjaxPro.JSON", s);
                JavaScriptObject obj2 = (JavaScriptObject) JavaScriptDeserializer.DeserializeFromJson(s, typeof(JavaScriptObject));
                for (int j = 0; j < parameters.Length; j++)
                {
                    if (obj2.Contains(parameters[j].Name))
                    {
                        objArray[j] = JavaScriptDeserializer.Deserialize(obj2[parameters[j].Name], parameters[j].ParameterType);
                    }
                }
            }
            return objArray;
        }

        public override string SerializeObject(object o)
        {
            string str = JavaScriptSerializer.Serialize(o);
            StringBuilder builder = new StringBuilder();
            builder.Append("<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/></head><body>\r\n<script type=\"text/javascript\" defer=\"defer\">\r\ndocument.body.res = \"");
            builder.Append(str.Replace(@"\", @"\\").Replace("\"", "\\\""));
            builder.Append("/\"+\"*\";\r\n</script>\r\n</body></html>");
            base.context.Response.ContentType = this.ContentType;
            base.context.Response.ContentEncoding = Encoding.UTF8;
            base.context.Response.Write(builder.ToString());
            return builder.ToString();
        }

        public override MethodInfo AjaxMethod
        {
            get
            {
                string methodName = base.context.Request["X-AjaxPro-Method"];
                if (methodName == null)
                {
                    return null;
                }
                return base.GetMethodInfo(methodName);
            }
        }

        internal override bool CanHandleRequest
        {
            get
            {
                return (base.context.Request["X-AjaxPro-Method"] != null);
            }
        }

        public override string ContentType
        {
            get
            {
                return "text/html";
            }
        }

        public override bool IsEncryptionAble
        {
            get
            {
                return true;
            }
        }
    }
}

