namespace AjaxPro
{
    using System;

    public class DateTimeConverter : IJavaScriptConverter
    {
        public DateTimeConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(DateTime) };
            base.m_deserializableTypes = new Type[] { typeof(DateTime) };
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            JavaScriptObject obj2 = o as JavaScriptObject;
            if (obj2 == null)
            {
                throw new NotSupportedException();
            }
            int year = (int) JavaScriptDeserializer.Deserialize(obj2["Year"], typeof(int));
            int month = (int) JavaScriptDeserializer.Deserialize(obj2["Month"], typeof(int));
            int day = (int) JavaScriptDeserializer.Deserialize(obj2["Day"], typeof(int));
            int hour = (int) JavaScriptDeserializer.Deserialize(obj2["Hour"], typeof(int));
            int minute = (int) JavaScriptDeserializer.Deserialize(obj2["Minute"], typeof(int));
            int second = (int) JavaScriptDeserializer.Deserialize(obj2["Second"], typeof(int));
            int millisecond = (int) JavaScriptDeserializer.Deserialize(obj2["Millisecond"], typeof(int));
            DateTime time = new DateTime(year, month, day, hour, minute, second, millisecond);
            return time.AddMinutes(TimeZone.CurrentTimeZone.GetUtcOffset(time).TotalMinutes);
        }

        public override string Serialize(object o)
        {
            if (!(o is DateTime))
            {
                throw new NotSupportedException();
            }
            DateTime time = Convert.ToDateTime(o).ToUniversalTime();
            return string.Format("new Date(Date.UTC({0},{1},{2},{3},{4},{5},{6}))", new object[] { time.Year, time.Month - 1, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond });
        }
    }
}

