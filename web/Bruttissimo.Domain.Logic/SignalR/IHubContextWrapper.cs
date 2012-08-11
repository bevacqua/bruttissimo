using SignalR.Hubs;

namespace Bruttissimo.Domain
{
    public interface IHubContextWrapper<THub> where THub : IHub
    {
        IHubContext Context { get; }
    }
}