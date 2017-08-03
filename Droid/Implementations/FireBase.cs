using Xamarin.Forms;
using KhulumaClient.Contracts;
using System;
using KhulumaClient.Droid.Implementations;
using Android.Gms.Gcm;
using Android.OS;
using Android.Util;
using System.Threading;
using Android.Gms.Iid;

[assembly: Dependency(typeof(FireBase))]
namespace KhulumaClient.Droid.Implementations
{
    class FireBase : IFireBase
    {
        string registrationId = GcmService.RegistrationID;
        private string token;
        public void FCMSubscribe(string currentGroup, string khuGroup)
        {

            try
            {



                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    Console.WriteLine("Hello, world");

                    var instanceID = InstanceID.GetInstance(Android.App.Application.Context);
                    token = instanceID.GetToken(
                        "950612846979", GoogleCloudMessaging.InstanceIdScope, null);

                    var pubsub = GcmPubSub.GetInstance(Android.App.Application.Context);
                    pubsub.Unsubscribe(token, "/topics/" + currentGroup);
                    pubsub.Subscribe(token, "/topics/"+ khuGroup, null);
                    

                }).Start();

                



            }
            catch(Exception ex)
            {
                string tag = "myapp";

                Log.Error(tag, "this is an info message {0}", ex);
            }


        }

        public void FCMUnsubscribe(string currentGroup)
        {
            try
            {



                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    /* run your code here */
                    Console.WriteLine("Hello, world");

                    var instanceID = InstanceID.GetInstance(Android.App.Application.Context);
                    token = instanceID.GetToken(
                        "950612846979", GoogleCloudMessaging.InstanceIdScope, null);

                    var pubsub = GcmPubSub.GetInstance(Android.App.Application.Context);
                    pubsub.Unsubscribe(token, "/topics/" + currentGroup);
                    


                }).Start();





            }
            catch (Exception ex)
            {
                string tag = "myapp";

                Log.Error(tag, "this is an info message {0}", ex);
            }
        }
    }


 }


