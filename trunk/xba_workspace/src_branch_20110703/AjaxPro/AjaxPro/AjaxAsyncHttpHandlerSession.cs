namespace AjaxPro
{
    using System;
    using System.Web.SessionState;

    internal class AjaxAsyncHttpHandlerSession : AjaxAsyncHttpHandler, IRequiresSessionState
    {
        internal AjaxAsyncHttpHandlerSession(IAjaxProcessor p) : base(p)
        {
        }
    }
}

