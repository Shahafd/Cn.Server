using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface ISessionData
    {
        int GetLoggedInID();
        void updateLoggedInID(int id);
    }
}
