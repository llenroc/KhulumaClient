using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KhulumaClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
            introPageImage.Aspect = Aspect.AspectFit;
            introPageImage.Source = ImageSource.FromResource("KhulumaClient.intropage.png");

            buttonContinue.Clicked += continueClicked;
        }

        private async void continueClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
