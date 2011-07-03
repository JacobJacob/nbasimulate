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
                if (localName.Equals("SBMP_MO_MESSAGE"))
                {
                    XmlNode node = document.SelectSingleNode("SBMP_MO_MESSAGE");
                    Console.WriteLine(node.ChildNodes[0].InnerText);
                    string innerText = node.ChildNodes[1].InnerText;
                    Console.WriteLine(innerText);
                    Console.WriteLine(node.ChildNodes[2].InnerText);
                    Console.WriteLine(node.ChildNodes[3].InnerText);
                    Console.WriteLine(node.ChildNodes[4].InnerText);
                    string str8 = node.ChildNodes[5].InnerText;
                    Console.WriteLine(str8);
                    Console.WriteLine(node.ChildNodes[6].InnerText);
                    Console.WriteLine(node.ChildNodes[7].InnerText);
                    Console.WriteLine(node.ChildNodes[8].InnerText);
                    Console.WriteLine(node.ChildNodes[9].InnerText);
                    Console.WriteLine(node.ChildNodes[10].InnerText);
                    Console.WriteLine(node.ChildNodes[11].InnerText);
                    string str15 = node.ChildNodes[12].InnerText;
                    Console.WriteLine(str15);
                    Console.WriteLine(node.ChildNodes[13].InnerText);
                    string str17 = node.ChildNodes[14].InnerText;
                    Console.WriteLine(str17);
                    string str18 = node.ChildNodes[15].InnerText;
                    Console.WriteLine(str18);
                    Console.WriteLine(node.ChildNodes[0x10].InnerText);
                    int intType = 0;
                    int intMessageCount = 0;
                    if (str18 == "2706")
                    {
                        intType = 0;
                        intMessageCount = 1;
                        this.strUserName = str17.Trim();
                        if (!ROOTUserManager.HasNickName(this.strUserName))
                        {
                            return "NoUserName";
                        }
                    }
                    else
                    {
                        switch (str17)
                        {
                            case "KBF":
                                intType = 1;
                                intMessageCount = 10;
                                break;

                            case "QXKBF":
                                intType = 2;
                                intMessageCount = 0;
                                break;
                        }
                    }
                    if (str8 == "1")
                    {
                        ROOTMoveBillManager.AddMoveBill(innerText, str15, this.intUserID, this.strUserName, intType, 0, intMessageCount);
                    }
                    return str;
                }
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

