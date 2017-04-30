using System;
using System.Collections;
using SQLite;

namespace KhulumaClient
{
	public class ChatModel
	{
		[PrimaryKey]
		public int ChatId { get; set; }
		public int UserId { get; set; }
		public int GroupId { get; set; }
		public DateTime TimeStamp { get; set; }
		public string timestampString { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }

		public ChatModel()
		{
		}
	}
}
