namespace AjaxPro
{
    using System;
    using System.Web;

    internal class AjaxSyncHttpHandler : IHttpHandler
    {
        private IAjaxProcessor p;

        internal AjaxSyncHttpHandler(IAjaxProcessor p)
        {
            this.p = p;
        }

        public void ProcessRequest(HttpContext context)
        {
            new AjaxProcHelper(this.p).Run();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

