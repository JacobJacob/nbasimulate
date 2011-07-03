namespace AjaxPro
{
    using System;
    using System.Web.SessionState;

    internal class AjaxAsyncHttpHandlerSessionReadOnly : AjaxAsyncHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        internal AjaxAsyncHttpHandlerSessionReadOnly(IAjaxProcessor p) : base(p)
        {
        }
    }
}

