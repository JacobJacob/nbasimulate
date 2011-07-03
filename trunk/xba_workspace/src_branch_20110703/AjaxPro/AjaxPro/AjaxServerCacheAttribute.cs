namespace AjaxPro
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class AjaxServerCacheAttribute : Attribute
    {
        private TimeSpan cacheDuration;
        private bool isCacheEnabled = false;

        public AjaxServerCacheAttribute(int seconds)
        {
            if (seconds > 0)
            {
                this.cacheDuration = new TimeSpan(0, 0, 0, seconds, 0);
                this.isCacheEnabled = true;
            }
        }

        internal TimeSpan CacheDuration
        {
            get
            {
                return this.cacheDuration;
            }
        }

        internal bool IsCacheEnabled
        {
            get
            {
                return this.isCacheEnabled;
            }
        }
    }
}

