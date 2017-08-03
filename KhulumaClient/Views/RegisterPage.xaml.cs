using KhulumaClient.Models;
using KhulumaClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class RegisterPage : ContentPage
	{
        PostUserModel appUser;
        AppUserModel appTestUser;
        Page userProfilePage;

        IRestService restService;


		List<locationModel> locations;
		int postResponse;

		public RegisterPage()
		{
			InitializeComponent();



			restService = new RestServiceImplementation();


		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
            buttonSave.IsEnabled = false;
            buttonSave.Text = "LOADING";
            appUser = new PostUserModel();
            appTestUser = new AppUserModel();

			locations = await restService.GetLocationsAsync();

			foreach (locationModel location in locations)
			{
				locationsList.Items.Add(location.HospitalName);
			}
			locationsList.SelectedIndex = 0;

			for (int age = 12; age < 90; age++)
			{
				agesList.Items.Add(age.ToString());
			}
			agesList.SelectedIndex = 10;
			gendersList.SelectedIndex = 0;

            buttonSave.IsEnabled = true;
            buttonSave.Text = "REGISTER";
        }

		public async void Handle_Clicked(object sender, System.EventArgs e)
		{
            buttonSave.IsEnabled = false;
           

            

			var ageIndex = agesList.Items[agesList.SelectedIndex];
			int age = Convert.ToInt32(ageIndex);

            appUser.Username = inputUsername.Text;
            appUser.Name = inputName.Text;
            appUser.Surname = inputSurname.Text;
            appUser.Age = age;
            appUser.Gender = gendersList.Items[gendersList.SelectedIndex];
            appUser.PhoneNumber = inputTelephone.Text;
            appUser.Email = inputEmail.Text;
            appUser.LocationId = locationsList.SelectedIndex+1;
            appUser.GroupId = 1;

            appUser.Surname = inputSurname.Text;
            
            if (appUser.Username == "" || appUser.Name == "" || appUser.Surname == "" || appUser.Gender == "None selected" || appUser.PhoneNumber == "" || appUser.Email == "")
            {
                await DisplayAlert("Alert", "Please fill in all fields", "OK");
                buttonSave.IsEnabled = true;
            } else if (!isValidEmail(appUser.Email))
            {
                await DisplayAlert("Alert", "Please fill in a valid email address", "OK");
                buttonSave.IsEnabled = true;
            }
                else
            {

                var responsePostUser = await restService.PostUser(appUser);

                var responseType = responsePostUser.responseType;

                if (responseType == ResponseType.Username)
                {
                    await DisplayAlert("Alert", "That username has already been taken, please choose another", "OK");
                    inputUsername.Focus();
                } else
                {
                    appTestUser = responsePostUser.appUser;
                }

                

                if (Helpers.Settings.isRegistered == true)
                {

                    buttonSave.IsVisible = false;
                    buttonSave.IsEnabled = false;



                    var thankYouPage = new ThankYouPage();
                    
                    Navigation.InsertPageBefore(thankYouPage, Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync(true);
                    //await Navigation.PushAsync(thankYouPage);
                }
                else
                {
                    if (responseType == ResponseType.Username)
                    {

                    } else
                    {
                        await DisplayAlert("Alert", "There was a registration error, please try again. If the problem persists, please contact an administrator.", "OK");
                    }

                    
                    buttonSave.IsEnabled = true;
                }
            }



        }


        public async void newPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GroupChatPage());

        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }


    }
}
