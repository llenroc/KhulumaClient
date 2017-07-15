using System;
using System.Collections;


namespace KhulumaClient
{
    public class ChatModel
    {
        
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
