namespace AjaxPro
{
    using System;
    using System.Reflection;
    using System.Web;

    public abstract class IAjaxProcessor
    {
        protected HttpContext context;
        protected AjaxMethodAttribute[] m_methodAttributes = null;
        protected AjaxNamespaceAttribute[] m_namespaceAttributes = null;
        protected AjaxServerCacheAttribute[] m_serverCacheAttributes = null;
        protected MethodInfo method;
        protected System.Type type;

        public IAjaxProcessor(HttpContext context, System.Type type)
        {
            this.context = context;
            this.type = type;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public MethodInfo GetMethodInfo(string methodName)
        {
            if (this.method != null)
            {
                return this.method;
            }
            if ((methodName != null) && (this.type != null))
            {
                MethodInfo[] methods = this.type.GetMethods();
                bool[] flagArray = new bool[methods.Length];
                for (int i = 0; i < methods.Length; i++)
                {
                    flagArray[i] = methods[i].GetCustomAttributes(typeof(AjaxMethodAttribute), true).Length > 0;
                }
                for (int j = 0; j < methods.Length; j++)
                {
                    if (flagArray[j])
                    {
                        AjaxNamespaceAttribute[] customAttributes = (AjaxNamespaceAttribute[]) methods[j].GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
                        if ((customAttributes.Length > 0) && (customAttributes[0].ClientNamespace == methodName))
                        {
                            this.method = methods[j];
                            this.m_methodAttributes = (AjaxMethodAttribute[]) methods[j].GetCustomAttributes(typeof(AjaxMethodAttribute), true);
                            this.m_namespaceAttributes = customAttributes;
                            this.m_serverCacheAttributes = (AjaxServerCacheAttribute[]) methods[j].GetCustomAttributes(typeof(AjaxServerCacheAttribute), true);
                            return this.method;
                        }
                    }
                }
                for (int k = 0; k < methods.Length; k++)
                {
                    if (flagArray[k] && (methods[k].Name == methodName))
                    {
                        this.method = methods[k];
                        this.m_methodAttributes = (AjaxMethodAttribute[]) methods[k].GetCustomAttributes(typeof(AjaxMethodAttribute), true);
                        this.m_namespaceAttributes = (AjaxNamespaceAttribute[]) methods[k].GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
                        this.m_serverCacheAttributes = (AjaxServerCacheAttribute[]) methods[k].GetCustomAttributes(typeof(AjaxServerCacheAttribute), true);
                        return this.method;
                    }
                }
            }
            return null;
        }

        public virtual bool IsValidAjaxToken(string serverToken)
        {
            if ((Utility.Settings == null) || !Utility.Settings.TokenEnabled)
            {
                return true;
            }
            if ((HttpContext.Current == null) || (HttpContext.Current.Request == null))
            {
                return false;
            }
            string str = HttpContext.Current.Request.Headers["X-AjaxPro-Token"];
            return ((serverToken != null) && (str == serverToken));
        }

        public virtual object[] RetreiveParameters()
        {
            return null;
        }

        public virtual string SerializeObject(object o)
        {
            return "";
        }

        public virtual MethodInfo AjaxMethod
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal virtual bool CanHandleRequest
        {
            get
            {
                return false;
            }
        }

        public virtual string ContentType
        {
            get
            {
                return "text/plain";
            }
        }

        internal HttpContext Context
        {
            get
            {
                return this.context;
            }
        }

        public virtual bool IsEncryptionAble
        {
            get
            {
                return false;
            }
        }

        public virtual AjaxMethodAttribute[] MethodAttributes
        {
            get
            {
                return this.m_methodAttributes;
            }
        }

        public virtual AjaxNamespaceAttribute[] NamespaceAttributes
        {
            get
            {
                return this.m_namespaceAttributes;
            }
        }

        public virtual AjaxServerCacheAttribute[] ServerCacheAttributes
        {
            get
            {
                return this.m_serverCacheAttributes;
            }
        }

        internal System.Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

