using System;
using System.Collections;
using SQLite;

namespace KhulumaClient
{
    public class ChatModel
    {
        [PrimaryKey]
        public int ChatMessageAPIModelId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public string Name { get; set; }
        public string Message { get; set; }


        public string MessageTimestamp { get; set; }

		public ChatModel()
		{
		}
	}
}
