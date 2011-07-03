namespace AjaxPro
{
    using System;
    using System.Runtime.InteropServices;

    public class EnumConverter : IJavaScriptConverter
    {
        public EnumConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(Enum) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            object obj2 = JavaScriptDeserializer.Deserialize(o, Enum.GetUnderlyingType(t));
            if (!Enum.IsDefined(t, obj2))
            {
                throw new ArgumentException("Invalid value for enum of type '" + t.ToString() + "'.", "o");
            }
            return Enum.ToObject(t, obj2);
        }

        public override string Serialize(object o)
        {
            return JavaScriptSerializer.Serialize((double) o);
        }

        public override bool TryDeserializeValue(IJavaScriptObject jso, Type t, out object o)
        {
            if (t.IsEnum)
            {
                o = this.Deserialize(jso, t);
                return true;
            }
            return base.TryDeserializeValue(jso, t, out o);
        }

        public override bool TrySerializeValue(object o, Type t, out string json)
        {
            if (t.IsEnum)
            {
                json = JavaScriptSerializer.Serialize(Convert.ChangeType(o, Enum.GetUnderlyingType(t)));
                return true;
            }
            return base.TrySerializeValue(o, t, out json);
        }
    }
}

