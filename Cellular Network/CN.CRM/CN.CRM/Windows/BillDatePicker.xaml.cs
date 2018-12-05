using CN.Common.Contracts.IViewModels;
using CN.Common.Models;
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
    /// Interaction logic for BillDatePicker.xaml
    /// </summary>
    public partial class BillDatePicker : Window
    {
        public IBillDatePickerViewModel viewModel { get; set; }


        public BillDatePicker(Client client)
        {
            InitializeComponent();
            viewModel = CrmContianer.container.GetInstance<IBillDatePickerViewModel>();
            DataContext = viewModel;
            // this.currentClient = client;
            viewModel.getClientFromWindow(client);
        }





        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            viewModel.OnCbObjectCheckBoxUnChecked(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.OnCbObjectCheckBoxChecked(sender, e);
        }
    }
}
