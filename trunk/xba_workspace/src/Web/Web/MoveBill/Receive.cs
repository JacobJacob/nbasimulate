namespace Web.MoveBill
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Web.DBData;

    public class Receive
    {
        private int intUserID = -1;
        public string Result = "ok";
        private string strUserName;

        public Receive(Stream stmInput)
        {
            Stream stream = stmInput;
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);
            string input = Encoding.UTF8.GetString(buffer);
            this.Result = this.ReceiveDataFromWapdm(input);
            Console.WriteLine(this.Result);
        }

        private string ReceiveDataFromWapdm(string input)
        {
            string str = "ok";
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(input);
                string localName = document.DocumentElement.LocalName;
                if (!localName.Equals("SBMP_MO_MESSAGE"))
                {
                    goto Label_026E;
                }
                XmlNode node = document.SelectSingleNode("SBMP_MO_MESSAGE");
                Console.WriteLine(node.ChildNodes[0].InnerText);
                string innerText = node.ChildNodes[1].InnerText;
                Console.WriteLine(innerText);
                Console.WriteLine(node.ChildNodes[2].InnerText);
                Console.WriteLine(node.ChildNodes[3].InnerText);
                Console.WriteLine(node.ChildNodes[4].InnerText);
                string str4 = node.ChildNodes[5].InnerText;
                Console.WriteLine(str4);
                Console.WriteLine(node.ChildNodes[6].InnerText);
                Console.WriteLine(node.ChildNodes[7].InnerText);
                Console.WriteLine(node.ChildNodes[8].InnerText);
                Console.WriteLine(node.ChildNodes[9].InnerText);
                Console.WriteLine(node.ChildNodes[10].InnerText);
                Console.WriteLine(node.ChildNodes[11].InnerText);
                string str5 = node.ChildNodes[12].InnerText;
                Console.WriteLine(str5);
                Console.WriteLine(node.ChildNodes[13].InnerText);
                string str6 = node.ChildNodes[14].InnerText;
                Console.WriteLine(str6);
                string str7 = node.ChildNodes[15].InnerText;
                Console.WriteLine(str7);
                Console.WriteLine(node.ChildNodes[0x10].InnerText);
                int intType = 0;
                int intMessageCount = 0;
                if (str7 == "2706")
                {
                    intType = 0;
                    intMessageCount = 1;
                    this.strUserName = str6.Trim();
                    if (!ROOTUserManager.HasNickName(this.strUserName))
                    {
                        return "NoUserName";
                    }
                }
                else
                {
                    string str9 = str6;
                    if (str9 != null)
                    {
                        if (str9 == "KBF")
                        {
                            intType = 1;
                            intMessageCount = 10;
                        }
                        else if (str9 == "QXKBF")
                        {
                            goto Label_023B;
                        }
                    }
                }
                goto Label_0241;
            Label_023B:
                intType = 2;
                intMessageCount = 0;
            Label_0241:
                if (str4 == "1")
                {
                    ROOTMoveBillManager.AddMoveBill(innerText, str5, this.intUserID, this.strUserName, intType, 0, intMessageCount);
                }
                return str;
            Label_026E:
                localName.Equals("SBMP_REPORT_MESSAGE");
            }
            catch (Exception exception)
            {
                str = "false";
                Console.WriteLine(exception.ToString());
            }
            return str;
        }
    }
}

