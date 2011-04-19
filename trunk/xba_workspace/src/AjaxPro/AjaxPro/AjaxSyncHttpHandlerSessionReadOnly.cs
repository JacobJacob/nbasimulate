namespace AjaxPro
{
    using System;
    using System.Web.SessionState;

    internal class AjaxSyncHttpHandlerSessionReadOnly : AjaxSyncHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        internal AjaxSyncHttpHandlerSessionReadOnly(IAjaxProcessor p) : base(p)
        {
        }
    }
}

