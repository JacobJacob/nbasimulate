namespace Web.zh.com.cga.xba.match1
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;

    [WebServiceBinding(Name="VOnlineReportSoap", Namespace="http://tempuri.org/"), DebuggerStepThrough, DesignerCategory("code")]
    public class VOnlineReport : SoapHttpClientProtocol
    {
        public VOnlineReport()
        {
            base.Url = "http://match1.xba.cga.com.cn/WebService/VOnlineReport.asmx";
        }

        public IAsyncResult BeginHelloWorld(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }

        public IAsyncResult BeginVOnlineReportDetail(int intTag, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("VOnlineReportDetail", new object[] { intTag }, callback, asyncState);
        }

        public string EndHelloWorld(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public string EndVOnlineReportDetail(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        [SoapDocumentMethod("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string HelloWorld()
        {
            return (string) base.Invoke("HelloWorld", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/VOnlineReportDetail", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string VOnlineReportDetail(int intTag)
        {
            return (string) base.Invoke("VOnlineReportDetail", new object[] { intTag })[0];
        }
    }
}

