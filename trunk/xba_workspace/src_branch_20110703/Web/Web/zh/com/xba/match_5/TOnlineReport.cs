namespace Web.zh.com.xba.match_5
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;

    [DebuggerStepThrough, WebServiceBinding(Name="TOnlineReportSoap", Namespace="http://tempuri.org/"), DesignerCategory("code")]
    public class TOnlineReport : SoapHttpClientProtocol
    {
        public TOnlineReport()
        {
            base.Url = "http://n1.113388.net/WebService/TOnlineReport.asmx";
        }

        public IAsyncResult BeginHelloWorld(AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }

        public IAsyncResult BeginTOnlineReportDetail(int intTag, AsyncCallback callback, object asyncState)
        {
            return base.BeginInvoke("TOnlineReportDetail", new object[] { intTag }, callback, asyncState);
        }

        public string EndHelloWorld(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        public string EndTOnlineReportDetail(IAsyncResult asyncResult)
        {
            return (string) base.EndInvoke(asyncResult)[0];
        }

        [SoapDocumentMethod("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string HelloWorld()
        {
            return (string) base.Invoke("HelloWorld", new object[0])[0];
        }

        [SoapDocumentMethod("http://tempuri.org/TOnlineReportDetail", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public string TOnlineReportDetail(int intTag)
        {
            return (string) base.Invoke("TOnlineReportDetail", new object[] { intTag })[0];
        }
    }
}

