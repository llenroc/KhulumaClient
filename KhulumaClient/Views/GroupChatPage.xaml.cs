using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;
using KhulumaClient.Views;
using System.Text.RegularExpressions;

namespace KhulumaClient
{
	public partial class GroupChatPage : ContentPage
	{

		public ObservableCollection<ChatModel> chatItems { get; set; }
        public string ChatGroupName { get; set; }
        List<ChatModel> Chats;
        public userModel thisUser { get; set; }
        public Page userProfilePage;
        public List<FlaggedContentModel> flaggedContentList;
        List<String> flaggedStringList;
        bool isFlagged;


        HubConnection hubConnection = new HubConnection("http://khulumaserver.azurewebsites.net");
		IRestService restService;




		public GroupChatPage()
		{
			InitializeComponent();
            isFlagged = false;

            thisUser = new userModel();

			var chatHubProxy = hubConnection.CreateHubProxy("ChatHub");
			restService = new RestServiceImplementation();

			ToolbarItem itemAbout = new ToolbarItem
			{
				Text = "Who We Are",
				Order = ToolbarItemOrder.Secondary
			};

            itemAbout.Clicked += itemMenuClicked;

			ToolbarItems.Add(itemAbout);

            

			GetInfo();

			


            chatHubProxy.On<string, string, string, int>("addNewMessageToPage", (name, message, timestamp, groupid) =>
			{

                if (groupid == thisUser.GroupId)
                {
                    
                    displayChat(name, timestamp, message);
                }

                
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
                    if (message.Length > 0)
                    {
                        await chatHubProxy.Invoke("Send", new object[] { name, message, userid, groupName, groupid, isFlagged });
                    }
                    

				}
				catch (Exception ex)
				{
					Debug.WriteLine("ERROR", ex.Message);
				}

				Debug.WriteLine("VARS: " + name + " " + message);



			};

            MessageBox.TextChanged += OnTextChanged;

            void OnTextChanged(object sender, EventArgs e)
            {

                var restrictCount = 250;
                String val = MessageBox.Text; //Get Current Text

                if (val.Length > restrictCount)//If it is more than your character restriction
                {
                    string sub = val.Substring(0, 250);

                    MessageBox.Text = sub;

                    //val = val.Remove(val.Length - 1);// Remove Last character 
                    //Set the Old value
                    val = sub;
                }
                String output;
                foreach (var item in flaggedStringList)
                {
                    Debug.WriteLine("ITEM: {0}", item);
                    Debug.WriteLine("VAL: {0}", val);
                    output = val.Replace(item,"****");
                    Debug.WriteLine("OUTPUT: {0}", output);

                    val = output;

                    

                }

                if (val.Contains("****")) isFlagged = true;

                

                MessageBox.Text = val;

            }



        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            int chatsCount;
            int totalChats;
            chatItems = new ObservableCollection<ChatModel>();
            Chats = await restService.GetChatsAsync();
            thisUser = await restService.GetThisUser();
            flaggedContentList = new List<FlaggedContentModel>();
            flaggedContentList = await restService.GetFlaggedContentAsync();

            flaggedStringList = new List<string>();

            foreach (FlaggedContentModel flag in flaggedContentList)
            {
                flaggedStringList.Add(flag.ContentText);
            }


            var groupID = thisUser.GroupId;
            Debug.WriteLine("THIS User GroupID: {0}", thisUser.GroupId);

            if (groupID == 0)
            {
                await DisplayAlert("Alert", "You have not been assigned to a group yet, please contact your administrator", "OK");
            }
            else
            {
                ChatGroupName = thisUser.GroupName;
                this.Title = "Group Chat: " + thisUser.GroupName;

                Helpers.Settings.GroupId = groupID;

                
                foreach (ChatModel chat in Chats)
                {
                    if (chat.Name == null) chat.Name = "Guest";

                    Debug.WriteLine("CHAT: {0} : {1} ::: {2} :", chat.Name, chat.MessageTimestamp, chat.Message);

                    Debug.WriteLine("INDEX: {0}", Chats.IndexOf(chat));
                    chatsCount = Chats.IndexOf(chat);
                    totalChats = Chats.Count;
                    if (chatsCount>totalChats-20)
                    {
                        chatItems.Add(chat);
                    }

                 



                }



                chatListView.ItemsSource = chatItems;

                var target = chatItems[chatItems.Count - 1];

                chatListView.ScrollTo(target, ScrollToPosition.End, true);

            }


        }

        public async void ItemProfile_Clicked(object sender, EventArgs e)
        {
            userProfilePage = new UserProfilePage();
            userProfilePage.BindingContext = thisUser;

            await Navigation.PushModalAsync(userProfilePage);
        }

        

		async void itemMenuClicked(object sender, EventArgs e)
		{
           
            await Navigation.PushAsync(new WhoWeArePage());

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

            Debug.WriteLine("CHAT: {0} : {1} ::: {2} :", name, time, message);

            chatItems.Add(new ChatModel()
			{
				Name = name,
				Message = message,
				MessageTimestamp = time

			});

			var target = chatItems[chatItems.Count - 1];

			chatListView.ScrollTo(target, ScrollToPosition.End, true);
		}

	}
}
