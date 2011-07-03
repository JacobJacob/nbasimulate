namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    public sealed class Utility
    {
        internal static bool ConverterRegistered = false;
        public static string HandlerExtension = ".ashx";
        public static string HandlerPath = "ajaxpro";
        private static AjaxSettings m_Settings = null;
        private static object m_SettingsLock = new object();
        internal static HybridDictionary pages = new HybridDictionary();

        internal static void AddConverter(AjaxSettings settings, IJavaScriptConverter converter)
        {
            AddConverter(settings, converter, false);
        }

        internal static void AddConverter(AjaxSettings settings, IJavaScriptConverter converter, bool replace)
        {
            Type type;
            for (int i = 0; i < converter.SerializableTypes.Length; i++)
            {
                type = converter.SerializableTypes[i];
                if (settings.SerializableConverters.ContainsKey(type))
                {
                    if (replace)
                    {
                        settings.SerializableConverters[type] = converter;
                    }
                }
                else
                {
                    settings.SerializableConverters.Add(type, converter);
                }
            }
            for (int j = 0; j < converter.DeserializableTypes.Length; j++)
            {
                type = converter.DeserializableTypes[j];
                if (settings.DeserializableConverters.ContainsKey(type))
                {
                    if (replace)
                    {
                        settings.DeserializableConverters[type] = converter;
                    }
                }
                else
                {
                    settings.DeserializableConverters.Add(type, converter);
                }
            }
        }

        internal static void AddDefaultConverter(AjaxSettings settings)
        {
            AddConverter(settings, new StringConverter());
            AddConverter(settings, new PrimitiveConverter());
            AddConverter(settings, new GuidConverter());
            AddConverter(settings, new ExceptionConverter());
            AddConverter(settings, new EnumConverter());
            AddConverter(settings, new DecimalConverter());
            AddConverter(settings, new NameValueCollectionConverter());
            AddConverter(settings, new DateTimeConverter());
            AddConverter(settings, new DataSetConverter());
            AddConverter(settings, new DataTableConverter());
            AddConverter(settings, new DataViewConverter());
            AddConverter(settings, new IJavaScriptObjectConverter());
            AddConverter(settings, new IListConverter());
            AddConverter(settings, new IDictionaryConverter());
            AddConverter(settings, new HashtableConverter());
            AddConverter(settings, new IEnumerableConverter());
            AddConverter(settings, new DataRowConverter());
            AddConverter(settings, new HtmlControlConverter());
        }

        [Obsolete("The recommended alternative is AjaxPro.ClientMethod.FromMethodInfo.", false)]
        public static string GetClientMethod(MethodInfo method)
        {
            ClientMethod method2 = ClientMethod.FromMethodInfo(method);
            if (method2 == null)
            {
                return null;
            }
            return (method2.ClassName + "," + method2.MethodName);
        }

        internal static ListDictionary GetScripts()
        {
            return GetScripts(false);
        }

        internal static ListDictionary GetScripts(bool RemoveFromCollection)
        {
            Guid key = (Guid) HttpContext.Current.Items["AjaxPro.pageID"];
            lock (pages.SyncRoot)
            {
                ListDictionary dictionary = (ListDictionary) pages[key];
                if (RemoveFromCollection && (dictionary != null))
                {
                    pages.Remove(key);
                }
                if (!RemoveFromCollection && (dictionary == null))
                {
                    dictionary = new ListDictionary();
                    pages[key] = dictionary;
                }
                return dictionary;
            }
        }

        internal static string GetSessionUri()
        {
            string str = "";
            if ((HttpContext.Current.Session != null) && HttpContext.Current.Session.IsCookieless)
            {
                str = "(" + HttpContext.Current.Session.SessionID + ")";
            }
            if ((str != null) && (str.Length != 0))
            {
                str = str + "/";
            }
            return str;
        }

        private static void page_PreRender(object sender, EventArgs e)
        {
            ListDictionary scripts = GetScripts(true);
            if (scripts != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("\r\n");
                foreach (string str in scripts.Values)
                {
                    builder.Append(str);
                    builder.Append("\r\n");
                }
                Page page = (Page) sender;
                if (page != null)
                {
                    page.RegisterClientScriptBlock("AjaxPro.javascript", builder.ToString());
                }
            }
        }

        internal static void RegisterClientScriptBlock(Page page, string key, string script)
        {
            Guid empty = Guid.Empty;
            if (!HttpContext.Current.Items.Contains("AjaxPro.pageID"))
            {
                empty = Guid.NewGuid();
                HttpContext.Current.Items.Add("AjaxPro.pageID", empty);
            }
            else
            {
                empty = (Guid) HttpContext.Current.Items["AjaxPro.pageID"];
            }
            page.PreRender += new EventHandler(Utility.page_PreRender);
            ListDictionary scripts = GetScripts();
            if (!scripts.Contains(key))
            {
                scripts.Add(key, script);
            }
        }

        internal static void RegisterCommonAjax()
        {
            RegisterCommonAjax((Page) HttpContext.Current.Handler);
        }

        internal static void RegisterCommonAjax(Page page)
        {
            if (page != null)
            {
                string str = HttpContext.Current.Request.ApplicationPath + (HttpContext.Current.Request.ApplicationPath.EndsWith("/") ? "" : "/");
                string str2 = str + HandlerPath + "/" + GetSessionUri() + "prototype" + HandlerExtension;
                string str3 = str + HandlerPath + "/" + GetSessionUri() + "core" + HandlerExtension;
                string str4 = str + HandlerPath + "/" + GetSessionUri() + "converter" + HandlerExtension;
                if (Settings != null)
                {
                    if (Settings.ScriptReplacements.ContainsKey("prototype"))
                    {
                        str2 = Settings.ScriptReplacements["prototype"];
                        if ((str2.Length > 0) && str2.StartsWith("~/"))
                        {
                            str2 = str + str2.Substring(2);
                        }
                    }
                    if (Settings.ScriptReplacements.ContainsKey("core"))
                    {
                        str3 = Settings.ScriptReplacements["core"];
                        if ((str3.Length > 0) && str3.StartsWith("~/"))
                        {
                            str3 = str + str3.Substring(2);
                        }
                    }
                    if (Settings.ScriptReplacements.ContainsKey("converter"))
                    {
                        str4 = Settings.ScriptReplacements["converter"];
                        if ((str4.Length > 0) && str4.StartsWith("~/"))
                        {
                            str4 = str + str4.Substring(2);
                        }
                    }
                }
                if (str2.Length > 0)
                {
                    RegisterClientScriptBlock(page, "AjaxPro.prototype", "<script type=\"text/javascript\" src=\"" + str2 + "\"></script>");
                }
                if (str3.Length > 0)
                {
                    RegisterClientScriptBlock(page, "AjaxPro.core", "<script type=\"text/javascript\" src=\"" + str3 + "\"></script>");
                }
                if (Settings.OldStyle.Contains("includeMsPrototype"))
                {
                    RegisterClientScriptBlock(page, "AjaxPro.ms", "<script type=\"text/javascript\" src=\"" + (str + HandlerPath + "/" + GetSessionUri() + "ms" + HandlerExtension) + "\"></script>");
                }
                if (str4.Length > 0)
                {
                    RegisterClientScriptBlock(page, "AjaxPro.converters", "<script type=\"text/javascript\" src=\"" + str4 + "\"></script>");
                }
                if (Settings.TokenEnabled)
                {
                    RegisterClientScriptBlock(page, "AjaxPro.token", "<script type=\"text/javascript\">AjaxPro.token = \"" + CurrentAjaxToken + "\";</script>");
                }
            }
        }

        [Obsolete("The recommended alternative is to add the converter type to web.config ajaxNet/ajaxSettings/jsonConverters.", false)]
        public static void RegisterConverterForAjax(IJavaScriptConverter converter)
        {
            AddConverter(Settings, converter);
        }

        public static void RegisterEnumForAjax(Type type)
        {
            Page handler = (Page) HttpContext.Current.Handler;
            RegisterEnumForAjax(type, handler);
        }

        public static void RegisterEnumForAjax(Type type, Page page)
        {
            RegisterCommonAjax(page);
            RegisterClientScriptBlock(page, "AjaxPro.AjaxEnum." + type.FullName, "<script type=\"text/javascript\">\r\n" + JavaScriptUtil.GetEnumRepresentation(type) + "\r\n</script>");
        }

        public static void RegisterTypeForAjax(Type type)
        {
            Page handler = (Page) HttpContext.Current.Handler;
            RegisterTypeForAjax(type, handler);
        }

        public static void RegisterTypeForAjax(Type type, Page page)
        {
            RegisterCommonAjax(page);
            string assemblyQualifiedName = type.FullName + "," + type.Assembly.FullName.Substring(0, type.Assembly.FullName.IndexOf(","));
            if (Settings.UseAssemblyQualifiedName)
            {
                assemblyQualifiedName = type.AssemblyQualifiedName;
            }
            if ((Settings != null) && Settings.UrlNamespaceMappings.ContainsValue(assemblyQualifiedName))
            {
                foreach (string str2 in Settings.UrlNamespaceMappings.Keys)
                {
                    if (Settings.UrlNamespaceMappings[str2].ToString() == assemblyQualifiedName)
                    {
                        assemblyQualifiedName = str2;
                        break;
                    }
                }
            }
            RegisterClientScriptBlock(page, "AjaxType." + type.FullName, "<script type=\"text/javascript\" src=\"" + HttpContext.Current.Request.ApplicationPath + (HttpContext.Current.Request.ApplicationPath.EndsWith("/") ? "" : "/") + HandlerPath + "/" + GetSessionUri() + assemblyQualifiedName + HandlerExtension + "\"></script>");
        }

        internal static void RemoveConverter(AjaxSettings settings, Type t)
        {
            Type current;
            bool flag = false;
            IEnumerator enumerator = settings.SerializableConverters.Keys.GetEnumerator();
            while (!flag && enumerator.MoveNext())
            {
                current = (Type) enumerator.Current;
                if (settings.SerializableConverters[current].GetType() == t)
                {
                    settings.SerializableConverters.Remove(current);
                    flag = true;
                }
            }
            flag = false;
            enumerator = settings.DeserializableConverters.Keys.GetEnumerator();
            while (!flag && enumerator.MoveNext())
            {
                current = (Type) enumerator.Current;
                if (settings.DeserializableConverters[current].GetType() == t)
                {
                    settings.DeserializableConverters.Remove(current);
                    flag = true;
                }
            }
        }

        public static string AjaxID
        {
            get
            {
                return "AjaxPro";
            }
        }

        internal static string CurrentAjaxToken
        {
            get
            {
                if ((HttpContext.Current == null) || (HttpContext.Current.Request == null))
                {
                    return null;
                }
                if (Settings == null)
                {
                    return null;
                }
                string hash = MD5Helper.GetHash(HttpContext.Current.Request.UserHostAddress);
                string str3 = MD5Helper.GetHash(HttpContext.Current.Request.UserAgent);
                string str4 = MD5Helper.GetHash("Michael Schwarz" + Settings.TokenSitePassword);
                if ((((hash == null) || (hash.Length == 0)) || ((str3 == null) || (str3.Length == 0))) || ((str4 == null) || (str4.Length == 0)))
                {
                    return null;
                }
                return MD5Helper.GetHash(hash.Substring(0, 5) + "-" + str3.Substring(0, 5) + "-" + str4.Substring(0, 5));
            }
        }

        internal static AjaxSettings Settings
        {
            get
            {
                if (m_Settings != null)
                {
                    return m_Settings;
                }
                lock (m_SettingsLock)
                {
                    if (m_Settings == null)
                    {
                        AjaxSettings config = null;
                        try
                        {
                            config = (AjaxSettings) ConfigurationSettings.GetConfig("ajaxNet/ajaxSettings");
                        }
                        catch (Exception)
                        {
                        }
                        if (config == null)
                        {
                            config = new AjaxSettings();
                            AddDefaultConverter(config);
                        }
                        m_Settings = config;
                    }
                    return m_Settings;
                }
            }
        }
    }
}

