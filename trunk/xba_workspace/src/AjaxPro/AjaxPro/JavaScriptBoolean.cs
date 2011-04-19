namespace AjaxPro
{
    using System;

    public class JavaScriptBoolean : IJavaScriptObject
    {
        private bool _value;

        public JavaScriptBoolean()
        {
            this._value = false;
        }

        public JavaScriptBoolean(bool value)
        {
            this._value = false;
            this._value = value;
        }

        public static implicit operator bool(JavaScriptBoolean o)
        {
            return bool.Parse(o.Value);
        }

        public static implicit operator string(JavaScriptBoolean o)
        {
            return o.ToString();
        }

        public override string ToString()
        {
            return bool.Parse(this.Value).ToString();
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

