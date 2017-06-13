using Xamarin.Forms;
using KhulumaClient.Contracts;
using KhulumaClient.iOS.Implementations;

[assembly: Dependency(typeof(FireBaseImplementation))]
namespace KhulumaClient.iOS.Implementations
{
    class FireBaseImplementation : IFireBase
    {
        public string GetTokenID()
        {
            return "iOS No implementation";
        }

        public void SubscribeToNotifications(string group)
        {
            
        }
    }
}