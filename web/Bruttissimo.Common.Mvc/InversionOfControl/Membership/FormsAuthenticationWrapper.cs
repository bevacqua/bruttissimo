using System.Web.Security;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Membership
{
    public class FormsAuthenticationWrapper : IFormsAuthentication
    {
        public void SetAuthCookie(string username, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        public void SetAuthCookie(string username, bool createPersistentCookie, string cookiePath)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie, cookiePath);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
