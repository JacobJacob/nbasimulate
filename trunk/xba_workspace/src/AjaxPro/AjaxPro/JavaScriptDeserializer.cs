namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Reflection;

    public sealed class JavaScriptDeserializer
    {
        public static object Deserialize(IJavaScriptObject o, Type type)
        {
            if (o == null)
            {
                return null;
            }
            if ((o is JavaScriptObject) && ((JavaScriptObject) o).Contains("__type"))
            {
                Type c = Type.GetType(((JavaScriptObject) o)["__type"].ToString());
                if ((type == null) || type.IsAssignableFrom(c))
                {
                    type = c;
                }
            }
            IJavaScriptConverter converter = null;
            if (Utility.Settings.DeserializableConverters.ContainsKey(type))
            {
                converter = (IJavaScriptConverter) Utility.Settings.DeserializableConverters[type];
                return converter.Deserialize(o, type);
            }
            IEnumerator enumerator = Utility.Settings.DeserializableConverters.Values.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object obj2;
                if (((IJavaScriptConverter) enumerator.Current).TryDeserializeValue(o, type, out obj2))
                {
                    return obj2;
                }
            }
            if (typeof(IJavaScriptObject).IsAssignableFrom(type))
            {
                return o;
            }
            if (!(o is JavaScriptObject))
            {
                throw new NotImplementedException(string.Concat(new object[] { "The object of type '", o.GetType().FullName, "' could not converted to type '", type, "'." }));
            }
            return DeserializeCustomObject(o as JavaScriptObject, type);
        }

        [Obsolete("The recommended alternative is AjaxPro.JavaScriptDeserializer.DeserializeFromJson(string, Type).")]
        public static object Deserialize(string json, Type type)
        {
            return DeserializeFromJson(json, type);
        }

        internal static object DeserializeCustomObject(JavaScriptObject o, Type type)
        {
            object obj2 = Activator.CreateInstance(type);
            foreach (MemberInfo info in type.GetMembers(BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.Public | BindingFlags.Instance))
            {
                if (info.MemberType == MemberTypes.Field)
                {
                    if (o.Contains(info.Name))
                    {
                        FieldInfo info2 = (FieldInfo) info;
                        info2.SetValue(obj2, Deserialize(o[info2.Name], info2.FieldType));
                    }
                }
                else if ((info.MemberType == MemberTypes.Property) && o.Contains(info.Name))
                {
                    PropertyInfo info3 = (PropertyInfo) info;
                    if (info3.CanWrite)
                    {
                        info3.SetValue(obj2, Deserialize(o[info3.Name], info3.PropertyType), new object[0]);
                    }
                }
            }
            return obj2;
        }

        public static object DeserializeFromJson(string json, Type type)
        {
            JSONParser parser = new JSONParser();
            return Deserialize(parser.GetJSONObject(json), type);
        }
    }
}

