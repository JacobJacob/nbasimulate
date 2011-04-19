namespace AjaxPro
{
    using System;
    using System.Collections.Specialized;
    using System.Text;

    public class NameValueCollectionConverter : IJavaScriptConverter
    {
        private string clientType = "Ajax.Web.NameValueCollection";

        public NameValueCollectionConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(NameValueCollection) };
            base.m_deserializableTypes = new Type[] { typeof(NameValueCollection) };
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptObject obj2 = o as JavaScriptObject;
            if (!typeof(NameValueCollection).IsAssignableFrom(t) || (obj2 == null))
            {
                throw new NotSupportedException();
            }
            NameValueCollection values = (NameValueCollection) Activator.CreateInstance(t);
            if (!obj2.Contains("keys") || !obj2.Contains("values"))
            {
                throw new ArgumentException("Missing values for 'keys' and 'values'.");
            }
            JavaScriptArray array = (JavaScriptArray) obj2["keys"];
            JavaScriptArray array2 = (JavaScriptArray) obj2["values"];
            if (array.Count != array2.Count)
            {
                throw new IndexOutOfRangeException("'keys' and 'values' have different length.");
            }
            for (int i = 0; i < array.Count; i++)
            {
                values.Add(array[i].ToString(), array2[i].ToString());
            }
            return values;
        }

        public override string GetClientScript()
        {
            return (JavaScriptUtil.GetClientNamespaceRepresentation(this.clientType) + "\r\n" + this.clientType + " = function(items) {\r\n\tthis.__type = \"System.Collections.Specialized.NameValueCollection\";\r\n\tthis.keys = [];\r\n\tthis.values = [];\r\n\r\n\tif(items != null && !isNaN(items.length)) {\r\n\t\tfor(var i=0; i<items.length; i++)\r\n\t\t\tthis.add(items[i][0], items[i][1]);\r\n\t}\r\n}\r\nObject.extend(" + this.clientType + ".prototype, {\r\n\tadd: function(k, v) {\r\n\t\tif(k == null || k.constructor != String || v == null || v.constructor != String)\r\n\t\t\treturn -1;\r\n\t\tthis.keys.push(k);\r\n\t\tthis.values.push(v);\r\n\t\treturn this.values.length -1;\r\n\t},\r\n\tcontainsKey: function(key) {\r\n\t\tfor(var i=0; i<this.keys.length; i++)\r\n\t\t\tif(this.keys[i] == key) return true;\r\n\t\treturn false;\r\n\t},\r\n\tgetKeys: function() {\r\n\t\treturn this.keys;\r\n\t},\r\n\tgetValue: function(k) {\r\n\t\tfor(var i=0; i<this.keys.length && i<this.values.length; i++)\r\n\t\t\tif(this.keys[i] == k) return this.values[i];\r\n\t\treturn null;\r\n\t},\r\n\tsetValue: function(k, v) {\r\n\t\tif(k == null || k.constructor != String || v == null || v.constructor != String)\r\n\t\t\treturn -1;\r\n\t\tfor(var i=0; i<this.keys.length && i<this.values.length; i++) {\r\n\t\t\tif(this.keys[i] == k) this.values[i] = v;\r\n\t\t\treturn i;\r\n\t\t}\r\n\t\treturn this.add(k, v);\r\n\t},\r\n\ttoJSON: function() {\r\n\t\treturn AjaxPro.toJSON({__type:this.__type,keys:this.keys,values:this.values});\r\n\t}\r\n}, true);\r\n");
        }

        public override string Serialize(object o)
        {
            NameValueCollection values = o as NameValueCollection;
            if (values == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            builder.Append("new ");
            builder.Append(this.clientType);
            builder.Append("([");
            for (int i = 0; i < values.Keys.Count; i++)
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
                builder.Append(JavaScriptSerializer.Serialize(values.Keys[i]));
                builder.Append(',');
                builder.Append(JavaScriptSerializer.Serialize(values[values.Keys[i]]));
                builder.Append(']');
            }
            builder.Append("])");
            return builder.ToString();
        }
    }
}

