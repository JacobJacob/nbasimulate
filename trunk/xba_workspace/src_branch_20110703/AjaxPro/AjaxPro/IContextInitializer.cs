namespace AjaxPro
{
    using System;
    using System.Web;

    public interface IContextInitializer
    {
        void InitializeContext(HttpContext context);
    }
}

