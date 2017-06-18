using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
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
            
            IsPlayServicesAvailable();

            

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());

            FirebaseApp.InitializeApp(this);

            Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);

           

        }

        protected override void OnResume()
        {
            base.OnResume();
        
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
