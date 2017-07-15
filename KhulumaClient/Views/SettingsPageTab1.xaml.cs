using KhulumaClient.Contracts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KhulumaClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPageTab1 : ContentPage
    {
        public SettingsPageTab1()
        {
            InitializeComponent();

           

         

        }

        private void subscribeButton_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IFireBase>().FCMSubscribe("1", "2");
        }
    }
}
