namespace AjaxPro
{
    using System;
    using System.Data;
    using System.Text;

    public class DataRowViewConverter : IJavaScriptConverter
    {
        private string clientType = "Ajax.Web.DataRow";

        public DataRowViewConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(DataRowView) };
        }

        public override string Serialize(object o)
        {
            DataRowView view = o as DataRowView;
            if (view == null)
            {
                throw new NotSupportedException();
            }
            StringBuilder builder = new StringBuilder();
            DataColumnCollection columns = view.DataView.Table.Columns;
            int count = columns.Count;
            bool flag = true;
            builder.Append("new ");
            builder.Append(this.clientType);
            builder.Append("([");
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
                builder.Append(JavaScriptSerializer.Serialize(view[columns[i].ColumnName]));
            }
            builder.Append("])");
            return builder.ToString();
        }
    }
}

