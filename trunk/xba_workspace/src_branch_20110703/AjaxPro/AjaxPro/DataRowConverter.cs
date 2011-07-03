namespace AjaxPro
{
    using System;
    using System.Data;
    using System.Text;

    public class DataRowConverter : IJavaScriptConverter
    {
        public DataRowConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(DataRow) };
        }

        public override string Serialize(object o)
        {
            DataRow row = o as DataRow;
            if (row == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            DataColumnCollection columns = row.Table.Columns;
            int count = columns.Count;
            bool flag = true;
            builder.Append("[");
            for (int i = 0; i < count; i++)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    builder.Append(",");
                }
                builder.Append(JavaScriptSerializer.Serialize(row[columns[i].ColumnName]));
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}

