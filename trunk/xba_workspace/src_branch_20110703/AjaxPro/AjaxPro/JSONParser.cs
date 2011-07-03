namespace AjaxPro
{
    using System;
    using System.Globalization;

    public sealed class JSONParser
    {
        private char _ch = ' ';
        private int _idx = 0;
        private string _json = null;
        public const char END_OF_STRING = '\0';
        public const char JSON_ARRAY_BEGIN = '[';
        public const char JSON_ARRAY_END = ']';
        public const char JSON_DECIMAL_SEPARATOR = '.';
        public const char JSON_ITEMS_SEPARATOR = ',';
        public const char JSON_OBJECT_BEGIN = '{';
        public const char JSON_OBJECT_END = '}';
        public const char JSON_PROPERTY_SEPARATOR = ':';
        public const char JSON_STRING_DOUBLE_QUOTE = '"';
        public const char JSON_STRING_SINGLE_QUOTE = '\'';
        public const char NEW_LINE = '\n';
        public const char RETURN = '\r';

        internal JSONParser()
        {
        }

        internal bool CompareNext(string s)
        {
            if ((this._idx + s.Length) > this._json.Length)
            {
                return false;
            }
            return (this._json.Substring(this._idx, s.Length) == s);
        }

        public IJavaScriptObject GetJSONObject(string json)
        {
            this._json = json;
            this._idx = 0;
            this._ch = ' ';
            return this.GetObject();
        }

        internal IJavaScriptObject GetObject()
        {
            if (this._json == null)
            {
                throw new Exception("Missing json string.");
            }
            this.ReadWhiteSpaces();
            switch (this._ch)
            {
                case '"':
                    return this.ReadString();

                case '-':
                    return this.ReadNumber();

                case '[':
                    return this.ReadArray();

                case '{':
                    return this.ReadObject();
            }
            return (((this._ch >= '0') && (this._ch <= '9')) ? this.ReadNumber() : this.ReadWord());
        }

        internal JavaScriptArray ReadArray()
        {
            JavaScriptArray array = new JavaScriptArray();
            if (this._ch != '[')
            {
                throw new NotSupportedException("Array could not be read.");
            }
            this.ReadNext();
            this.ReadWhiteSpaces();
            if (this._ch == ']')
            {
                this.ReadNext();
                return array;
            }
            while (this._ch != '\0')
            {
                array.Add(this.GetObject());
                this.ReadWhiteSpaces();
                if (this._ch == ']')
                {
                    this.ReadNext();
                    return array;
                }
                if (this._ch != ',')
                {
                    return array;
                }
                this.ReadNext();
                this.ReadWhiteSpaces();
            }
            return array;
        }

        internal JavaScriptString ReadJavaScriptObject()
        {
            JavaScriptString str = new JavaScriptString();
            int num = 0;
            bool flag = false;
            while (this._ch != '\0')
            {
                if (this._ch == '(')
                {
                    num++;
                    flag = true;
                }
                else if (this._ch == ')')
                {
                    num--;
                }
                if (flag)
                {
                }
                str += (JavaScriptString) this._ch;
                this.ReadNext();
                if (flag && (num == 0))
                {
                    return str;
                }
            }
            return str;
        }

        internal bool ReadNext()
        {
            if (this._idx >= this._json.Length)
            {
                this._ch = '\0';
                return false;
            }
            this._ch = this._json[this._idx];
            this._idx++;
            return true;
        }

        internal JavaScriptNumber ReadNumber()
        {
            JavaScriptNumber number = new JavaScriptNumber();
            if (this._ch == '-')
            {
                number += "-";
                this.ReadNext();
            }
            while (((this._ch >= '0') && (this._ch <= '9')) && (this._ch != '\0'))
            {
                number += (JavaScriptNumber) this._ch;
                this.ReadNext();
            }
            if (this._ch == '.')
            {
                number += 0x2e;
                this.ReadNext();
                while (((this._ch >= '0') && (this._ch <= '9')) && (this._ch != '\0'))
                {
                    number += (JavaScriptNumber) this._ch;
                    this.ReadNext();
                }
            }
            if ((this._ch == 'e') || (this._ch == 'E'))
            {
                number += 0x65;
                this.ReadNext();
                if ((this._ch == '-') || (this._ch == '+'))
                {
                    number += (JavaScriptNumber) this._ch;
                    this.ReadNext();
                }
                while (((this._ch >= '0') && (this._ch <= '9')) && (this._ch != '\0'))
                {
                    number += (JavaScriptNumber) this._ch;
                    this.ReadNext();
                }
            }
            return number;
        }

        internal JavaScriptObject ReadObject()
        {
            JavaScriptObject obj2 = new JavaScriptObject();
            if (this._ch == '{')
            {
                this.ReadNext();
                this.ReadWhiteSpaces();
                if (this._ch == '}')
                {
                    this.ReadNext();
                    return obj2;
                }
                while (this._ch != '\0')
                {
                    string key = (string) this.ReadString();
                    this.ReadWhiteSpaces();
                    if (this._ch != ':')
                    {
                        break;
                    }
                    this.ReadNext();
                    obj2.Add(key, this.GetObject());
                    this.ReadWhiteSpaces();
                    if (this._ch == '}')
                    {
                        this.ReadNext();
                        return obj2;
                    }
                    if (this._ch != ',')
                    {
                        break;
                    }
                    this.ReadNext();
                    this.ReadWhiteSpaces();
                }
            }
            throw new NotSupportedException("obj");
        }

        internal bool ReadPrev()
        {
            if (this._idx <= 0)
            {
                return false;
            }
            this._idx--;
            this._ch = this._json[this._idx];
            return true;
        }

        internal JavaScriptString ReadString()
        {
            JavaScriptString str = new JavaScriptString();
            if (this._ch != '"')
            {
                throw new NotSupportedException("The string could not be read.");
            }
            while (this.ReadNext())
            {
                string str2;
                int num;
                if (this._ch == '"')
                {
                    this.ReadNext();
                    return str;
                }
                if (this._ch != '\\')
                {
                    goto Label_0128;
                }
                this.ReadNext();
                switch (this._ch)
                {
                    case 'r':
                    {
                        str += 13;
                        continue;
                    }
                    case 't':
                    {
                        str += 9;
                        continue;
                    }
                    case 'u':
                        str2 = "";
                        num = 0;
                        goto Label_00F9;

                    case 'n':
                    {
                        str += 10;
                        continue;
                    }
                    case 'f':
                    {
                        str += 12;
                        continue;
                    }
                    case '\\':
                    {
                        str += 0x5c;
                        continue;
                    }
                    case 'b':
                    {
                        str += 8;
                        continue;
                    }
                    default:
                        goto Label_0117;
                }
            Label_00DC:
                this.ReadNext();
                str2 = str2 + this._ch;
                num++;
            Label_00F9:
                if (num < 4)
                {
                    goto Label_00DC;
                }
                str += (ushort) int.Parse(str2, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                continue;
            Label_0117:
                str += (JavaScriptString) this._ch;
                continue;
            Label_0128:
                str += (JavaScriptString) this._ch;
            }
            return str;
        }

        internal void ReadWhiteSpaces()
        {
            while ((this._ch != '\0') && (this._ch <= ' '))
            {
                this.ReadNext();
            }
        }

        internal IJavaScriptObject ReadWord()
        {
            char ch = this._ch;
            switch (ch)
            {
                case 'f':
                    if (!this.CompareNext("alse"))
                    {
                        break;
                    }
                    this.ReadNext();
                    this.ReadNext();
                    this.ReadNext();
                    this.ReadNext();
                    this.ReadNext();
                    return new JavaScriptBoolean(false);

                case 'n':
                    if (this.CompareNext("ull"))
                    {
                        this.ReadNext();
                        this.ReadNext();
                        this.ReadNext();
                        this.ReadNext();
                        return null;
                    }
                    if (this.CompareNext("ew "))
                    {
                        return this.ReadJavaScriptObject();
                    }
                    break;

                default:
                    if ((ch != 't') || !this.CompareNext("rue"))
                    {
                        break;
                    }
                    this.ReadNext();
                    this.ReadNext();
                    this.ReadNext();
                    this.ReadNext();
                    return new JavaScriptBoolean(true);
            }
            throw new NotSupportedException("word " + this._ch);
        }
    }
}

