using System;
using System.Web.Mvc;

namespace Bruttissimo.Domain
{
	public class AuthenticationResult
	{
		public ActionResult Action { get; set; }
		public string Message { get; set; }
		public Exception Exception { get; set; }
		public ConnectionStatus Status { get; set; }
		public string DisplayName { get; set; }
		public long? UserId { get; set; }
		public bool IsNewUser { get; set; }
		public bool IsNewConnection { get; set; }
	}
}