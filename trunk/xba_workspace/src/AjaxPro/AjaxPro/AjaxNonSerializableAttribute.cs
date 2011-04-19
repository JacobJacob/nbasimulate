namespace AjaxPro
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple=false)]
    public class AjaxNonSerializableAttribute : Attribute
    {
    }
}

