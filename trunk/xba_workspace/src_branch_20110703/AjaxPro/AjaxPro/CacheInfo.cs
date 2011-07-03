namespace AjaxPro
{
    using System;

    internal class CacheInfo
    {
        private string etag;
        private DateTime lastMod;

        internal CacheInfo(string etag, DateTime lastMod)
        {
            this.etag = etag;
            this.lastMod = lastMod;
        }

        internal string ETag
        {
            get
            {
                return null;
            }
        }

        internal DateTime LastModified
        {
            get
            {
                return this.lastMod;
            }
        }
    }
}

