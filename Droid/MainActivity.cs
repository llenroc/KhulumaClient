using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Gms.Common;

using Android.Util;
using Gcm.Client;
using System;
using Android.Gms.Iid;
using Android.Gms.Gcm;

namespace KhulumaClient.Droid
{
    [Activity(Label = "KhulumaClient.Droid", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{

        // Create a new instance field for this activity.
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

            // Set the current instance of MainActivity.
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());

            try
            {
                // Check to ensure everything's set up right
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);

                // Register for push notifications
                System.Diagnostics.Debug.WriteLine("Registering...");
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);

                

            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }


        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
        
        }

        protected override void OnStart()
        {
            base.OnStart();
           

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
