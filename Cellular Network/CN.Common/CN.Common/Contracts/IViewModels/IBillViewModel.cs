using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CN.Common.Models.TempModels;

namespace CN.Common.Contracts.IViewModels
{
    public interface IBillViewModel
    {
        void GetGridFromWindow(Grid grid);
        void GetClientBill(ClientBill clientBill);
    }
}
