using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CN.Common.Models;

namespace CN.Common.Contracts.IViewModels
{
    public interface IAddEditLineViewModel
    {
        void GetData(Client client, bool newLine);
        void GetLoggedInUser(User loggedInUser);
      
    }
}
