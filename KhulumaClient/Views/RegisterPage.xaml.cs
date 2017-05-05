using KhulumaClient.Models;
using KhulumaClient.Views;
using System;
using System.Collections.Generic;
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



			if (locations != null)
			{
				
			}
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

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
            
            if (appTestUser.Username == "" || appTestUser.Name == "" || appTestUser.Surname == "" || appTestUser.Gender == "None selected" || appTestUser.PhoneNumber == "" || appTestUser.Email == "")
            {
                await DisplayAlert("Alert", "Please fill in all fields", "OK");
                buttonSave.IsEnabled = true;
            } else
            {
                await restService.PostUser(appUser);

                if (Helpers.Settings.isRegistered == true)
                {
                    //RegisterFormBox.IsVisible = false;
                    //RegisterFormBox.IsEnabled = false;

                    //SubmissionBox.IsVisible = true;
                    //SubmissionBox.IsEnabled = true;


                    buttonGotoChat.IsEnabled = true;
                    buttonGotoChat.IsVisible = true;
                    

                    userProfilePage = new UserProfilePage();
                    userProfilePage.BindingContext = appUser;

                    await Navigation.PushModalAsync(userProfilePage);
                    


                }
                else
                {
                    await DisplayAlert("Alert", "There was a registration error, please try again. If the problem persists, please contact an administrator.", "OK");
                    buttonSave.IsEnabled = true;
                }
            }



        }


        public async void newPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new GroupChatPage());

        }


    }
}
