using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class GroupChatPage : ContentPage
	{

		public ObservableCollection<ChatModel> chatItems { get; set; }
        public string ChatGroupName { get; set; }

        List<ChatModel> Chats;
        public userModel thisUser { get; set; }

		HubConnection hubConnection = new HubConnection("http://khulumaserver.azurewebsites.net");
		IRestService restService;




		public GroupChatPage()
		{
			InitializeComponent();
            

            thisUser = new userModel();

			var chatHubProxy = hubConnection.CreateHubProxy("ChatHub");
			restService = new RestServiceImplementation();

			ToolbarItem itemMenu = new ToolbarItem
			{
				Text = "Who We Are",
				Order = ToolbarItemOrder.Secondary
			};

			itemMenu.Clicked += itemMenuClicked;

			ToolbarItems.Add(itemMenu);

			GetInfo();

			chatItems = new ObservableCollection<ChatModel>();

			chatListView.ItemsSource = chatItems;



			chatHubProxy.On<string, string, string>("addNewMessageToPage", (name, message, timestamp) =>
			{
				displayChat(name, timestamp, message);
			});

			chatHubProxy.On<string, string, string>("quartzJobExecuted", (name, message, hournow) =>
			{
	
				displayChat(name, message, hournow);


			});

			SendButton.Clicked += async (object sender, EventArgs e) =>
			{

				Debug.WriteLine("I've been clicked");
				int userid = Helpers.Settings.id;
				int groupid = Helpers.Settings.GroupId;
                string groupName = ChatGroupName;
				var message = MessageBox.Text;
				MessageBox.Text = "";

				var name = Helpers.Settings.Username;

				try
				{
					await chatHubProxy.Invoke("Send", new object[] { name, message, userid, groupName, groupid });

				}
				catch (Exception ex)
				{
					Debug.WriteLine("ERROR", ex.Message);
				}

				Debug.WriteLine("VARS: " + name + " " + message);



			};

		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			Chats = await restService.GetChatsAsync();
            thisUser = await restService.GetThisUser();
            var groupID = thisUser.GroupId;
            Debug.WriteLine("THIS User GroupID: {0}", thisUser.GroupId);

            if (groupID == 0)
            {
                await DisplayAlert("Alert", "You have not been assigned to a group yet, please contact your administrator", "OK");
            } else
            {
                ChatGroupName = thisUser.GroupName;
                this.Title = "Group Chat: " + thisUser.GroupName;

                Helpers.Settings.GroupId = groupID;
                

                foreach (ChatModel chat in Chats)
                {
                    if (chat.Name == null) chat.Name = "Guest";

                    displayChat(chat.Name, chat.timestampString, chat.Message);

                }
            }


			

	
		}

		async void itemMenuClicked(object sender, EventArgs e)
		{
           
            await Navigation.PushAsync(new SingleItemPage());

		}

		public async Task GetInfo()
		{
			try
			{
				await hubConnection.Start();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Connection failed: " + ex.ToString());
			}

		}
		public void displayChat(string name, string time, string message)
		{
			chatItems.Add(new ChatModel()
			{
				Name = name,
				Message = message,
				timestampString = time

			});

			var target = chatItems[chatItems.Count - 1];

			chatListView.ScrollTo(target, ScrollToPosition.End, true);
		}

	}
}
