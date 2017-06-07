using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Firebase;

namespace KhulumaClient.Droid
{
    [Activity(Label = "KhulumaClient.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{

        public static MainActivity instance;
        const string TAG = "MainActivity";

        
        protected override void OnCreate(Bundle bundle)
		{
            var thisContext = Application.ApplicationContext;
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
            
            //FirebaseApp app = FirebaseApp.InitializeApp(this);
            FirebaseApp.InitializeApp(Application.Context);


        

            IsPlayServicesAvailable();

           
            
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            string googleAPIResultText;
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    googleAPIResultText = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    googleAPIResultText = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                googleAPIResultText = "Google Play Services is available.";
                return true;
            }
        }



    }
}
