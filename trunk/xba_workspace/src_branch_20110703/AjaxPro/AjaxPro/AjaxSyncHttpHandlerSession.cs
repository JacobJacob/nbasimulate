namespace AjaxPro
{
    using System;
    using System.Web.SessionState;

    internal class AjaxSyncHttpHandlerSession : AjaxSyncHttpHandler, IRequiresSessionState
    {
        internal AjaxSyncHttpHandlerSession(IAjaxProcessor p) : base(p)
        {
        }
    }
}

