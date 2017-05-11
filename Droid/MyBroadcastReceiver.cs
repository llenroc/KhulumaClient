using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.Widget;
using Android.OS;
using static Android.Media.Audiofx.BassBoost;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is needed only for Android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace KhulumaClient.Droid
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
         Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
         Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
         Categories = new string[] { "@PACKAGE_NAME@" })]
   public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public static string[] SENDER_IDS = new string[] { Constants.SenderID };

        public const string TAG = "MyBroadcastReceiver-GCM";

       

    }

    public class WakefulReceiver : WakefulBroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            StartWakefulService(context, intent);
        }
    }

    [Service]
    public class NotificationIntentService : IntentService
    {
        public NotificationIntentService() : base("NotificationIntentService") { }
        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                HandleMessage(ApplicationContext, intent);
            }
            finally
            {
                WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
            }
        }

       private void HandleMessage(Context context, Intent intent)
        {
            Toast.MakeText(context, "Hello from Handle Message", ToastLength.Short).Show();

            var message = string.Empty;
            var trip = string.Empty;
            Bundle extras = intent.Extras;

            message = string.IsNullOrEmpty(extras.GetString("message")) ? "NEW MSG" :
                extras.GetString("message");

            trip = string.IsNullOrEmpty(extras.GetString("trip")) ? "NEW TRIP" :
                extras.GetString("message");

            var title = "Notification: " + (message.Length > 10 ? message.Substring(0, 10) + "..." : message);

            if (!string.IsNullOrEmpty(message))
            {
                //title = App.GetValueFromKey(37); // "New Message!"
                title = "New Message";
            }
            else if (!string.IsNullOrEmpty(trip))
            {
                //title = App.GetValueFromKey(36); // "Trip Update!"
                title = "Trip Update";
            }

            intent.AddFlags(ActivityFlags.SingleTop); // origin --> .ClearTask
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.CancelCurrent); // origin --> OneShot

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                //.SetSmallIcon(Resource.Drawable.icon_notifcation)
                .SetContentTitle(title)
                .SetContentText(intent.GetStringExtra("message"))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);
                //.SetSound(Settings.System.DefaultNotificationUri);
                

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(1, builder.Build());
        }


    

    }



        [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        private NotificationHub Hub { get; set; }

        public PushHandlerService() : base(Constants.SenderID)
        {
            Log.Info(MyBroadcastReceiver.TAG, "PushHandlerService() constructor");
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info(MyBroadcastReceiver.TAG, "GCM Message Received!");

            var msg = new StringBuilder();

            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }

            string messageText = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(messageText))
            {
                createNotification("New Khuluma message!", messageText);
            }
            else
            {
                createNotification("Unknown message details", msg.ToString());
            }
            HandleMessage(ApplicationContext, intent);



        }

        private void HandleMessage(Context context, Intent intent)
        {
            Toast.MakeText(context, "Hello from Handle Message", ToastLength.Short).Show();

            var message = string.Empty;
            var trip = string.Empty;
            Bundle extras = intent.Extras;

            message = string.IsNullOrEmpty(extras.GetString("message")) ? "NEW MSG" :
                extras.GetString("message");

            trip = string.IsNullOrEmpty(extras.GetString("trip")) ? "NEW TRIP" :
                extras.GetString("message");

            var title = "Notification: " + (message.Length > 10 ? message.Substring(0, 10) + "..." : message);

            if (!string.IsNullOrEmpty(message))
            {
                //title = App.GetValueFromKey(37); // "New Message!"
                title = "New Message";
            }
            else if (!string.IsNullOrEmpty(trip))
            {
                //title = App.GetValueFromKey(36); // "Trip Update!"
                title = "Trip Update";
            }

            intent.AddFlags(ActivityFlags.SingleTop); // origin --> .ClearTask
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.CancelCurrent); // origin --> OneShot

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                //.SetSmallIcon(Resource.Drawable.icon_notifcation)
                .SetContentTitle(title)
                .SetContentText(intent.GetStringExtra("message"))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);
            //.SetSound(Settings.System.DefaultNotificationUri);


            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(1, builder.Build());
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(MyBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            RegistrationID = registrationId;

            createNotification("PushHandlerService-GCM Registered...",
                                "The device has been Registered!");

            Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
                                        context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadcastReceiver.TAG, ex.Message);
            }

            //var tags = new List<string>() { "falcons" }; // create tags if you want
            var tags = new List<string>() { };

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(MyBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(MyBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);

            createNotification("GCM Unregistered...", "The device has been unregistered!");
        }

        void createNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show UI
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Create the notification
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);

            //Auto-cancel will remove the notification once the user touches it
            notification.Flags = NotificationFlags.AutoCancel;

            //Set the notification info
            //we use the pending intent, passing our ui intent over, which will get called
            //when the notification is tapped.
            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

            //Show the notification
            notificationManager.Notify(1, notification);
            dialogNotify(title, desc);
        }

        protected void dialogNotify(String title, String message)
        {

            MainActivity.instance.RunOnUiThread(() => {
                AlertDialog.Builder dlg = new AlertDialog.Builder(MainActivity.instance);
                AlertDialog alert = dlg.Create();
                alert.SetTitle(title);
                alert.SetButton("Ok", delegate {
                    alert.Dismiss();
                });
                alert.SetMessage(message);
                alert.Show();
            });
        }

    }

}