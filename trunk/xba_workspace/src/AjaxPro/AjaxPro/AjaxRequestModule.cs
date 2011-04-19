namespace AjaxPro
{
    using System;
    using System.Web;

    public class AjaxRequestModule : IHttpModule
    {
        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication) sender;
            if (application.Request.RawUrl.ToLower().StartsWith(((application.Request.ApplicationPath == "/") ? application.Request.ApplicationPath : (application.Request.ApplicationPath + "/")) + Utility.HandlerPath) && ((application.Request.UrlReferrer == null) || (application.Context.Request.UrlReferrer.Host != application.Context.Request.Url.Host)))
            {
                throw new HttpException(500, "Access denied.");
            }
        }

        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.context_BeginRequest);
        }
    }
}

