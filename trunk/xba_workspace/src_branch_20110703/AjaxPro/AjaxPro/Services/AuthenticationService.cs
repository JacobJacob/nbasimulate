namespace AjaxPro.Services
{
    using AjaxPro;
    using System;
    using System.Web.Security;

    [AjaxNamespace("AjaxPro.Services.Authentication")]
    public class AuthenticationService
    {
        [AjaxMethod]
        public static bool Login(string username, string password)
        {
            if (FormsAuthentication.Authenticate(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }

        [AjaxMethod]
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        [AjaxMethod]
        public static bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException("ValidateUser is not yet implemented.");
        }
    }
}

