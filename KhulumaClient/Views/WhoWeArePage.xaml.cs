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
    public partial class WhoWeArePage : ContentPage
    {
        public List<MobilePageModel> MobilePages;
        IRestService restService;
        public WhoWeArePage()
        {
            InitializeComponent();
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

                if (page.PageId == 1)
                {
                    Title = page.PageTitle;
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = page.PageHTMLContent;

                    contentViewer.Source = htmlSource;
                }
            }

        }

    }
}
