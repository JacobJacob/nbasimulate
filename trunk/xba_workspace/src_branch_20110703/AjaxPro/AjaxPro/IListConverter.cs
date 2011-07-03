namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IListConverter : IJavaScriptConverter
    {
        public IListConverter()
        {
            base.m_AllowInheritance = true;
            base.m_serializableTypes = new Type[] { typeof(IList), typeof(IEnumerable) };
            base.m_deserializableTypes = base.m_serializableTypes;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptArray array = o as JavaScriptArray;
            if (array == null)
            {
                throw new NotSupportedException();
            }
            if (t.IsArray)
            {
                Type elementType = Type.GetType(t.AssemblyQualifiedName.Replace("[]", ""));
                Array array2 = Array.CreateInstance(elementType, (array != null) ? array.Count : 0);
                try
                {
                    if (array == null)
                    {
                        return array2;
                    }
                    for (int j = 0; j < array.Count; j++)
                    {
                        object obj2 = JavaScriptDeserializer.Deserialize(array[j], elementType);
                        array2.SetValue(obj2, j);
                    }
                }
                catch (InvalidCastException exception)
                {
                    throw new InvalidCastException("Array ('" + t.Name + "') could not be filled with value of type '" + elementType.Name + "'.", exception);
                }
                return array2;
            }
            if (!typeof(IList).IsAssignableFrom(t) || !(o is JavaScriptArray))
            {
                throw new NotSupportedException();
            }
            IList list = (IList) Activator.CreateInstance(t);
            ParameterInfo info2 = t.GetMethod("Add").GetParameters()[0];
            for (int i = 0; i < array.Count; i++)
            {
                list.Add(JavaScriptDeserializer.Deserialize(array[i], info2.ParameterType));
            }
            return list;
        }

        public override string Serialize(object o)
        {
            StringBuilder builder = new StringBuilder();
            IEnumerable enumerable = (IEnumerable) o;
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
            if (t.IsArray)
            {
                json = this.Serialize(o);
                return true;
            }
            return base.TrySerializeValue(o, t, out json);
        }
    }
}

