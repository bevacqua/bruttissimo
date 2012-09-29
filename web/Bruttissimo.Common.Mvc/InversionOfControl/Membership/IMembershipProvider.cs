namespace Bruttissimo.Common.Mvc.InversionOfControl.Membership
{
    public interface IMembershipProvider
    {
        bool ValidateUser(string username, string password);
    }
}
