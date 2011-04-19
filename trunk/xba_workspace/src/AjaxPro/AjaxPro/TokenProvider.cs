namespace AjaxPro
{
    using System;

    public abstract class TokenProvider
    {
        public abstract string GetToken();
        public abstract bool Parse(string token);
    }
}

