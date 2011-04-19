namespace AjaxPro
{
    using System;
    using System.Security.Principal;
    using System.Web;

    internal class AjaxAsyncHttpHandler : IHttpAsyncHandler, IHttpHandler
    {
        private HttpContext ctx = null;
        private IAjaxProcessor p;

        internal AjaxAsyncHttpHandler(IAjaxProcessor p)
        {
            this.p = p;
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            this.ctx = context;
            IntPtr token = WindowsIdentity.GetCurrent().Token;
            AjaxProcHelper helper = new AjaxProcHelper(this.p, token);
            AsyncAjaxProcDelegate delegate2 = new AsyncAjaxProcDelegate(helper.Run);
            return delegate2.BeginInvoke(cb, delegate2);
        }

        public void EndProcessRequest(IAsyncResult ar)
        {
            ((AsyncAjaxProcDelegate) ar.AsyncState).EndInvoke(ar);
        }

        public void ProcessRequest(HttpContext context)
        {
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

