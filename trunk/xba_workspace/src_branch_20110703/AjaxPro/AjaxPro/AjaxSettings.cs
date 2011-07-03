namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    internal class AjaxSettings
    {
        internal Hashtable DeserializableConverters = new Hashtable();
        private AjaxEncryption m_AjaxEncryption = null;
        private string m_CoreScript = null;
        private bool m_DebugEnabled = false;
        private StringCollection m_OldStyle = new StringCollection();
        private bool m_OnlyAllowTypesInList = false;
        private StringDictionary m_ScriptReplacements = new StringDictionary();
        private bool m_TokenEnabled = false;
        private string m_TokenSitePassword = "ajaxpro";
        private Hashtable m_UrlNamespaceMappings = new Hashtable();
        private bool m_UseAssemblyQualifiedName = false;
        internal Hashtable SerializableConverters = new Hashtable();

        internal AjaxSettings()
        {
        }

        [Obsolete("The recommended alternative is to configure a scriptReplacement/file in web.config.", true)]
        internal string CoreScript
        {
            get
            {
                return this.m_CoreScript;
            }
            set
            {
                this.m_CoreScript = value;
            }
        }

        internal bool DebugEnabled
        {
            get
            {
                return this.m_DebugEnabled;
            }
            set
            {
                this.m_DebugEnabled = value;
            }
        }

        internal AjaxEncryption Encryption
        {
            get
            {
                return this.m_AjaxEncryption;
            }
            set
            {
                this.m_AjaxEncryption = value;
            }
        }

        internal StringCollection OldStyle
        {
            get
            {
                return this.m_OldStyle;
            }
            set
            {
                this.m_OldStyle = value;
            }
        }

        internal bool OnlyAllowTypesInList
        {
            get
            {
                return this.m_OnlyAllowTypesInList;
            }
            set
            {
                this.m_OnlyAllowTypesInList = value;
            }
        }

        internal StringDictionary ScriptReplacements
        {
            get
            {
                return this.m_ScriptReplacements;
            }
            set
            {
                this.m_ScriptReplacements = value;
            }
        }

        internal bool TokenEnabled
        {
            get
            {
                return this.m_TokenEnabled;
            }
            set
            {
                this.m_TokenEnabled = value;
            }
        }

        internal string TokenSitePassword
        {
            get
            {
                return this.m_TokenSitePassword;
            }
            set
            {
                this.m_TokenSitePassword = value;
            }
        }

        internal Hashtable UrlNamespaceMappings
        {
            get
            {
                return this.m_UrlNamespaceMappings;
            }
            set
            {
                this.m_UrlNamespaceMappings = value;
            }
        }

        internal bool UseAssemblyQualifiedName
        {
            get
            {
                return this.m_UseAssemblyQualifiedName;
            }
            set
            {
                this.m_UseAssemblyQualifiedName = value;
            }
        }
    }
}

