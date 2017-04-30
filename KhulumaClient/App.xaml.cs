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


			if (debug_mode == true)
			{

				//Helpers.Settings.Username = "Debugger";
			

				MainPage = new NavigationPage(new DataPage());

			}
			else {


				if (Helpers.Settings.isRegistered == true)
				{

					MainPage = new NavigationPage(new GroupChatPage());

				}
				else
				{

					MainPage = new NavigationPage(new RegisterPage());

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
