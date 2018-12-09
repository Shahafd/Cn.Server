using CN.Common.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
   public class SessionData : ISessionData
    {
        int loggedInID;
        public int GetLoggedInID()
        {
            return loggedInID;
        }

        public void updateLoggedInID(int id)
        {
            loggedInID = id;
        }
    }
}
