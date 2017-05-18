using KhulumaClient.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

           
            bool debug_mode = false;
            bool registered = Helpers.Settings.isRegistered;

            if (debug_mode)
			{

				MainPage = new NavigationPage(new SettingsPage());

			}
			else {

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

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}


	}
}
