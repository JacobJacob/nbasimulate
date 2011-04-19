namespace AjaxPro
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    public sealed class JavaScriptUtil
    {
        public static XmlDocument ConvertIJavaScriptObjectToXml(IJavaScriptObject o)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml("<root/>");
            ConvertIJavaScriptObjectToXml(document.DocumentElement, o);
            return document;
        }

        internal static void ConvertIJavaScriptObjectToXml(XmlNode n, IJavaScriptObject o)
        {
            if (o is JavaScriptArray)
            {
                XmlElement element = n.OwnerDocument.CreateElement("array");
                foreach (IJavaScriptObject obj2 in (JavaScriptArray) o)
                {
                    ConvertIJavaScriptObjectToXml(element, obj2);
                }
                n.AppendChild(element);
            }
            else if (o is JavaScriptBoolean)
            {
                XmlElement newChild = n.OwnerDocument.CreateElement("boolean");
                newChild.InnerText = (o is JavaScriptBoolean) ? "true" : "false";
                n.AppendChild(newChild);
            }
            else if (o is JavaScriptNumber)
            {
                XmlElement element3 = n.OwnerDocument.CreateElement("number");
                element3.InnerText = o.ToString();
                n.AppendChild(element3);
            }
            else if (o is JavaScriptString)
            {
                XmlElement element4 = n.OwnerDocument.CreateElement("string");
                element4.InnerText = o.ToString();
                n.AppendChild(element4);
            }
            else if (o is JavaScriptObject)
            {
                XmlElement element5 = n.OwnerDocument.CreateElement("object");
                foreach (string str in ((JavaScriptObject) o).Keys)
                {
                    XmlElement element6 = n.OwnerDocument.CreateElement("property");
                    element6.SetAttribute("name", str);
                    element5.AppendChild(element6);
                    ConvertIJavaScriptObjectToXml(element6, ((JavaScriptObject) o)[str]);
                }
                n.AppendChild(element5);
            }
        }

        public static IJavaScriptObject ConvertXmlToIJavaScriptObject(XmlDocument doc)
        {
            if (((doc == null) || (doc.DocumentElement == null)) || (doc.DocumentElement.ChildNodes.Count != 1))
            {
                return null;
            }
            return ConvertXmlToIJavaScriptObject(doc.DocumentElement.ChildNodes[0]);
        }

        internal static IJavaScriptObject ConvertXmlToIJavaScriptObject(XmlNode n)
        {
            switch (n.Name)
            {
                case "array":
                {
                    JavaScriptArray array = new JavaScriptArray();
                    foreach (XmlNode node in n.ChildNodes)
                    {
                        array.Add(ConvertXmlToIJavaScriptObject(node));
                    }
                    return array;
                }
                case "boolean":
                    return new JavaScriptBoolean(n.InnerText == "true");

                case "number":
                {
                    JavaScriptNumber number = new JavaScriptNumber();
                    number.Append(n.InnerText);
                    return number;
                }
                case "string":
                {
                    JavaScriptString str = new JavaScriptString();
                    str.Append(n.InnerText);
                    return str;
                }
                case "object":
                {
                    JavaScriptObject obj2 = new JavaScriptObject();
                    foreach (XmlNode node2 in n.SelectNodes("property"))
                    {
                        if ((node2.Attributes["name"] != null) && (node2.ChildNodes.Count == 1))
                        {
                            obj2.Add(node2.Attributes["name"].Value, ConvertXmlToIJavaScriptObject(node2.ChildNodes[0]));
                        }
                    }
                    return obj2;
                }
            }
            return null;
        }

        internal static string GetClientNamespaceRepresentation(string ns)
        {
            if (ns == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            string[] strArray = ns.Split(new char[] { '.' });
            string str = strArray[0];
            builder.Append("if(typeof " + str + " == \"undefined\") " + str + "={};\r\n");
            for (int i = 1; i < (strArray.Length - 1); i++)
            {
                str = str + "." + strArray[i];
                builder.Append("if(typeof " + str + " == \"undefined\") " + str + "={};\r\n");
            }
            return builder.ToString();
        }

        internal static string GetEnumRepresentation(Type type)
        {
            if (!type.IsEnum)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            AjaxNamespaceAttribute[] customAttributes = (AjaxNamespaceAttribute[]) type.GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
            if ((customAttributes.Length > 0) && (customAttributes[0].ClientNamespace.Replace(".", "").Length > 0))
            {
                builder.Append("addNamespace(\"" + customAttributes[0].ClientNamespace + "\");\r\n");
                builder.Append(customAttributes[0].ClientNamespace + ".");
                builder.Append(type.Name);
            }
            else
            {
                builder.Append("addNamespace(\"" + ((type.FullName.IndexOf(".") > 0) ? type.FullName.Substring(0, type.FullName.LastIndexOf(".")) : type.FullName) + "\");\r\n");
                builder.Append(type.FullName);
            }
            builder.Append(" = {\r\n");
            string[] names = Enum.GetNames(type);
            int index = 0;
            foreach (object obj2 in Enum.GetValues(type))
            {
                builder.Append("\t\"");
                builder.Append(names[index]);
                builder.Append("\":");
                builder.Append(JavaScriptSerializer.Serialize(obj2));
                if (index < (names.Length - 1))
                {
                    builder.Append(",\r\n");
                }
                index++;
            }
            builder.Append("\r\n}");
            return builder.ToString();
        }

        public static string QuoteHtmlAttribute(string s)
        {
            return QuoteString(s, '\'').Replace("\"", "&#34;");
        }

        public static string QuoteString(string s)
        {
            return QuoteString(s, '"');
        }

        internal static string QuoteString(string s, char quoteChar)
        {
            if ((s == null) || ((s.Length == 1) && (s[0] == '\0')))
            {
                return new string(quoteChar, 2);
            }
            int length = s.Length;
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char ch = s[i];
                if ((ch == '\\') || (ch == quoteChar))
                {
                    builder.Append('\\');
                    builder.Append(ch);
                    continue;
                }
                switch (ch)
                {
                    case '\b':
                    {
                        builder.Append(@"\b");
                        continue;
                    }
                    case '\t':
                    {
                        builder.Append(@"\t");
                        continue;
                    }
                    case '\n':
                    {
                        builder.Append(@"\n");
                        continue;
                    }
                    case '\f':
                    {
                        builder.Append(@"\f");
                        continue;
                    }
                    case '\r':
                    {
                        builder.Append(@"\r");
                        continue;
                    }
                }
                if (ch < ' ')
                {
                    string str = "000" + int.Parse(new string(ch, 1), NumberStyles.HexNumber);
                    builder.Append(@"\u" + str.Substring(str.Length - 4));
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return (quoteChar + builder.ToString() + quoteChar);
        }
    }
}

