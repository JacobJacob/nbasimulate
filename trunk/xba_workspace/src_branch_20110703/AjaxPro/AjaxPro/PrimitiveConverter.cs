namespace AjaxPro
{
    using System;
    using System.Globalization;

    public class PrimitiveConverter : IJavaScriptConverter
    {
        public PrimitiveConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(bool), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(double), typeof(float) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            if (!t.IsPrimitive)
            {
                throw new NotSupportedException();
            }
            string s = o.ToString().Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            if ((t == typeof(bool)) && (o is JavaScriptBoolean))
            {
                return (bool) ((JavaScriptBoolean) o);
            }
            if (t == typeof(byte))
            {
                return byte.Parse(s);
            }
            if (t == typeof(sbyte))
            {
                return sbyte.Parse(s);
            }
            if (t == typeof(short))
            {
                return short.Parse(s);
            }
            if (t == typeof(ushort))
            {
                return ushort.Parse(s);
            }
            if (t == typeof(int))
            {
                return int.Parse(s);
            }
            if (t == typeof(uint))
            {
                return uint.Parse(s);
            }
            if (t == typeof(long))
            {
                return long.Parse(s);
            }
            if (t == typeof(ulong))
            {
                return ulong.Parse(s);
            }
            if (t == typeof(double))
            {
                return double.Parse(s);
            }
            if (t != typeof(float))
            {
                throw new NotImplementedException("This primitive data type '" + t.FullName + "' is not implemented.");
            }
            return float.Parse(s);
        }

        public override string Serialize(object o)
        {
            if (o is bool)
            {
                return (((bool) o) ? "true" : "false");
            }
            if ((o is byte) || (o is sbyte))
            {
                return o.ToString();
            }
            if ((o is float) && float.IsNaN((float) o))
            {
                throw new ArgumentException("object must be a valid number (float.NaN)", "o");
            }
            if ((o is double) && double.IsNaN((double) o))
            {
                throw new ArgumentException("object must be a valid number (double.NaN)", "o");
            }
            string str = o.ToString().ToLower().Replace(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".");
            if ((str.IndexOf('e') < 0) && (str.IndexOf('.') > 0))
            {
                while (str.EndsWith("0"))
                {
                    str.Substring(0, str.Length - 1);
                }
                if (str.EndsWith("."))
                {
                    str.Substring(0, str.Length - 1);
                }
            }
            return str;
        }
    }
}

