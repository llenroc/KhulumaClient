using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class RegisterPage : ContentPage
	{

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
			agesList.SelectedIndex = 0;
			gendersList.SelectedIndex = 0;
		}

		public async void Handle_Clicked(object sender, System.EventArgs e)
		{
			buttonSave.IsEnabled = false;
			userModel appUser = new userModel();

            

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
            appUser.GroupId = 0;

            if (appUser.Username == "" || appUser.Name == "" || appUser.Surname == "" || appUser.Gender == "None selected" || appUser.PhoneNumber == "" || appUser.Email == "")
            {
                await DisplayAlert("Alert", "Please fill in all fields", "OK");
            } else
            {
                await restService.PostUser(appUser);

                if (Helpers.Settings.isRegistered == true)
                {
                    buttonGotoChat.IsVisible = true;
                    buttonGotoChat.IsEnabled = true;

                    await DisplayAlert("Registered Success", "You have been registered with the following settings: " + System.Environment.NewLine +
                                 "Username: " + appUser.Username +
                                 "Name: " + appUser.Name +
                                 "Surname: " + appUser.Surname +
                                 "Age: " + appUser.Age +
                                 "Gender: " + appUser.Gender +
                                 "Phone Number: " + appUser.PhoneNumber +
                                 "Email: " + appUser.Email +
                                 "Location Id: " + appUser.LocationId,
                                       "OK");

                }
            }

            



		}

		public async void newPage(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new GroupChatPage());

		}
	}
}
