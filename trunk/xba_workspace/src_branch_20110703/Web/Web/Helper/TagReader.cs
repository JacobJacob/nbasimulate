namespace Web.Helper
{
    using System;
    using System.Collections;

    public class TagReader
    {
        private string line;

        public TagReader()
        {
        }

        public TagReader(string line)
        {
            this.line = line;
        }

        public ArrayList GetItems(string tag)
        {
            string openTag = "<" + tag + ">";
            string closeTag = "</" + tag + ">";
            ArrayList list = new ArrayList();
            while (this.HasItems(openTag))
            {
                string tagline = this.GetTagline(openTag, closeTag);
                list.Add(tagline);
                int num = this.line.Length - 1;
                int index = this.line.IndexOf(closeTag);
                string str4 = this.line.Substring(index + 1, num - index);
                this.line = str4;
            }
            return list;
        }

        public ArrayList GetItems(string openTag, string closeTag)
        {
            ArrayList list = new ArrayList();
            while (this.HasItems(openTag))
            {
                string tagline = this.GetTagline(openTag, closeTag);
                list.Add(tagline);
                int num = this.line.Length - 1;
                int index = this.line.IndexOf(closeTag);
                string str2 = this.line.Substring(index + 1, num - index);
                this.line = str2;
            }
            return list;
        }

        public ArrayList GetItems(string line, string openTag, string closeTag)
        {
            ArrayList list = new ArrayList();
            while (this.HasItems(line, openTag))
            {
                string str = this.GetTagline(line, openTag, closeTag);
                list.Add(str);
                int num = line.Length - 1;
                int index = line.IndexOf(closeTag);
                line = line.Substring(index + 1, num - index);
            }
            return list;
        }

        public string GetTagline(string tag)
        {
            string str = "<" + tag + ">";
            string str2 = "</" + tag + ">";
            int length = str.Length;
            int index = this.line.IndexOf(str);
            int num3 = this.line.IndexOf(str2);
            if ((index >= 0) && (num3 >= 0))
            {
                return this.line.Substring(index + length, (num3 - index) - length);
            }
            return "";
        }

        public string GetTagline(string openTag, string closeTag)
        {
            int length = openTag.Length;
            int index = this.line.IndexOf(openTag);
            int num3 = this.line.IndexOf(closeTag);
            if ((index >= 0) && (num3 >= 0))
            {
                return this.line.Substring(index + length, (num3 - index) - length);
            }
            return "";
        }

        public string GetTagline(string line, string openTag, string closeTag)
        {
            int length = openTag.Length;
            int index = line.IndexOf(openTag);
            int num3 = line.IndexOf(closeTag);
            if ((index >= 0) && (num3 >= 0))
            {
                return line.Substring(index + length, (num3 - index) - length);
            }
            return "";
        }

        public bool HasItems(string openTag)
        {
            return (this.line.IndexOf(openTag) != -1);
        }

        public bool HasItems(string line, string openTag)
        {
            return (line.IndexOf(openTag) != -1);
        }

        public bool IsCloseTag()
        {
            return (this.line.StartsWith("</") && this.line.EndsWith(">"));
        }

        public bool IsCloseTag(string line)
        {
            return (line.StartsWith("</") && line.EndsWith(">"));
        }

        public bool IsOpenTag()
        {
            return ((this.line.StartsWith("<") && !this.line.StartsWith("</")) && this.line.EndsWith(">"));
        }

        public bool IsOpenTag(string line)
        {
            return ((line.StartsWith("<") && !line.StartsWith("</")) && line.EndsWith(">"));
        }

        public bool IsTag()
        {
            if (!this.IsOpenTag() && !this.IsCloseTag())
            {
                return false;
            }
            return true;
        }

        public bool IsTag(string line)
        {
            if (!this.IsOpenTag(line) && !this.IsCloseTag(line))
            {
                return false;
            }
            return true;
        }
    }
}

