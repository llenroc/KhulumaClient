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

using Xamarin.Forms;
using KhulumaClient.Droid;
using KhulumaClient.Droid.Implementations;
using KhulumaClient.Contracts;
using Android.Gms.Common;
using Android.Util;
using Firebase.Messaging;
using Firebase.Iid;
using Firebase;

[assembly: Dependency(typeof(FireBaseImplementation))]
namespace KhulumaClient.Droid.Implementations
{
    class FireBaseImplementation : IFireBase
    {
        const string TAG = "MainActivity";
        string firebaseToken;
        public string GetTokenID()
        {
            firebaseToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "InstanceID token: " + firebaseToken);
            return firebaseToken;
            
        }

        public void SubscribeToNotifications(string group)
        {
            FirebaseMessaging.Instance.SubscribeToTopic(group);
            Log.Debug(TAG, "Subscribed to remote notifications");
        }
    }
}