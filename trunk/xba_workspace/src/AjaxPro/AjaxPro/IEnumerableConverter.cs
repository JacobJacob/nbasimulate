namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IEnumerableConverter : IJavaScriptConverter
    {
        public IEnumerableConverter()
        {
            base.m_AllowInheritance = true;
            base.m_serializableTypes = new Type[] { typeof(IEnumerable) };
        }

        public override string Serialize(object o)
        {
            IEnumerable enumerable = o as IEnumerable;
            if (enumerable == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            builder.Append("[");
            foreach (object obj2 in enumerable)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append(JavaScriptSerializer.Serialize(obj2));
            }
            builder.Append("]");
            return builder.ToString();
        }

        public override bool TryDeserializeValue(IJavaScriptObject jso, Type t, out object o)
        {
            if (typeof(IDictionary).IsAssignableFrom(t))
            {
                o = null;
                return false;
            }
            return base.TryDeserializeValue(jso, t, out o);
        }

        public override bool TrySerializeValue(object o, Type t, out string json)
        {
            if (typeof(IDictionary).IsAssignableFrom(t))
            {
                json = null;
                return false;
            }
            return base.TrySerializeValue(o, t, out json);
        }
    }
}

