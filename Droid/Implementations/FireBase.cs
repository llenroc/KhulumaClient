using Xamarin.Forms;
using KhulumaClient.Droid;
using KhulumaClient.Contracts;
using System;
using KhulumaClient.Droid.Implementations;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Firebase;

[assembly: Dependency(typeof(FireBase))]
namespace KhulumaClient.Droid.Implementations
{
    class FireBase : IFireBase
    {
        const string TAG = "MainActivity";
        
        public void FCMSubscribe(string currentGroup, string khuGroup)
        {
            
            try
            {
                Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);

                FirebaseMessaging.Instance.SubscribeToTopic(khuGroup);
                Log.Debug(TAG, "Subscribed to remote notifications: " + khuGroup);

                FirebaseMessaging.Instance.UnsubscribeFromTopic(currentGroup);
                Log.Debug(TAG, "Subscribed to remote notifications: " + khuGroup);
            }
            catch(Exception ex) {

            }


        }
    }
}