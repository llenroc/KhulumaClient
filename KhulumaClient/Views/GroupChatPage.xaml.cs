using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;
using KhulumaClient.Views;
using KhulumaClient.Contracts;

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


        HubConnection hubConnection = new HubConnection(Constants.baseUri);
        IHubProxy chatHubProxy;
		IRestService restService;




		public GroupChatPage()
		{
			InitializeComponent();

           

            isFlagged = false;
            
           
            chatListView.ItemSelected += (sender, e) => {
                ((ListView)sender).SelectedItem = null;
            };

            thisUser = new userModel();

			
            
			restService = new RestServiceImplementation();

			ToolbarItem itemAbout = new ToolbarItem
			{
				Text = "Who We Are",
				Order = ToolbarItemOrder.Secondary
			};

            itemAbout.Clicked += itemMenuClicked;

			ToolbarItems.Add(itemAbout);


            chatHubProxy = hubConnection.CreateHubProxy("ChatHub");

            startHub();

            chatHubProxy.On<string, string, string, int[]>("addNewMessageToPage", (name, message, timestamp, idVals) =>
			{
                int groupid = idVals[0];
                int userid = idVals[1];

                if (groupid == thisUser.GroupId)
                {
                    
                    displayChat(name, timestamp, message, userid);
                }

                var target = chatItems[chatItems.Count - 1];

                chatListView.ScrollTo(target, ScrollToPosition.End, true);

            });

			chatHubProxy.On<string, string, string>("quartzJobExecuted", (name, message, hournow) =>
			{

                var userid = 0;
                displayChat(name, message, hournow, userid);

			});
            
			SendButton.Clicked += async (object sender, EventArgs e) =>
			{

				Debug.WriteLine("I've been clicked");

                

                if (MessageBox.Text=="") return;

				int userid = Helpers.Settings.id;

                if (userid == 0)
                {
                    userid = 1;
                }

				int groupid = Helpers.Settings.GroupId;
                if (groupid==0)
                {
                    groupid = 2;
                }

                string groupName = ChatGroupName;
				var message = MessageBox.Text;

                /*****************************/
                /*****************************/
                isFlagged = false;
                
                String val = MessageBox.Text; //Get Current Text
                String output;
                foreach (var item in flaggedStringList)
                {
                    Debug.WriteLine("ITEM: {0}", item);
                    Debug.WriteLine("VAL: {0}", val);
                    output = val.Replace(item, "****");
                    Debug.WriteLine("OUTPUT: {0}", output);

                    val = output;

                
                }

                if (val.Contains("****")) isFlagged = true;

                /*****************************/
                /*****************************/
                message = val;
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
                    startHub();
                }

				Debug.WriteLine("VARS: " + name + " " + message);



			};

            MessageBox.TextChanged += OnTextChanged;

            void EditorCompleted(object sender, EventArgs e)
            {
                var text = ((Editor)sender).Text; // sender is cast to an Editor to enable reading the `Text` property of the view.
            }

            void OnTextChanged(object sender, EventArgs e)
            {
                
                var restrictCount = 250;
                int totalChar = 0;
                String val = MessageBox.Text; //Get Current Text

                if (val.Length > restrictCount)//If it is more than your character restriction
                {
                    string sub = val.Substring(0, 250);

                    MessageBox.Text = sub;

                   
                    val = sub;
                }
                totalChar = restrictCount - val.Length;
                messageCounter.Text = totalChar + " Characters left";

            }



        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            
            int chatsCount = 0;
            int totalChats = 0;
            int groupID;
            int currentGroupID;

            chatItems = new ObservableCollection<ChatModel>();
            
            thisUser = await restService.GetThisUser();
            groupID = thisUser.GroupId;
            currentGroupID = Helpers.Settings.GroupId;
            Helpers.Settings.GroupId = groupID;
            Chats = await restService.GetChatsAsync();

            flaggedContentList = new List<FlaggedContentModel>();
            flaggedContentList = await restService.GetFlaggedContentAsync();

            flaggedStringList = new List<string>();

            foreach (FlaggedContentModel flag in flaggedContentList)
            {
                flaggedStringList.Add(flag.ContentText);
            }


            
          
            try
            {
             
                DependencyService.Get<IFireBase>().FCMSubscribe(currentGroupID.ToString(), groupID.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"", ex.InnerException);
            }
           
           
            
           
            Debug.WriteLine("THIS User GroupID: {0}", thisUser.GroupId);

            if (groupID == 1)
            {
                messageInputBox.IsVisible = false;
                await DisplayAlert("Alert", "You have not been assigned to a group yet, please contact your administrator", "OK");
            }
            else
            {
                ChatGroupName = thisUser.GroupName;
                this.Title = "Khuluma Chat: " + thisUser.GroupName;

                Helpers.Settings.GroupId = groupID;

                
                foreach (ChatModel chat in Chats)
                {
                    if (chat.Name == null) chat.Name = "Guest";

                    Debug.WriteLine("CHAT: {0} : {1} ::: {2} :", chat.Name, chat.MessageTimestamp, chat.Message);

                    Debug.WriteLine("INDEX: {0}", Chats.IndexOf(chat));
                    chatsCount = Chats.IndexOf(chat);
                    totalChats = Chats.Count;

                    if (totalChats < 21)
                    {
                        chatItems.Add(chat);
                    } else
                    {
                        if (chatsCount > totalChats - 20)
                        {
                            chatItems.Add(chat);
                        }
                    }

                 

                }



                chatListView.ItemsSource = chatItems;

                var target = chatItems[chatItems.Count - 1];

                chatListView.ScrollTo(target, ScrollToPosition.End, true);

            }


        }

        public async void startHub()
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

	
		public void displayChat(string name, string time, string message, int userid)
		{

            Debug.WriteLine("CHAT: {0} : {1} ::: {2} :", name, time, message);

            chatItems.Add(new ChatModel()
			{
				Name = name,
				Message = message,
				MessageTimestamp = time,
                UserId = userid

			});

			var target = chatItems[chatItems.Count - 1];

			chatListView.ScrollTo(target, ScrollToPosition.End, true);
		}

	}
}
