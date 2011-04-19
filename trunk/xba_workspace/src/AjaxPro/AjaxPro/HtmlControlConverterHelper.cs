namespace AjaxPro
{
    using System;
    using System.Web.UI;

    internal class HtmlControlConverterHelper : TemplateControl
    {
        internal static Control Parse(string controlString)
        {
            HtmlControlConverterHelper helper = new HtmlControlConverterHelper();
            return helper.ParseControl(controlString);
        }
    }
}

