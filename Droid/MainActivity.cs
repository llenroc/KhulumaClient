using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Firebase;
using Android.Widget;

namespace KhulumaClient.Droid
{
    [Activity(Label = "KhulumaClient.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{

        static MainActivity instance = null;
        public static string TAG = "MainActivity";
        // Return the current activity instance.
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }


        protected override void OnCreate(Bundle bundle)
		{
            
            instance = this;
            
            IsPlayServicesAvailable();

            

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());

            


        }

        protected override void OnResume()
        {
            base.OnResume();
        
        }

        protected override void OnStart()
        {
            base.OnStart();
            FirebaseApp.InitializeApp(this);

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
                    Log.Debug(TAG, "Refreshed token: " + googleAPIResultText);
                    Finish();
                }
                return false;
            }
            else
            {
                googleAPIResultText = "Google Play Services is available.";
                Log.Debug(TAG, "Refreshed token: " + googleAPIResultText);
                return true;
            }
        }



    }
}
