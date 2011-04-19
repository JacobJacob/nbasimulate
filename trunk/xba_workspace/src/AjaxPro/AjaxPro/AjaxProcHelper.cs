namespace AjaxPro
{
    using System;
    using System.Reflection;
    using System.Security;
    using System.Security.Principal;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;

    internal class AjaxProcHelper
    {
        private IAjaxProcessor p;
        private IntPtr token;
        private WindowsImpersonationContext winctx;

        internal AjaxProcHelper(IAjaxProcessor p)
        {
            this.token = IntPtr.Zero;
            this.winctx = null;
            this.p = p;
        }

        internal AjaxProcHelper(IAjaxProcessor p, IntPtr token) : this(p)
        {
            this.token = token;
        }

        internal void Run()
        {
            if (this.p.Context.Trace.IsEnabled)
            {
                this.p.Context.Trace.Write("AjaxPro", "Begin ProcessRequest");
            }
            try
            {
                if (this.token != IntPtr.Zero)
                {
                    this.winctx = WindowsIdentity.Impersonate(this.token);
                }
                this.p.Context.Response.Expires = 0;
                this.p.Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                this.p.Context.Response.ContentType = this.p.ContentType;
                this.p.Context.Response.ContentEncoding = Encoding.UTF8;
                if (!this.p.IsValidAjaxToken(Utility.CurrentAjaxToken))
                {
                    this.p.SerializeObject(new SecurityException("The AjaxPro-Token is not valid."));
                    if (this.p.Context.Trace.IsEnabled)
                    {
                        this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                    }
                }
                else
                {
                    object[] args = null;
                    object o = null;
                    try
                    {
                        args = this.p.RetreiveParameters();
                    }
                    catch (Exception exception)
                    {
                        this.p.SerializeObject(exception);
                        if (this.p.Context.Trace.IsEnabled)
                        {
                            this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                        }
                        return;
                    }
                    string key = string.Concat(new object[] { this.p.Type.FullName, "|", this.p.GetType().Name, "|", this.p.AjaxMethod.Name, "|", this.p.GetHashCode() });
                    if (this.p.Context.Cache[key] != null)
                    {
                        if (this.p.Context.Trace.IsEnabled)
                        {
                            this.p.Context.Trace.Write("AjaxPro", "Using cached result");
                        }
                        this.p.Context.Response.AddHeader("X-AjaxPro-Cache", "server");
                        this.p.Context.Response.Write(this.p.Context.Cache[key]);
                        if (this.p.Context.Trace.IsEnabled)
                        {
                            this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                        }
                    }
                    else
                    {
                        try
                        {
                            if (this.p.Context.Trace.IsEnabled)
                            {
                                this.p.Context.Trace.Write("AjaxPro", "Invoking " + this.p.Type.FullName + "." + this.p.AjaxMethod.Name);
                            }
                            if (this.p.AjaxMethod.IsStatic)
                            {
                                try
                                {
                                    o = this.p.Type.InvokeMember(this.p.AjaxMethod.Name, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase, null, null, args);
                                }
                                catch (Exception exception2)
                                {
                                    if (exception2.InnerException != null)
                                    {
                                        this.p.SerializeObject(exception2.InnerException);
                                    }
                                    else
                                    {
                                        this.p.SerializeObject(exception2);
                                    }
                                    if (this.p.Context.Trace.IsEnabled)
                                    {
                                        this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                if (this.p.Context.Trace.IsEnabled)
                                {
                                    this.p.Context.Trace.Write("AjaxPro", "Reflection Start");
                                }
                                try
                                {
                                    object obj3 = Activator.CreateInstance(this.p.Type, new object[0]);
                                    if (typeof(IContextInitializer).IsAssignableFrom(this.p.Type))
                                    {
                                        ((IContextInitializer) obj3).InitializeContext(this.p.Context);
                                    }
                                    if (obj3 != null)
                                    {
                                        o = this.p.AjaxMethod.Invoke(obj3, args);
                                    }
                                }
                                catch (Exception exception3)
                                {
                                    string str2 = string.Format("AjaxPro Error", this.p.Context.User.Identity.Name);
                                    if (exception3.InnerException != null)
                                    {
                                        this.p.SerializeObject(exception3.InnerException);
                                    }
                                    else
                                    {
                                        this.p.SerializeObject(exception3);
                                    }
                                    if (this.p.Context.Trace.IsEnabled)
                                    {
                                        this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                                    }
                                    return;
                                }
                                if (this.p.Context.Trace.IsEnabled)
                                {
                                    this.p.Context.Trace.Write("AjaxPro", "Reflection End");
                                }
                            }
                        }
                        catch (Exception exception4)
                        {
                            if (exception4.InnerException != null)
                            {
                                this.p.SerializeObject(exception4.InnerException);
                            }
                            else
                            {
                                this.p.SerializeObject(exception4);
                            }
                            if (this.p.Context.Trace.IsEnabled)
                            {
                                this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                            }
                            return;
                        }
                        try
                        {
                            if ((o != null) && (o.GetType() == typeof(XmlDocument)))
                            {
                                this.p.Context.Response.ContentType = "text/xml";
                                ((XmlDocument) o).Save(this.p.Context.Response.OutputStream);
                                if (this.p.Context.Trace.IsEnabled)
                                {
                                    this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                                }
                            }
                            else
                            {
                                string str3 = null;
                                StringBuilder builder = new StringBuilder();
                                try
                                {
                                    str3 = this.p.SerializeObject(o);
                                }
                                catch (Exception exception5)
                                {
                                    this.p.SerializeObject(exception5);
                                    if (this.p.Context.Trace.IsEnabled)
                                    {
                                        this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                                    }
                                    return;
                                }
                                if ((this.p.ServerCacheAttributes.Length > 0) && this.p.ServerCacheAttributes[0].IsCacheEnabled)
                                {
                                    this.p.Context.Cache.Add(key, str3, null, DateTime.Now.Add(this.p.ServerCacheAttributes[0].CacheDuration), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                                    if (this.p.Context.Trace.IsEnabled)
                                    {
                                        this.p.Context.Trace.Write("AjaxPro", string.Concat(new object[] { "Adding result to cache for ", this.p.ServerCacheAttributes[0].CacheDuration.TotalSeconds, " seconds (HashCode = ", this.p.GetHashCode().ToString(), ")" }));
                                    }
                                }
                                if (this.p.Context.Trace.IsEnabled)
                                {
                                    this.p.Context.Trace.Write("AjaxPro", "Result (maybe encrypted): " + str3);
                                    this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                                }
                            }
                        }
                        catch (Exception exception6)
                        {
                            this.p.SerializeObject(exception6);
                            if (this.p.Context.Trace.IsEnabled)
                            {
                                this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                            }
                        }
                    }
                }
            }
            catch (Exception exception7)
            {
                this.p.SerializeObject(exception7);
                if (this.p.Context.Trace.IsEnabled)
                {
                    this.p.Context.Trace.Write("AjaxPro", "End ProcessRequest");
                }
            }
            finally
            {
                if (this.token != IntPtr.Zero)
                {
                    this.winctx.Undo();
                }
            }
        }
    }
}

