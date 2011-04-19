namespace AjaxPro
{
    using System;
    using System.Reflection;

    public class ClientMethod
    {
        public string ClassName;
        public string MethodName;

        public static ClientMethod FromDelegate(Delegate d)
        {
            if (d == null)
            {
                return null;
            }
            return FromMethodInfo(d.Method);
        }

        public static ClientMethod FromMethodInfo(MethodInfo method)
        {
            if (method.GetCustomAttributes(typeof(AjaxMethodAttribute), true).Length == 0)
            {
                return null;
            }
            AjaxNamespaceAttribute[] customAttributes = (AjaxNamespaceAttribute[]) method.ReflectedType.GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
            AjaxNamespaceAttribute[] attributeArray2 = (AjaxNamespaceAttribute[]) method.GetCustomAttributes(typeof(AjaxNamespaceAttribute), true);
            ClientMethod method2 = new ClientMethod();
            if (customAttributes.Length > 0)
            {
                method2.ClassName = customAttributes[0].ClientNamespace;
            }
            else
            {
                method2.ClassName = method.ReflectedType.FullName;
            }
            if (attributeArray2.Length > 0)
            {
                method2.MethodName = method2.MethodName + attributeArray2[0].ClientNamespace;
            }
            else
            {
                method2.MethodName = method2.MethodName + method.Name;
            }
            return method2;
        }

        public override string ToString()
        {
            return (this.ClassName + "." + this.MethodName);
        }
    }
}

