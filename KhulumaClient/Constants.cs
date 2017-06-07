using System;
namespace KhulumaClient
{
	public class Constants
	{
		public Constants()
		{
		}

		public static string Username = "user";
		public static string Password = "pass";

        public static string baseUri = "http://khulumalive.azurewebsites.net/";
        //public static string baseUri = "http://localhost:50927";

        
		public static string apiLocationsUrl = "/api/APILocationModels";
		public static string apiAppUsersUrl = "/api/APIAppUserModels";
		public static string apiChatMessagesUrl = "/api/APIChatMessage";

		public static string apiMobilePagesUrl = "/api/APIMobilePagesModels";

		public static string apiFlaggedContentUrl = "/api/APIFlaggedContentModels";


	}
}
