namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IDictionaryConverter : IJavaScriptConverter
    {
        private string clientType = "Ajax.Web.Dictionary";

        public IDictionaryConverter()
        {
            base.m_AllowInheritance = true;
            base.m_serializableTypes = new Type[] { typeof(IDictionary), typeof(NameValueCollection) };
            base.m_deserializableTypes = new Type[] { typeof(IDictionary), typeof(NameValueCollection) };
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptObject obj2 = o as JavaScriptObject;
            if (obj2 == null)
            {
                throw new NotSupportedException();
            }
            if (!obj2.Contains("keys") || !obj2.Contains("values"))
            {
                throw new NotSupportedException();
            }
            IDictionary dictionary = (IDictionary) Activator.CreateInstance(t);
            ParameterInfo[] parameters = t.GetMethod("Add").GetParameters();
            Type parameterType = parameters[0].ParameterType;
            Type type = parameters[1].ParameterType;
            JavaScriptArray array = obj2["keys"] as JavaScriptArray;
            JavaScriptArray array2 = obj2["values"] as JavaScriptArray;
            for (int i = 0; (i < array.Count) && (i < array2.Count); i++)
            {
                object key = JavaScriptDeserializer.Deserialize(array[i], parameterType);
                object obj4 = JavaScriptDeserializer.Deserialize(array2[i], type);
                dictionary.Add(key, obj4);
            }
            return dictionary;
        }

        public override string GetClientScript()
        {
            return (JavaScriptUtil.GetClientNamespaceRepresentation(this.clientType) + "\r\n" + this.clientType + " = function(type,items) {\r\n\tthis.__type = type;\r\n\tthis.keys = [];\r\n\tthis.values = [];\r\n\r\n\tif(items != null && !isNaN(items.length)) {\r\n\t\tfor(var i=0; i<items.length; i++)\r\n\t\t\tthis.add(items[i][0], items[i][1]);\r\n\t}\r\n}\r\nObject.extend(" + this.clientType + ".prototype, {\r\n\tadd: function(k, v) {\r\n\t\tthis.keys.push(k);\r\n\t\tthis.values.push(v);\r\n\t\treturn this.values.length -1;\r\n\t},\r\n\tcontainsKey: function(key) {\r\n\t\tfor(var i=0; i<this.keys.length; i++)\r\n\t\t\tif(this.keys[i] == key) return true;\r\n\t\treturn false;\r\n\t},\r\n\tgetKeys: function() {\r\n\t\treturn this.keys;\r\n\t},\r\n\tgetValue: function(key) {\r\n\t\tfor(var i=0; i<this.keys.length && i<this.values.length; i++)\r\n\t\t\tif(this.keys[i] == key) return this.values[i];\r\n\t\treturn null;\r\n\t},\r\n\tsetValue: function(k, v) {\r\n\t\tfor(var i=0; i<this.keys.length && i<this.values.length; i++) {\r\n\t\t\tif(this.keys[i] == k) this.values[i] = v;\r\n\t\t\treturn i;\r\n\t\t}\r\n\t\treturn this.add(k, v);\r\n\t},\r\n\ttoJSON: function() {\r\n\t\treturn AjaxPro.toJSON({__type:this.__type,keys:this.keys,values:this.values});\r\n\t}\r\n}, true);\r\n");
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
            builder.Append("new ");
            builder.Append(this.clientType);
            builder.Append("(");
            bool flag2 = enumerator.MoveNext();
            Type type = o.GetType();
            builder.Append(JavaScriptSerializer.Serialize(type.FullName));
            builder.Append(",[");
            if (flag2)
            {
                do
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
                    builder.Append(']');
                }
                while (enumerator.MoveNext());
            }
            builder.Append("]");
            builder.Append(")");
            return builder.ToString();
        }

        public override bool TrySerializeValue(object o, Type t, out string json)
        {
            return base.TrySerializeValue(o, t, out json);
        }
    }
}

