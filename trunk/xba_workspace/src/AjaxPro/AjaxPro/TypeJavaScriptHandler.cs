namespace AjaxPro
{
    using System;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Caching;
    using System.Web.SessionState;

    internal class TypeJavaScriptHandler : IHttpHandler, IReadOnlySessionState, IRequiresSessionState
    {
        private Type type;

        internal TypeJavaScriptHandler(Type type)
        {
            this.type = type;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Trace.IsEnabled)
            {
                context.Trace.Write("AjaxPro", "Render class proxy Javascript");
            }
            MethodInfo[] methods = this.type.GetMethods();
            string s = context.Request.Headers["If-None-Match"];
            string str2 = context.Request.Headers["If-Modified-Since"];
            string assemblyQualifiedName = this.type.FullName + "," + this.type.Assembly.FullName.Split(new char[] { ',' })[0];
            if (Utility.Settings.UseAssemblyQualifiedName)
            {
                assemblyQualifiedName = this.type.AssemblyQualifiedName;
            }
            if ((Utility.Settings != null) && Utility.Settings.UrlNamespaceMappings.ContainsValue(assemblyQualifiedName))
            {
                foreach (string str4 in Utility.Settings.UrlNamespaceMappings.Keys)
                {
                    if (Utility.Settings.UrlNamespaceMappings[str4].ToString() == assemblyQualifiedName)
                    {
                        assemblyQualifiedName = str4;
                        break;
                    }
                }
            }
            if (context.Cache[assemblyQualifiedName] != null)
            {
                CacheInfo info = (CacheInfo) context.Cache[assemblyQualifiedName];
                if ((s != null) && (s == info.ETag))
                {
                    context.Response.StatusCode = 0x130;
                    return;
                }
                if (str2 != null)
                {
                    if (str2.IndexOf(";") > 0)
                    {
                        str2 = str2.Split(new char[] { ';' })[0];
                    }
                    try
                    {
                        if (DateTime.Compare(Convert.ToDateTime(str2.ToString()).ToUniversalTime(), info.LastModified.ToUniversalTime()) >= 0)
                        {
                            context.Response.StatusCode = 0x130;
                            return;
                        }
                    }
                    catch (FormatException)
                    {
                        if (context.Trace.IsEnabled)
                        {
                            context.Trace.Write("AjaxPro", "The header value for If-Modified-Since = " + str2 + " could not be converted to a System.DateTime.");
                        }
                    }
                }
            }
            s = this.type.AssemblyQualifiedName;
            s = MD5Helper.GetHash(Encoding.Default.GetBytes(s));
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            context.Response.AddHeader("Content-Type", "application/x-javascript");
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetETag(s);
            context.Response.Cache.SetLastModified(date);
            StringBuilder builder = new StringBuilder();
            AjaxNamespaceAttribute[] customAttributes = (AjaxNamespaceAttribute[]) this.type.GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
            string fullName = this.type.FullName;
            if ((customAttributes.Length > 0) && (customAttributes[0].ClientNamespace != null))
            {
                fullName = customAttributes[0].ClientNamespace;
            }
            builder.Append(JavaScriptUtil.GetClientNamespaceRepresentation(fullName));
            builder.Append(fullName);
            builder.Append("_class = function() {};\r\n");
            builder.Append("Object.extend(");
            builder.Append(fullName);
            builder.Append("_class.prototype, Object.extend(new AjaxPro.AjaxClass(), {\r\n");
            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo info2 = methods[i];
                if (info2.IsPublic)
                {
                    AjaxNamespaceAttribute[] attributeArray2 = (AjaxNamespaceAttribute[]) info2.GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
                    AjaxMethodAttribute[] attributeArray3 = (AjaxMethodAttribute[]) info2.GetCustomAttributes(typeof(AjaxMethodAttribute), true);
                    if (attributeArray3.Length != 0)
                    {
                        ParameterInfo[] parameters = info2.GetParameters();
                        builder.Append("\t");
                        if (attributeArray2.Length == 0)
                        {
                            builder.Append(info2.Name);
                        }
                        else
                        {
                            builder.Append(attributeArray2[0].ClientNamespace);
                        }
                        builder.Append(": function(");
                        for (int j = 0; j < parameters.Length; j++)
                        {
                            builder.Append(parameters[j].Name);
                            if (j < (parameters.Length - 1))
                            {
                                builder.Append(", ");
                            }
                        }
                        builder.Append(") {\r\n");
                        builder.Append("\t\treturn this.invoke(\"");
                        if (attributeArray2.Length == 0)
                        {
                            builder.Append(info2.Name);
                        }
                        else
                        {
                            builder.Append(attributeArray2[0].ClientNamespace);
                        }
                        builder.Append("\", {");
                        for (int k = 0; k < parameters.Length; k++)
                        {
                            builder.Append("\"");
                            builder.Append(parameters[k].Name);
                            builder.Append("\":");
                            builder.Append(parameters[k].Name);
                            if (k < (parameters.Length - 1))
                            {
                                builder.Append(", ");
                            }
                        }
                        builder.Append("}, this.");
                        if (attributeArray2.Length == 0)
                        {
                            builder.Append(info2.Name);
                        }
                        else
                        {
                            builder.Append(attributeArray2[0].ClientNamespace);
                        }
                        builder.Append(".getArguments().slice(");
                        builder.Append(parameters.Length.ToString());
                        builder.Append("));\r\n\t},\r\n");
                    }
                }
            }
            builder.Append("\turl: '");
            string str6 = context.Request.ApplicationPath + (context.Request.ApplicationPath.EndsWith("/") ? "" : "/") + Utility.HandlerPath + "/" + Utility.GetSessionUri() + assemblyQualifiedName + Utility.HandlerExtension;
            builder.Append(str6);
            builder.Append("'\r\n");
            builder.Append("}));\r\n");
            builder.Append(fullName);
            builder.Append(" = new ");
            builder.Append(fullName);
            builder.Append("_class();\r\n");
            context.Response.Write(builder.ToString());
            context.Response.Write("\r\n");
            context.Cache.Add(assemblyQualifiedName, new CacheInfo(s, date), null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            if (context.Trace.IsEnabled)
            {
                context.Trace.Write("AjaxPro", "End ProcessRequest");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

