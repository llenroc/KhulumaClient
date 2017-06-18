using Xamarin.Forms;
using KhulumaClient.Droid;
using KhulumaClient.Contracts;
using System;
using KhulumaClient.Droid.Implementations;
using Firebase.Messaging;
using Android.Util;

[assembly: Dependency(typeof(FireBase))]
namespace KhulumaClient.Droid.Implementations
{
    class FireBase : IFireBase
    {
        const string TAG = "MainActivity";
        public void FCMSubscribe(string khuGroup)
        {
            FirebaseMessaging.Instance.SubscribeToTopic(khuGroup);
            Log.Debug(TAG, "Subscribed to remote notifications");
        }
    }
}