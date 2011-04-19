namespace Web
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Web.UI;
    using System.Xml;
    using Web.DBData;
    using Web.MoveBill;

    public class ReceiveMobile : Page
    {
        public string inputStr;
        private int intOrderCategory = 0;
        private int intUserID = -1;
        public string Result = "ok";
        private string strMO_MESSAGE_ID;
        private string strMoveBillNumber;
        private string strUserName = "";

        private string GetMessage()
        {
            int mobileMsgIDByOID = ROOTUserManager.GetMobileMsgIDByOID(this.strUserName, this.strMO_MESSAGE_ID);
            return ("XBA快讯：" + ROOTUserManager.GetContentByMobileMsgID(mobileMsgIDByOID) + " 本条两元。");
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            Stream inputStream = this.Page.Request.InputStream;
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, (int) inputStream.Length);
            this.inputStr = Encoding.UTF8.GetString(buffer);
            base.Response.Write("ok");
            new Thread(new ThreadStart(this.SetThread)).Start();
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
                    node.ChildNodes[0].InnerText.Trim();
                    string strOID = node.ChildNodes[1].InnerText.Trim();
                    this.strMO_MESSAGE_ID = strOID;
                    node.ChildNodes[2].InnerText.Trim();
                    node.ChildNodes[3].InnerText.Trim();
                    node.ChildNodes[4].InnerText.Trim();
                    string str4 = node.ChildNodes[5].InnerText.Trim();
                    node.ChildNodes[6].InnerText.Trim();
                    node.ChildNodes[7].InnerText.Trim();
                    node.ChildNodes[8].InnerText.Trim();
                    node.ChildNodes[9].InnerText.Trim();
                    node.ChildNodes[10].InnerText.Trim();
                    node.ChildNodes[11].InnerText.Trim();
                    string strMoveBillPhone = node.ChildNodes[12].InnerText.Trim();
                    this.strMoveBillNumber = strMoveBillPhone;
                    node.ChildNodes[13].InnerText.Trim();
                    string str6 = node.ChildNodes[14].InnerText.Trim();
                    string str7 = node.ChildNodes[15].InnerText.Trim();
                    node.ChildNodes[0x10].InnerText.Trim();
                    ROOTRecManager.AddOrderRecord(this.strMO_MESSAGE_ID);
                    if (str7 == "3211")
                    {
                        if (str6.Trim().StartsWith("X"))
                        {
                            this.strUserName = str6.Trim().Substring(1, str6.Length - 1);
                            if (str4 == "1")
                            {
                                int num2;
                                this.intUserID = ROOTUserManager.GetUserIDByUserName(this.strUserName);
                                if (this.intUserID < 1)
                                {
                                    str = "false";
                                    string str8 = "未找到用户" + this.strUserName + "，无法进行点播。本条免费。";
                                    ROOTRecManager.AddOrderRecord(this.strUserName + "用户名未找到");
                                    new Send(this.strMO_MESSAGE_ID, "4320MFBZ4320M0000", "3211", this.strMoveBillNumber, str8, this.strUserName, 0, this.intOrderCategory);
                                    return str;
                                }
                                DateTime today = DateTime.Today;
                                int num = -today.Day + 1;
                                today = today.AddDays((double) num);
                                if ((this.strMoveBillNumber.Substring(0, 3).Equals("130") || this.strMoveBillNumber.Substring(0, 3).Equals("131")) || ((this.strMoveBillNumber.Substring(0, 3).Equals("132") || this.strMoveBillNumber.Substring(0, 3).Equals("133")) || this.strMoveBillNumber.Substring(0, 3).Equals("153")))
                                {
                                    num2 = 0;
                                }
                                else
                                {
                                    num2 = ROOTUserManager.CheckPayOrderByDate(strMoveBillPhone, this.intUserID, today, DateTime.Today);
                                }
                                if (num2 == 1)
                                {
                                    this.intOrderCategory = 1;
                                    ROOTUserManager.AddPayOrderByMoveBill(this.intUserID, this.strUserName, 3, strOID, "3枚金币", this.intOrderCategory, 1, 2, strMoveBillPhone, DateTime.Today);
                                    ROOTRecManager.AddOrderRecord(this.strUserName + "已提交定单");
                                    string message = this.GetMessage();
                                    new Send(this.strMO_MESSAGE_ID, "4320LQDB4320T0200", "3211", this.strMoveBillNumber, message, this.strUserName, 3, this.intOrderCategory);
                                    return str;
                                }
                                this.intOrderCategory = 0;
                                string str10 = "";
                                switch (num2)
                                {
                                    case 2:
                                        str10 = this.strUserName + "，今天您的点播数已经超过七条，敬请明天再点播。谢谢您对XBA的支持！本条免费。";
                                        break;

                                    case 3:
                                        str10 = this.strUserName + "，本月您的点播数已经超过七条，敬请下月再点播。谢谢您对XBA的支持！本条免费。";
                                        break;

                                    case 4:
                                        str10 = this.strUserName + "，今天您的帐号点播已经超过七条，敬请明天再点播。谢谢您对XBA的支持！本条免费。";
                                        break;
                                }
                                ROOTRecManager.AddOrderRecord(this.strUserName + "超过条数限制");
                                new Send(this.strMO_MESSAGE_ID, "4320MFBZ4320M0000", "3211", this.strMoveBillNumber, str10, this.strUserName, 0, this.intOrderCategory);
                            }
                            return str;
                        }
                        str = "false";
                        string strText = "XBA篮球经理用户请在用户名前添加大写字母X。本条免费。";
                        ROOTRecManager.AddOrderRecord(this.strUserName + "短信格式编写错误");
                        new Send(this.strMO_MESSAGE_ID, "4320MFBZ4320M0000", "3211", this.strMoveBillNumber, strText, this.strUserName, 0, this.intOrderCategory);
                    }
                    return str;
                }
                localName.Equals("SBMP_REPORT_MESSAGE");
            }
            catch (Exception exception)
            {
                exception.ToString();
                str = "false";
                Thread.ResetAbort();
            }
            return str;
        }

        public void SetThread()
        {
            this.Result = this.ReceiveDataFromWapdm(this.inputStr);
            ROOTRecManager.AddOrderRecord("End - " + this.Result);
        }
    }
}

