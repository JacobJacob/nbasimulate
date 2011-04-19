namespace AjaxPro
{
    using System;

    [Obsolete("The recommended alternative is adding the converter to web.config/ajaxNet/ajaxSettings/jsonConverters.", true), AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class JavaScriptConverterAttribute : Attribute
    {
        private Type type = null;

        public JavaScriptConverterAttribute(Type type)
        {
            if (!typeof(IJavaScriptConverter).IsAssignableFrom(type))
            {
                throw new InvalidCastException();
            }
            this.type = type;
        }

        internal IJavaScriptConverter Converter
        {
            get
            {
                return (IJavaScriptConverter) Activator.CreateInstance(this.type);
            }
        }
    }
}

