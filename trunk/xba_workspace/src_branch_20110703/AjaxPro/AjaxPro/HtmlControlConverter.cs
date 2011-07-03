namespace AjaxPro
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class HtmlControlConverter : IJavaScriptConverter
    {
        public HtmlControlConverter()
        {
            base.m_serializableTypes = new Type[] { typeof(HtmlControl), typeof(HtmlAnchor), typeof(HtmlButton), typeof(HtmlImage), typeof(HtmlInputButton), typeof(HtmlInputCheckBox), typeof(HtmlInputRadioButton), typeof(HtmlInputText), typeof(HtmlSelect), typeof(HtmlTableCell), typeof(HtmlTable), typeof(HtmlTableRow), typeof(HtmlTextArea) };
            base.m_deserializableTypes = new Type[] { typeof(HtmlControl), typeof(HtmlAnchor), typeof(HtmlButton), typeof(HtmlImage), typeof(HtmlInputButton), typeof(HtmlInputCheckBox), typeof(HtmlInputRadioButton), typeof(HtmlInputText), typeof(HtmlSelect), typeof(HtmlTableCell), typeof(HtmlTable), typeof(HtmlTableRow), typeof(HtmlTextArea) };
        }

        internal static string AddRunAtServer(string input, string tagName)
        {
            Match match = new Regex("<" + Regex.Escape(tagName) + @"[^>]*?(?<InsertPos>\s*)/?>", RegexOptions.Singleline | RegexOptions.IgnoreCase).Match(input);
            if (match.Success)
            {
                Group group = match.Groups["InsertPos"];
                return input.Insert(group.Index + group.Length, " runat=\"server\"");
            }
            return input;
        }

        internal static string CorrectAttributes(string input)
        {
            string pattern = "selected=\"selected\"";
            input = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase).Replace(input, "selected=\"true\"");
            pattern = "multiple=\"multiple\"";
            input = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase).Replace(input, "multiple=\"true\"");
            pattern = "disabled=\"disabled\"";
            input = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase).Replace(input, "disabled=\"true\"");
            return input;
        }

        public override object Deserialize(IJavaScriptObject o, Type t)
        {
            if (!typeof(HtmlControl).IsAssignableFrom(t) || !(o is JavaScriptString))
            {
                throw new NotSupportedException();
            }
            return HtmlControlFromString(o.ToString(), t);
        }

        internal static HtmlControl HtmlControlFromString(string html, Type type)
        {
            if (!typeof(HtmlControl).IsAssignableFrom(type))
            {
                throw new InvalidCastException("The target type is not a HtmlControlType");
            }
            html = AddRunAtServer(html, (Activator.CreateInstance(type) as HtmlControl).TagName);
            if (type.IsAssignableFrom(typeof(HtmlSelect)))
            {
                html = CorrectAttributes(html);
            }
            Control control = HtmlControlConverterHelper.Parse(html);
            if (control.GetType() == type)
            {
                return (control as HtmlControl);
            }
            foreach (Control control2 in control.Controls)
            {
                if (control2.GetType() == type)
                {
                    return (control2 as HtmlControl);
                }
            }
            return null;
        }

        internal static string HtmlControlToString(HtmlControl control)
        {
            StringWriter writer = new StringWriter(new StringBuilder());
            control.RenderControl(new HtmlTextWriter(writer));
            return JavaScriptSerializer.Serialize(writer.ToString());
        }

        public override string Serialize(object o)
        {
            if (!(o is Control))
            {
                throw new NotSupportedException();
            }
            return HtmlControlToString((HtmlControl) o);
        }
    }
}

