using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CN.Common.Models;

namespace CN.Common.Contracts.IViewModels
{
    public interface IBillDatePickerViewModel
    {
        void OnCbObjectCheckBoxChecked(object sender, EventArgs e);
        void OnCbObjectCheckBoxUnChecked(object sender, EventArgs e);
        void getClientFromWindow(Client client);
    }
}
