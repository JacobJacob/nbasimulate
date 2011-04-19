namespace AjaxPro
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IJavaScriptObjectConverter : IJavaScriptConverter
    {
        public IJavaScriptObjectConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(IJavaScriptObject), typeof(JavaScriptArray), typeof(JavaScriptBoolean), typeof(JavaScriptNumber), typeof(JavaScriptObject), typeof(JavaScriptString) };
        }

        public override string Serialize(object o)
        {
            JavaScriptObject obj2 = o as JavaScriptObject;
            if (obj2 == null)
            {
                return ((IJavaScriptObject) o).Value;
            }
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            builder.Append("{");
            foreach (string str in obj2.Keys)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append(JavaScriptSerializer.Serialize(str));
                builder.Append(":");
                builder.Append(JavaScriptSerializer.Serialize(obj2[str]));
            }
            builder.Append("}");
            return builder.ToString();
        }

        public override bool TryDeserializeValue(IJavaScriptObject jso, Type t, out object o)
        {
            if (typeof(IJavaScriptObject).IsAssignableFrom(t))
            {
                o = jso;
                return true;
            }
            return base.TryDeserializeValue(jso, t, out o);
        }
    }
}

