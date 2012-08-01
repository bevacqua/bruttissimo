using SignalR.Hubs;

namespace Bruttissimo.Domain.Logic.Hubs
{
	[HubName("posts")]
	public class PostHub : Hub
	{
		[HubMethodName("testMessage")]
		public void TestMessage(string post)
		{
			Clients.postCreated(Context.ConnectionId, post);
		}
	}
}