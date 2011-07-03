namespace AjaxPro
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class JavaScriptArray : ArrayList, IJavaScriptObject
    {
        public JavaScriptArray()
        {
        }

        public JavaScriptArray(IJavaScriptObject[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                this.Add(items[i]);
            }
        }

        public int Add(IJavaScriptObject value)
        {
            return base.Add(value);
        }

        public override int Add(object value)
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.Value;
        }

        public IJavaScriptObject this[int index]
        {
            get
            {
                return (IJavaScriptObject) base[index];
            }
        }

        public string Value
        {
            get
            {
                return JavaScriptSerializer.Serialize(this.ToArray());
            }
        }
    }
}

