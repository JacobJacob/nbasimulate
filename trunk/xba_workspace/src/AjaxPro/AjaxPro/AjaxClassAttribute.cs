namespace AjaxPro
{
    using System;

    [Obsolete("The recommended alternative is AjaxPro.AjaxNamespaceAttribute.", true), AttributeUsage(AttributeTargets.Class)]
    public class AjaxClassAttribute : Attribute
    {
        public AjaxClassAttribute(string names)
        {
        }
    }
}

