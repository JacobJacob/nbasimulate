namespace AjaxPro
{
    using System;
    using System.Data;

    public class DataViewConverter : IJavaScriptConverter
    {
        public DataViewConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(DataView) };
        }

        public override string Serialize(object o)
        {
            DataView view = o as DataView;
            if (view == null)
            {
                throw new NotSupportedException();
            }
            DataTableConverter converter = new DataTableConverter();
            DataTable table = view.Table.Clone();
            for (int i = 0; i < view.Count; i++)
            {
                table.ImportRow(view[i].Row);
            }
            return converter.Serialize(table);
        }
    }
}

