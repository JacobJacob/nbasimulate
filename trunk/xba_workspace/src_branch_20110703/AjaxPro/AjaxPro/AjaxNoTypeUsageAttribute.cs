namespace AjaxPro
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false)]
    public class AjaxNoTypeUsageAttribute : Attribute
    {
    }
}

