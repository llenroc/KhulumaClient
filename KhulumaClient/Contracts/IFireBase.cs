using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhulumaClient.Contracts
{
    public interface IFireBase
    {

        void FCMSubscribe(string currentGroup, string khuGroup);
    }
}
