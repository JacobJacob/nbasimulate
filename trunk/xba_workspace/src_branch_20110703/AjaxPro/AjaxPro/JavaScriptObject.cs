namespace AjaxPro
{
    using System;
    using System.Collections.Specialized;
    using System.Reflection;

    public class JavaScriptObject : IJavaScriptObject
    {
        private StringCollection keys = new StringCollection();
        private HybridDictionary list = new HybridDictionary();

        public void Add(string key, IJavaScriptObject value)
        {
            this.keys.Add(key);
            this.list.Add(key, value);
        }

        public bool Contains(string key)
        {
            return this.list.Contains(key);
        }

        public override string ToString()
        {
            return this.Value;
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public IJavaScriptObject this[string key]
        {
            get
            {
                return (IJavaScriptObject) this.list[key];
            }
        }

        public string[] Keys
        {
            get
            {
                string[] array = new string[this.keys.Count];
                this.keys.CopyTo(array, 0);
                return array;
            }
        }

        public string Value
        {
            get
            {
                return JavaScriptSerializer.Serialize(this.list);
            }
        }
    }
}

