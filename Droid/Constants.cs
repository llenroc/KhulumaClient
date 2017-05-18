using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KhulumaClient.Droid
{
    public static class Constants
    {
        public const string SenderID = "950612846979"; // Google API Project Number
        public const string ListenConnectionString = "Endpoint=sb://khunotificationsnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=Xoq5THimKs/HxRJXyb/BwDYvBOJngoKga4ZODR2h7aw=";
        public const string NotificationHubName = "khunotifications";
    }
}