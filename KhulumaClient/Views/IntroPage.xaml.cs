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
    public partial class IntroPage : CarouselPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
