namespace Web.Helper
{
    using System;
    using System.Text.RegularExpressions;

    public class Cuter
    {
        private string strCuter;
        private string strSplit;

        public Cuter(string cuter)
        {
            this.strCuter = cuter;
            this.strSplit = ",";
        }

        public Cuter(string cuter, string split)
        {
            this.strCuter = cuter;
            this.strSplit = split;
        }

        public void AddItem(string item)
        {
            this.strCuter = this.strCuter + item + this.strSplit;
        }

        public void DelItem(string item)
        {
            string[] arrCuter = this.GetArrCuter();
            int length = arrCuter.Length;
            this.strCuter = "";
            for (int i = 0; i < length; i++)
            {
                if (item != arrCuter[i])
                {
                    this.strCuter = this.strCuter + arrCuter[i];
                    if (i < (length - 1))
                    {
                        this.strCuter = this.strCuter + this.strSplit;
                    }
                }
            }
            if (this.strCuter.Substring(this.strCuter.Length - 1, 1) == this.strSplit)
            {
                this.strCuter = this.strCuter.Substring(0, this.strCuter.Length - 1);
            }
        }

        public string[] GetArrCuter()
        {
            return Regex.Split(this.strCuter, this.strSplit);
        }

        public string GetCuter()
        {
            return this.strCuter;
        }

        public string GetCuter(int x)
        {
            return this.GetArrCuter()[x];
        }

        public int GetIndex(string item)
        {
            string[] arrCuter = this.GetArrCuter();
            int length = arrCuter.Length;
            for (int i = 0; i < length; i++)
            {
                if (item == arrCuter[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSize()
        {
            return this.GetArrCuter().Length;
        }

        public void SetCuter(int x, string s)
        {
            string[] arrCuter = this.GetArrCuter();
            arrCuter[x] = s;
            string str = "";
            for (int i = 0; i < this.GetSize(); i++)
            {
                if (i != (this.GetSize() - 1))
                {
                    str = str + arrCuter[i] + this.strSplit;
                }
                else
                {
                    str = str + arrCuter[i];
                }
            }
            this.strCuter = str;
        }

        public override string ToString()
        {
            return this.strCuter;
        }
    }
}

