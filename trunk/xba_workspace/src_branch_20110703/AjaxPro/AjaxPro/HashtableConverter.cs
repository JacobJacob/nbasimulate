namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Text;

    public class HashtableConverter : IJavaScriptConverter
    {
        public HashtableConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(Hashtable) };
            base.m_deserializableTypes = new Type[] { typeof(Hashtable) };
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            if (!(o is JavaScriptArray))
            {
                throw new NotSupportedException();
            }
            JavaScriptArray array = (JavaScriptArray) o;
            for (int i = 0; i < array.Count; i++)
            {
                if (!(array[i] is JavaScriptArray))
                {
                    throw new NotSupportedException();
                }
            }
            IDictionary dictionary = (IDictionary) Activator.CreateInstance(t);
            for (int j = 0; j < array.Count; j++)
            {
                JavaScriptArray array2 = (JavaScriptArray) array[j];
                object key = JavaScriptDeserializer.Deserialize(array2[0], Type.GetType(((JavaScriptString) array2[2]).ToString()));
                object obj3 = JavaScriptDeserializer.Deserialize(array2[1], Type.GetType(((JavaScriptString) array2[3]).ToString()));
                dictionary.Add(key, obj3);
            }
            return dictionary;
        }

        public override string Serialize(object o)
        {
            IDictionary dictionary = o as IDictionary;
            if (dictionary == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
            enumerator.Reset();
            bool flag = true;
            builder.Append("[");
            while (enumerator.MoveNext())
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append('[');
                builder.Append(JavaScriptSerializer.Serialize(enumerator.Key));
                builder.Append(',');
                builder.Append(JavaScriptSerializer.Serialize(enumerator.Value));
                builder.Append(',');
                builder.Append(JavaScriptSerializer.Serialize(enumerator.Key.GetType().FullName));
                builder.Append(',');
                builder.Append(JavaScriptSerializer.Serialize(enumerator.Value.GetType().FullName));
                builder.Append(']');
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}

