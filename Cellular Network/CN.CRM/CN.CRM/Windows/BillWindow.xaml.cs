using CN.Common.Contracts.IViewModels;
using CN.Common.Models.TempModels;
using CN.CRM.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CN.CRM.Windows
{
    /// <summary>
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        public IBillViewModel viewModel { get; set; }

        public BillWindow(ClientBill clientBill)
        {
            InitializeComponent();
            viewModel = CrmContianer.container.GetInstance<IBillViewModel>();
            DataContext = viewModel;
            viewModel.GetClientBill(clientBill);
            viewModel.GetGridFromWindow(DynamicGrid);
        }
    }
}
