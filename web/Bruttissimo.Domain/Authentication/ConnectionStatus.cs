namespace Bruttissimo.Domain.Authentication
{
    public enum ConnectionStatus
    {
        Faulted,
        Canceled,
        RedirectToProvider,
        Authenticated,
        InvalidCredentials
    }
}
