using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using KhulumaClient.Models;

namespace KhulumaClient
{
	public class RestServiceImplementation : IRestService
	{

		HttpClient client;

		public List<locationModel> Locations { get; private set; }
		public List<MobilePageModel> MobilePages { get; private set; }
		public List<FlaggedContentModel> FlaggedContent { get; private set; }
		public List<ChatModel> Chats { get; private set;}

        public userModel thisUser { get; set; }

		public RestServiceImplementation() 
		{
			var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

        //Get User
        public async Task<userModel> GetThisUser()
        {
            thisUser = new userModel();

            var userIDSetting = Helpers.Settings.id;

            if (userIDSetting == 0)
            {
                userIDSetting = 1;
            }

            var thisUserID = userIDSetting;
            var uri = new Uri(string.Format(Constants.baseUri + Constants.apiAppUsersUrl + "/" + thisUserID));

            try
            {
                var response = await client.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    thisUser = JsonConvert.DeserializeObject<userModel>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return thisUser;
        }


		// Get locations
		public async Task<List<locationModel>> GetLocationsAsync()
		{
			Locations = new List<locationModel>();
			var uri = new Uri(string.Format(Constants.baseUri + Constants.apiLocationsUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					Locations = JsonConvert.DeserializeObject<List<locationModel>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR: {0}", ex.Message);
			}

			return Locations;

		}

		// Get Chat Messages
		public async Task<List<ChatModel>> GetChatsAsync()
		{
			Chats = new List<ChatModel>();

            var userID = Helpers.Settings.id;
            var groupID = Helpers.Settings.GroupId;

            if (userID == 0)
            {
                userID = 1;
            }
   



            var groupChatsListurl = "/APIChatMessages/" + groupID + "/groupMessages";

            var uri = new Uri(string.Format(Constants.baseUri + groupChatsListurl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					Chats = JsonConvert.DeserializeObject<List<ChatModel>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR: {0}", ex.Message);
			}

            if (Chats.Count < 1)
            {
                Chats.Add(new ChatModel() {
                    Name="Khuluma",
                    Message="Welcome to the group, there are no messages yet."
                });
            }

			return Chats;
		}


		//Create App User Account
		public async Task<AppUserModel> PostUser(PostUserModel user)
		{
			Debug.WriteLine("Post User: ");
            
			AppUserModel responseAppUser = new AppUserModel();

            var uri = new Uri(string.Format(Constants.baseUri + Constants.apiAppUsersUrl, string.Empty));

			try
			{
				var json = JsonConvert.SerializeObject(user);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				response = await client.PostAsync(uri, content);

				if (response.IsSuccessStatusCode)
				{
					Debug.WriteLine(@"User successfully created");

					var newcontent = await response.Content.ReadAsStringAsync();


					responseAppUser = JsonConvert.DeserializeObject<AppUserModel>(newcontent);

					Debug.WriteLine("Returned User: {0}, ID: {1}", responseAppUser.Name, responseAppUser.ID);

					Helpers.Settings.GeneralSettings = "test";
					KhulumaClient.Helpers.Settings.id = responseAppUser.ID;
					Helpers.Settings.Username = responseAppUser.Username;
					Helpers.Settings.Name = responseAppUser.Name;
					Helpers.Settings.Surname = responseAppUser.Surname;
					Helpers.Settings.Age = responseAppUser.Age;
					Helpers.Settings.Gender = responseAppUser.Gender;
					Helpers.Settings.Email = responseAppUser.Email;
					Helpers.Settings.PhoneNumber = responseAppUser.PhoneNumber;
					Helpers.Settings.HomeAddress = responseAppUser.HomeAddress;
					Helpers.Settings.LocationId = Convert.ToInt32(responseAppUser.LocationId);
					Helpers.Settings.GroupId = 1;

					Helpers.Settings.isRegistered = true;

				}


			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR: ", ex.Message);
			}

            return responseAppUser;
        }

		public async Task<List<MobilePageModel>> GetMobilePagesAsync()
		{
			MobilePages = new List<MobilePageModel>();

			var uri = new Uri(string.Format(Constants.baseUri + Constants.apiMobilePagesUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					MobilePages = JsonConvert.DeserializeObject<List<MobilePageModel>>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR: {0}", ex.Message);
			}

			return MobilePages;
		}

		public async Task<List<FlaggedContentModel>> GetFlaggedContentAsync()
		{
            FlaggedContent = new List<FlaggedContentModel>();

            var uri = new Uri(string.Format(Constants.baseUri + Constants.apiFlaggedContentUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    FlaggedContent = JsonConvert.DeserializeObject<List<FlaggedContentModel>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR: {0}", ex.Message);
            }

            return FlaggedContent;

        }
	}
}
