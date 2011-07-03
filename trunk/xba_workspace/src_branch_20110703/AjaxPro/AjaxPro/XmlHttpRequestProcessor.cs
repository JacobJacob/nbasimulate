namespace AjaxPro
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    internal class XmlHttpRequestProcessor : IAjaxProcessor
    {
        private int hashCode;

        internal XmlHttpRequestProcessor(HttpContext context, Type type) : base(context, type)
        {
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }

        public override object[] RetreiveParameters()
        {
            base.context.Request.ContentEncoding = Encoding.UTF8;
            ParameterInfo[] parameters = base.method.GetParameters();
            object[] objArray = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                objArray[i] = parameters[i].DefaultValue;
            }
            byte[] buffer = new byte[base.context.Request.InputStream.Length];
            if (base.context.Request.InputStream.Read(buffer, 0, buffer.Length) == 0)
            {
                return null;
            }
            StreamReader reader = new StreamReader(new MemoryStream(buffer), Encoding.UTF8);
            string jsoncrypt = null;
            try
            {
                jsoncrypt = reader.ReadToEnd();
            }
            finally
            {
                reader.Close();
            }
            if (((jsoncrypt != null) && (parameters.Length != 0)) && (jsoncrypt != "{}"))
            {
                this.hashCode = jsoncrypt.GetHashCode();
                if ((Utility.Settings != null) && (Utility.Settings.Encryption != null))
                {
                    jsoncrypt = Utility.Settings.Encryption.CryptProvider.Decrypt(jsoncrypt);
                }
                base.context.Items.Add("AjaxPro.JSON", jsoncrypt);
                JavaScriptObject obj2 = (JavaScriptObject) JavaScriptDeserializer.DeserializeFromJson(jsoncrypt, typeof(JavaScriptObject));
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
            string s = JavaScriptSerializer.Serialize(o) + ";/*";
            base.context.Response.Write(s);
            return s;
        }

        public override MethodInfo AjaxMethod
        {
            get
            {
                string methodName = base.context.Request.Headers["X-AjaxPro-Method"];
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
                return (base.context.Request.Headers["X-AjaxPro-Method"] != null);
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

