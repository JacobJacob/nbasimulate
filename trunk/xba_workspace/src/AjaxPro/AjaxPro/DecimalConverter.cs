namespace AjaxPro
{
    using System;
    using System.Globalization;

    public class DecimalConverter : IJavaScriptConverter
    {
        public DecimalConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(decimal) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptNumber number = o as JavaScriptNumber;
            if (number == null)
            {
                throw new NotSupportedException();
            }
            return decimal.Parse(number.Value.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), NumberStyles.Any);
        }

        public override string Serialize(object o)
        {
            return JavaScriptSerializer.Serialize((double) ((decimal) o));
        }
    }
}

