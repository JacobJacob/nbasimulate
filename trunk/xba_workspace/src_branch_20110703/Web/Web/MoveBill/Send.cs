namespace Web.MoveBill
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Xml;
    using Web.DBData;

    public class Send
    {
        private static int CONNECT_ERROR = -1000;
        private int intCoin;
        private int intOrderCategory;
        private static int IO_ERROR = -2000;
        private static int SEND_OK = 0;
        private string strMO_MESSAGE_ID;
        private string strUserName;
        private static int WAPDM_ERROR = -3000;

        public Send(string strbstrMoMessageID, string strbstrBusinessCode, string strbstrLongCode, string strbstrFeeMsisdn, string strText, string strUserName, int intCoin, int intOrderCategory)
        {
            this.strUserName = strUserName;
            this.strMO_MESSAGE_ID = strbstrMoMessageID;
            this.intCoin = intCoin;
            this.intOrderCategory = intOrderCategory;
            this.SmbppSendUnicodeMessage(strbstrMoMessageID, strbstrBusinessCode, strbstrLongCode, strbstrFeeMsisdn, strbstrFeeMsisdn, strText, 0, 0, 0, 0, 0, 0);
        }

        private void GiveMoney()
        {
            if (this.intOrderCategory == 1)
            {
                ROOTUserManager.AddCoinReceive(this.strUserName, this.intCoin, this.strMO_MESSAGE_ID);
                ROOTRecManager.AddOrderRecord("GiveMoney方法中：金币已发放");
            }
        }

        private int parseWAPDMResponse(string inStr)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(inStr);
                if (document.DocumentElement.LocalName.Trim().Equals("IMessageID"))
                {
                    return Convert.ToInt32(document.GetElementsByTagName("Result").Item(0).InnerText);
                }
                return WAPDM_ERROR;
            }
            catch (Exception exception)
            {
                exception.ToString();
                return WAPDM_ERROR;
            }
        }

        private int SendDataToWapdm(string destURL, string paramStr)
        {
            int num = 0;
            WebResponse response = null;
            try
            {
                ROOTRecManager.AddOrderRecord("正在发送消息...");
                string userName = "4320";
                string password = "KI34fe3D3x";
                NetworkCredential cred = new NetworkCredential(userName, password);
                new WebClient();
                CredentialCache cache = new CredentialCache();
                cache.Add(new Uri(destURL), "NTLM", cred);
                WebRequest request = WebRequest.Create(destURL);
                request.Credentials = cache;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StringBuilder builder = new StringBuilder();
                char[] anyOf = new char[] { '?', '=', '&' };
                byte[] bytes = null;
                if (paramStr != null)
                {
                    int num3;
                    for (int i = 0; i < paramStr.Length; i = num3 + 1)
                    {
                        num3 = paramStr.IndexOfAny(anyOf, i);
                        if (num3 == -1)
                        {
                            builder.Append(HttpUtility.UrlEncode(paramStr.Substring(i, paramStr.Length - i)));
                            break;
                        }
                        builder.Append(HttpUtility.UrlEncodeUnicode(paramStr.Substring(i, num3 - i)));
                        builder.ToString();
                        builder.Append(paramStr.Substring(num3, 1));
                    }
                    bytes = Encoding.UTF8.GetBytes(builder.ToString());
                    request.ContentLength = bytes.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                else
                {
                    request.ContentLength = 0L;
                }
                response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                StreamReader reader = new StreamReader(responseStream, encoding);
                char[] chArray2 = new char[0x100];
                int length = reader.Read(chArray2, 0, 0x100);
                StringBuilder builder2 = new StringBuilder();
                while (length > 0)
                {
                    string str3 = new string(chArray2, 0, length);
                    builder2.Append(str3);
                    length = reader.Read(chArray2, 0, 0x100);
                }
                builder2.Replace("\r\n", "");
                ROOTRecManager.AddOrderRecord("正常发送完毕：Result=" + this.parseWAPDMResponse(builder2.ToString()));
            }
            catch (UriFormatException exception)
            {
                exception.ToString();
                num = CONNECT_ERROR;
            }
            catch (IOException exception2)
            {
                exception2.ToString();
                num = IO_ERROR;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return num;
        }

        private void SmbppSendUnicodeMessage(string bstrMoMessageID, string bstrBusinessCode, string bstrLongCode, string bstrFeeMsisdn, string bstrDesMsisdn, string bstrMessageContent, int lTpPid, int lTpUdhi, int lSendDate, int lSendTime, int lExpireDate, int lExpireTime)
        {
            string paramStr = string.Concat(new object[] { 
                "bstrMoMessageID=", bstrMoMessageID, "&bstrBusinessCode=", bstrBusinessCode, "&bstrLongCode=", bstrLongCode, "&bstrFeeMsisdn=", bstrFeeMsisdn, "&bstrDesMsisdn=", bstrDesMsisdn, "&bstrMessageContent=", bstrMessageContent, "&lTpPid=", lTpPid, "&lTpUdhi=", lTpUdhi, 
                "&lSendDate=", lSendDate, "&lSendTime=", lSendTime, "&lExpireDate=", lExpireDate, "&lExpireTime=", lExpireTime
             });
            string destURL = "http://59.151.7.69/SmbpHttpAgent/SmbpHttpAgent.asmx/SmbppSendUnicodeMessage";
            int num = this.SendDataToWapdm(destURL, paramStr);
            if (num == SEND_OK)
            {
                ROOTRecManager.AddOrderRecord("send ok");
                this.GiveMoney();
            }
            else if (num == CONNECT_ERROR)
            {
                ROOTRecManager.AddOrderRecord("not connect");
            }
            else if (num == IO_ERROR)
            {
                ROOTRecManager.AddOrderRecord("IO Exception");
            }
            else if (num == WAPDM_ERROR)
            {
                ROOTRecManager.AddOrderRecord("wapdm error");
            }
            else
            {
                ROOTRecManager.AddOrderRecord("unknown error");
            }
        }
    }
}

