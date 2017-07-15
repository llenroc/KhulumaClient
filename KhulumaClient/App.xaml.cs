using KhulumaClient.Contracts;
using KhulumaClient.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class App : Application
	{
        bool registered;
        int groupID;
        bool debug_mode;
        public App()
		{
			InitializeComponent();

            debug_mode = false;

            registered = Helpers.Settings.isRegistered;
            groupID = Helpers.Settings.GroupId;

            if (debug_mode)
            {
                MainPage = new NavigationPage(new GroupChatPage());
            }
            else
            {
                if (registered)
                {


                    MainPage = new NavigationPage(new GroupChatPage());
                }
                else
                {

                    MainPage = new NavigationPage(new IntroPage());
                }
            }
               

		}

		protected override void OnStart()
		{
            
            
        }

		protected async override void OnSleep()
		{
            registered = Helpers.Settings.isRegistered;

            if (registered)
            {
                // Handle when your app sleeps
                var nav = MainPage.Navigation;

                // you may want to clear the stack (history)
                await nav.PopToRootAsync(true);
            }
            
            
           
        }

		protected override void OnResume()
		{
            registered = Helpers.Settings.isRegistered;
            groupID = Helpers.Settings.GroupId;

            // Handle when your app resumes
            if (registered)
            {
                MainPage = new NavigationPage(new GroupChatPage());
            }
            else
            {

                
            }

        }




    }
}
