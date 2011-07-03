namespace AjaxPro
{
    using System;

    public class JavaScriptNumber : IJavaScriptObject
    {
        private string _value;

        public JavaScriptNumber()
        {
            this._value = string.Empty;
        }

        public JavaScriptNumber(double d)
        {
            this._value = string.Empty;
            this.Append(JavaScriptSerializer.Serialize(d));
        }

        public JavaScriptNumber(short i)
        {
            this._value = string.Empty;
            this.Append(JavaScriptSerializer.Serialize(i));
        }

        public JavaScriptNumber(int i)
        {
            this._value = string.Empty;
            this.Append(JavaScriptSerializer.Serialize(i));
        }

        public JavaScriptNumber(long i)
        {
            this._value = string.Empty;
            this.Append(JavaScriptSerializer.Serialize(i));
        }

        public JavaScriptNumber(float s)
        {
            this._value = string.Empty;
            this.Append(JavaScriptSerializer.Serialize(s));
        }

        internal void Append(char c)
        {
            this._value = this._value + c;
        }

        internal void Append(string s)
        {
            this._value = this._value + s;
        }

        internal int IndexOf(string s)
        {
            return this._value.IndexOf(s);
        }

        public static JavaScriptNumber operator +(JavaScriptNumber a, char c)
        {
            a.Append(c);
            return a;
        }

        public static JavaScriptNumber operator +(JavaScriptNumber a, string s)
        {
            a.Append(s);
            return a;
        }

        public static implicit operator double(JavaScriptNumber o)
        {
            return double.Parse(o.Value);
        }

        public static implicit operator short(JavaScriptNumber o)
        {
            return short.Parse(o.Value);
        }

        public static implicit operator int(JavaScriptNumber o)
        {
            return int.Parse(o.Value);
        }

        public static implicit operator long(JavaScriptNumber o)
        {
            return long.Parse(o.Value);
        }

        public static implicit operator float(JavaScriptNumber o)
        {
            return float.Parse(o.Value);
        }

        public static implicit operator string(JavaScriptNumber o)
        {
            return o.Value;
        }

        public override string ToString()
        {
            return this.Value;
        }

        public string Value
        {
            get
            {
                return this._value;
            }
        }
    }
}

