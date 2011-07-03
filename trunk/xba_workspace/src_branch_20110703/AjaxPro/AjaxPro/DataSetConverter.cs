namespace AjaxPro
{
    using System;
    using System.Data;
    using System.Text;

    public class DataSetConverter : IJavaScriptConverter
    {
        private string clientType = "Ajax.Web.DataSet";

        public DataSetConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(DataSet) };
            base.m_deserializableTypes = new Type[] { typeof(DataSet) };
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptObject obj2 = o as JavaScriptObject;
            if (obj2 == null)
            {
                throw new NotSupportedException();
            }
            if (!obj2.Contains("Tables") || !(obj2["Tables"] is JavaScriptArray))
            {
                throw new NotSupportedException();
            }
            JavaScriptArray array = (JavaScriptArray) obj2["Tables"];
            DataSet set = new DataSet();
            DataTable table = null;
            foreach (IJavaScriptObject obj3 in array)
            {
                table = (DataTable) JavaScriptDeserializer.Deserialize(obj3, typeof(DataTable));
                if (table != null)
                {
                    set.Tables.Add(table);
                }
            }
            return set;
        }

        public override string GetClientScript()
        {
            return (JavaScriptUtil.GetClientNamespaceRepresentation(this.clientType) + "\r\n" + this.clientType + " = function(t) {\r\n\tthis.__type = \"System.Data.DataSet,System.Data\";\r\n\tthis.Tables = [];\r\n\tthis.addTable = function(t) {\r\n\t\tthis.Tables.push(t);\r\n\t}\r\n\tif(t != null) {\r\n\t\tfor(var i=0; i<t.length; i++) {\r\n\t\t\tthis.addTable(t[i]);\r\n\t\t}\r\n\t}\r\n}\r\n");
        }

        public override string Serialize(object o)
        {
            DataSet set = o as DataSet;
            if (set == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            builder.Append("new ");
            builder.Append(this.clientType);
            builder.Append("([");
            foreach (DataTable table in set.Tables)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append(JavaScriptSerializer.Serialize(table));
            }
            builder.Append("])");
            return builder.ToString();
        }
    }
}

