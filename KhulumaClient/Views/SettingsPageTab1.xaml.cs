using KhulumaClient.Contracts;
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
    public partial class SettingsPageTab1 : ContentPage
    {
        public SettingsPageTab1()
        {
            InitializeComponent();

            

            logTokenButton.Clicked += (sender, e) =>
            {
                string tokenID = DependencyService.Get<IFireBase>().GetTokenID();
                textTokenIDResponse.Text = tokenID;
            };
            subscribeButton.Clicked += (sender, e) =>
            {
                DependencyService.Get<IFireBase>().SubscribeToNotifications("1");
            };

        }
    }
}
