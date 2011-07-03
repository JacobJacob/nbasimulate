namespace AjaxPro
{
    using System;

    public class GuidConverter : IJavaScriptConverter
    {
        public GuidConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(Guid) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            return new Guid(o.ToString());
        }

        public override string Serialize(object o)
        {
            return JavaScriptSerializer.Serialize(o.ToString());
        }
    }
}

