namespace AjaxPro
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class AjaxMethodAttribute : Attribute
    {
        private HttpSessionStateRequirement requireSessionState;
        private bool useAsyncProcessing;

        public AjaxMethodAttribute()
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        public AjaxMethodAttribute(HttpSessionStateRequirement requireSessionState)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
            this.requireSessionState = requireSessionState;
        }

        [Obsolete("The use of this argument is currently in beta state, please report any problems to bug@schwarz-interactive.de.")]
        public AjaxMethodAttribute(bool useAsyncProcessing)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
            this.useAsyncProcessing = useAsyncProcessing;
        }

        [Obsolete("The recommended alternative is AjaxPro.AjaxServerCacheAttribute.", true)]
        public AjaxMethodAttribute(int cacheSeconds)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        [Obsolete("The recommended alternative is AjaxPro.AjaxNamespaceAttribute.", true)]
        public AjaxMethodAttribute(string methodName)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        [Obsolete("The use of this argument is currently in beta state, please report any problems to bug@schwarz-interactive.de.")]
        public AjaxMethodAttribute(HttpSessionStateRequirement requireSessionState, bool useAsyncProcessing)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
            this.requireSessionState = requireSessionState;
            this.useAsyncProcessing = useAsyncProcessing;
        }

        [Obsolete("The recommended alternative is AjaxPro.AjaxServerCacheAttribute.", true)]
        public AjaxMethodAttribute(int cacheSeconds, HttpSessionStateRequirement requireSessionState)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        [Obsolete("The recommended alternative for methodName is AjaxPro.AjaxNamespaceAttribute.", true)]
        public AjaxMethodAttribute(string methodName, HttpSessionStateRequirement requireSessionState)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        [Obsolete("The recommended alternative for methodName is AjaxPro.AjaxNamespaceAttribute.", true)]
        public AjaxMethodAttribute(string methodName, int cacheSeconds)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        [Obsolete("The recommended alternative for methodName is AjaxPro.AjaxNamespaceAttribute.", true)]
        public AjaxMethodAttribute(string methodName, int cacheSeconds, HttpSessionStateRequirement requireSessionState)
        {
            this.useAsyncProcessing = false;
            this.requireSessionState = HttpSessionStateRequirement.UseDefault;
        }

        internal HttpSessionStateRequirement RequireSessionState
        {
            get
            {
                return this.requireSessionState;
            }
        }

        internal bool UseAsyncProcessing
        {
            get
            {
                return this.useAsyncProcessing;
            }
        }
    }
}

