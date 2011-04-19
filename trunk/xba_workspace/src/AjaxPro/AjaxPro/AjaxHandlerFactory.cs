namespace AjaxPro
{
    using System;
    using System.IO;
    using System.Web;

    public class AjaxHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(context.Request.Path);
            Type type = null;
            Exception exception = null;
            bool flag = false;
            try
            {
                if ((Utility.Settings != null) && Utility.Settings.UrlNamespaceMappings.Contains(fileNameWithoutExtension))
                {
                    flag = true;
                    type = Type.GetType(Utility.Settings.UrlNamespaceMappings[fileNameWithoutExtension].ToString(), true);
                }
                if (type == null)
                {
                    type = Type.GetType(fileNameWithoutExtension, true);
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
            }
            switch (requestType)
            {
                case "GET":
                    switch (fileNameWithoutExtension.ToLower())
                    {
                        case "prototype":
                            return new EmbeddedJavaScriptHandler("prototype");

                        case "core":
                            return new EmbeddedJavaScriptHandler("core");

                        case "ms":
                            return new EmbeddedJavaScriptHandler("ms");

                        case "prototype-core":
                        case "core-prototype":
                            return new EmbeddedJavaScriptHandler("prototype,core");

                        case "converter":
                            return new ConverterJavaScriptHandler();
                    }
                    if (exception != null)
                    {
                        return null;
                    }
                    if (Utility.Settings.OnlyAllowTypesInList && !flag)
                    {
                        return null;
                    }
                    return new TypeJavaScriptHandler(type);

                case "POST":
                {
                    if (Utility.Settings.OnlyAllowTypesInList && !flag)
                    {
                        return null;
                    }
                    IAjaxProcessor[] processorArray = new IAjaxProcessor[] { new XmlHttpRequestProcessor(context, type), new IFrameProcessor(context, type) };
                    for (int i = 0; i < processorArray.Length; i++)
                    {
                        if (processorArray[i].CanHandleRequest)
                        {
                            if (exception != null)
                            {
                                processorArray[i].SerializeObject(new NotSupportedException("This method is either not marked with an AjaxMethod or is not available."));
                                return null;
                            }
                            AjaxMethodAttribute[] customAttributes = (AjaxMethodAttribute[]) processorArray[i].AjaxMethod.GetCustomAttributes(typeof(AjaxMethodAttribute), true);
                            bool useAsyncProcessing = false;
                            HttpSessionStateRequirement readWrite = HttpSessionStateRequirement.ReadWrite;
                            if (Utility.Settings.OldStyle.Contains("sessionStateDefaultNone"))
                            {
                                readWrite = HttpSessionStateRequirement.None;
                            }
                            if (customAttributes.Length > 0)
                            {
                                useAsyncProcessing = customAttributes[0].UseAsyncProcessing;
                                if (customAttributes[0].RequireSessionState != HttpSessionStateRequirement.UseDefault)
                                {
                                    readWrite = customAttributes[0].RequireSessionState;
                                }
                            }
                            switch (readWrite)
                            {
                                case HttpSessionStateRequirement.ReadWrite:
                                    if (useAsyncProcessing)
                                    {
                                        return new AjaxAsyncHttpHandlerSession(processorArray[i]);
                                    }
                                    return new AjaxSyncHttpHandlerSession(processorArray[i]);

                                case HttpSessionStateRequirement.Read:
                                    if (useAsyncProcessing)
                                    {
                                        return new AjaxAsyncHttpHandlerSessionReadOnly(processorArray[i]);
                                    }
                                    return new AjaxSyncHttpHandlerSessionReadOnly(processorArray[i]);

                                case HttpSessionStateRequirement.None:
                                    if (useAsyncProcessing)
                                    {
                                        return new AjaxAsyncHttpHandler(processorArray[i]);
                                    }
                                    return new AjaxSyncHttpHandler(processorArray[i]);
                            }
                            if (!useAsyncProcessing)
                            {
                                return new AjaxSyncHttpHandlerSession(processorArray[i]);
                            }
                            return new AjaxAsyncHttpHandlerSession(processorArray[i]);
                        }
                    }
                    break;
                }
            }
            return null;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}

