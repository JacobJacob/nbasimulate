namespace AjaxPro
{
    using System;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=false)]
    public class AjaxNamespaceAttribute : Attribute
    {
        private string _clientNS = null;

        public AjaxNamespaceAttribute(string clientNS)
        {
            string pattern = @"^[a-zA-Z_]{1}([a-zA-Z_]*([\d]*)?)*((\.)?[a-zA-Z_]+([\d]*)?)*$";
            if ((!Regex.IsMatch(clientNS, pattern) || clientNS.StartsWith(".")) || clientNS.EndsWith("."))
            {
                throw new NotSupportedException("The namespace '" + clientNS + "' is not supported.");
            }
            this._clientNS = clientNS;
        }

        internal string ClientNamespace
        {
            get
            {
                if ((this._clientNS != null) && (this._clientNS.Trim().Length > 0))
                {
                    return this._clientNS.Replace("-", "_").Replace(" ", "_");
                }
                return this._clientNS;
            }
        }
    }
}

