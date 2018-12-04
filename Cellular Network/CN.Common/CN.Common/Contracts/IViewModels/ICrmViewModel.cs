using CN.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IViewModels
{
    public interface ICrmViewModel
    {
        void UpdateUser(User user);
        void LoadClients();
        void SearchInputChanged(string input);
    }
}
