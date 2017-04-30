using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhulumaClient
{
	public partial class TabbedContentPage
	{
		List<MobilePageModel> MobilePages;
		IRestService restService;

		public TabbedContentPage()
		{
			InitializeComponent();





		}


		protected async override void OnAppearing()
		{
			base.OnAppearing();

			MobilePages = await restService.GetMobilePagesAsync();

			foreach (MobilePageModel mobilePage in MobilePages)
			{
				DisplayAlert(mobilePage.PageTitle, mobilePage.PageHTMLContent, "OK");
			}

			//listView.ItemsSource = await restService.GetMobilePagesAsync();




		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			var postItem = e.SelectedItem as MobilePageModel;


			var postPage = new SingleItemPage();

			postPage.BindingContext = postItem;

			Navigation.PushAsync(postPage);
		}



	}
}
