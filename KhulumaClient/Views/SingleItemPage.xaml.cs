using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KhulumaClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleItemPage : ContentPage
    {
        public List<MobilePageModel> MobilePages;
        IRestService restService;

        public SingleItemPage()
        {
            //InitializeComponent();

            MobilePages = new List<MobilePageModel>();
            restService = new RestServiceImplementation();

           

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MobilePages = await restService.GetMobilePagesAsync();

            foreach (var page in MobilePages)
            {
                Debug.WriteLine("{0} : {1} : {2}", page.PageId, page.PageTitle, page.PageHTMLContent);
            }

        }
        }
}
