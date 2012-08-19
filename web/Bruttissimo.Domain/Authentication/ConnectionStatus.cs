namespace Bruttissimo.Domain
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
