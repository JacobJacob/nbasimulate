namespace AjaxPro
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public class ExceptionConverter : IJavaScriptConverter
    {
        public ExceptionConverter()
        {
            base.m_AllowInheritance = true;
            base.m_serializableTypes = new Type[] { typeof(Exception), typeof(NotImplementedException), typeof(NotSupportedException), typeof(NullReferenceException), typeof(SecurityException) };
        }

        public override string Serialize(object o)
        {
            Exception exception = (Exception) o;
            StringBuilder builder = new StringBuilder();
            builder.Append("null; r.error = ");
            builder.Append("{\"Message\":");
            builder.Append(JavaScriptSerializer.Serialize(exception.Message));
            builder.Append(",\"Type\":");
            builder.Append(JavaScriptSerializer.Serialize(o.GetType().FullName));
            if (Utility.Settings.DebugEnabled)
            {
                builder.Append(",\"Stack\":");
                builder.Append(JavaScriptSerializer.Serialize(exception.StackTrace));
                if (exception.TargetSite != null)
                {
                    builder.Append(",\"TargetSite\":");
                    builder.Append(JavaScriptSerializer.Serialize(exception.TargetSite.ToString()));
                }
                builder.Append(",\"Source\":");
                builder.Append(JavaScriptSerializer.Serialize(exception.Source));
            }
            builder.Append("}");
            return builder.ToString();
        }

        public override bool TrySerializeValue(object o, Type t, out string json)
        {
            Exception exception = o as Exception;
            if (exception != null)
            {
                json = this.Serialize(exception);
                return true;
            }
            return base.TrySerializeValue(o, t, out json);
        }
    }
}

