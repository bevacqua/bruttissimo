namespace Bruttissimo.Common.Mvc
{
    public interface IMembershipProvider
    {
        bool ValidateUser(string username, string password);
    }
}
