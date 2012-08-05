using System;

namespace Bruttissimo.Domain.Entity
{
	public class Log
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Thread { get; set; }
		public string Level { get; set; }
		public string Logger { get; set; }
		public string Message { get; set; }
		public string Exception { get; set; }
		public string StackTrace { get; set; }
	}
}
