namespace AjaxPro
{
    using System;
    using System.Runtime.InteropServices;

    public class StringConverter : IJavaScriptConverter
    {
        public StringConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(string), typeof(char) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            if (!Utility.Settings.OldStyle.Contains("allowNumberBooleanAsString"))
            {
                if (o is JavaScriptNumber)
                {
                    return JavaScriptDeserializer.Deserialize(o, typeof(long));
                }
                if (o is JavaScriptBoolean)
                {
                    return JavaScriptDeserializer.Deserialize(o, typeof(bool));
                }
            }
            if (t == typeof(char))
            {
                string str = o.ToString();
                if (str.Length == 0)
                {
                    return '\0';
                }
                return str[0];
            }
            return o.ToString();
        }

        public override string Serialize(object o)
        {
            return JavaScriptUtil.QuoteString(o.ToString());
        }

        public override bool TryDeserializeValue(IJavaScriptObject jso, Type t, out object o)
        {
            if (t.IsAssignableFrom(typeof(string)))
            {
                o = this.Deserialize(jso, t);
                return true;
            }
            return base.TryDeserializeValue(jso, t, out o);
        }
    }
}

