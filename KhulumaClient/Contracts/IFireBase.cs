using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhulumaClient.Contracts
{
    public interface IFireBase
    {
        string GetTokenID();
        void SubscribeToNotifications(string group);
    }
}
