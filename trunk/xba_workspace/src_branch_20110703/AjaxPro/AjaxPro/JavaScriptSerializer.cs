namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Text;

    public sealed class JavaScriptSerializer
    {
        public static string Serialize(object o)
        {
            string str;
            if ((o == null) || (o is DBNull))
            {
                return "null";
            }
            IJavaScriptConverter converter = null;
            Type key = o.GetType();
            if (Utility.Settings.SerializableConverters.ContainsKey(key))
            {
                converter = (IJavaScriptConverter) Utility.Settings.SerializableConverters[key];
                return converter.Serialize(o);
            }
            IEnumerator enumerator = Utility.Settings.SerializableConverters.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (((IJavaScriptConverter) enumerator.Current).TrySerializeValue(o, key, out str))
                {
                    return str;
                }
            }
            try
            {
                str = SerializeCustomObject(o);
            }
            catch (StackOverflowException)
            {
                throw new Exception("AjaxPro exception while trying to serialize type '" + key.Name + "'.");
            }
            return str;
        }

        internal static string SerializeCustomObject(object o)
        {
            Type type = o.GetType();
            AjaxNonSerializableAttribute[] customAttributes = (AjaxNonSerializableAttribute[]) type.GetCustomAttributes(typeof(AjaxNonSerializableAttribute), true);
            AjaxNoTypeUsageAttribute[] attributeArray2 = (AjaxNoTypeUsageAttribute[]) type.GetCustomAttributes(typeof(AjaxNoTypeUsageAttribute), true);
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            builder.Append('{');
            if (attributeArray2.Length == 0)
            {
                builder.Append("\"__type\":");
                builder.Append(Serialize(type.AssemblyQualifiedName));
                flag = false;
            }
            foreach (FieldInfo info in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (((customAttributes.Length > 0) && (info.GetCustomAttributes(typeof(AjaxPropertyAttribute), true).Length > 0)) || ((customAttributes.Length == 0) && (info.GetCustomAttributes(typeof(AjaxNonSerializableAttribute), true).Length == 0)))
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(',');
                    }
                    builder.Append(Serialize(info.Name));
                    builder.Append(':');
                    builder.Append(Serialize(info.GetValue(o)));
                }
            }
            flag = false;
            foreach (PropertyInfo info2 in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
            {
                MethodInfo getMethod = info2.GetGetMethod();
                if ((getMethod.GetParameters().Length <= 0) && (((customAttributes.Length > 0) && (getMethod.GetCustomAttributes(typeof(AjaxPropertyAttribute), true).Length > 0)) || ((customAttributes.Length == 0) && (getMethod.GetCustomAttributes(typeof(AjaxNonSerializableAttribute), true).Length == 0))))
                {
                    if (flag)
                    {
                        flag = false;
                    }
                    else
                    {
                        builder.Append(",");
                    }
                    builder.Append(Serialize(info2.Name));
                    builder.Append(':');
                    builder.Append(Serialize(getMethod.Invoke(o, null)));
                }
            }
            builder.Append('}');
            return builder.ToString();
        }

        [Obsolete("The recommended alternative is JavaScriptSerializer.Serialize(object).", false)]
        public static string SerializeString(string s)
        {
            return Serialize(s);
        }
    }
}

