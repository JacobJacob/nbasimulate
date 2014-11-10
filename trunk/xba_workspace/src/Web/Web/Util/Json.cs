namespace Web.Util
{
    using LitJson;
    using System;

    internal class Json
    {
        internal static string Dump(object obj)
        {
            return JsonMapper.ToJson(obj);
        }

        internal static T Load<T>(string json)
        {
            return JsonMapper.ToObject<T>(json);
        }
    }
}

