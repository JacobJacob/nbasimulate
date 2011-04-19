namespace AjaxPro
{
    using System;

    public class JavaScriptString : IJavaScriptObject
    {
        private string _value;

        public JavaScriptString()
        {
            this._value = string.Empty;
        }

        public JavaScriptString(char c)
        {
            this._value = string.Empty;
            this.Append(c);
        }

        public JavaScriptString(string s)
        {
            this._value = string.Empty;
            this.Append(s);
        }

        internal void Append(char c)
        {
            this._value = this._value + c;
        }

        internal void Append(string s)
        {
            this._value = this._value + s;
        }

        public static JavaScriptString operator +(JavaScriptString a, char c)
        {
            a.Append(c);
            return a;
        }

        public static JavaScriptString operator +(JavaScriptString a, string s)
        {
            a.Append(s);
            return a;
        }

        public static implicit operator string(JavaScriptString o)
        {
            return o.ToString();
        }

        public override string ToString()
        {
            return this._value;
        }

        public string Value
        {
            get
            {
                return JavaScriptSerializer.Serialize(this._value);
            }
        }
    }
}

