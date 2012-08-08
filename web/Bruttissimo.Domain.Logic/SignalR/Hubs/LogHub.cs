using SignalR.Hubs;

namespace Bruttissimo.Domain.Logic.Hubs
{
	[HubName("logs")]
	public class LogHub : Hub
	{
		[HubMethodName("testMessage")]
		public void TestMessage(string message)
		{
			Clients.testBcast(Context.ConnectionId, message); // why U no come back??
		}
	}
}