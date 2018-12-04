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
    /// Interaction logic for CrmWindow.xaml
    /// </summary>
    public partial class CrmWindow : Window
    {
        public ICrmViewModel viewModel { get; set; }
        public CrmWindow(User loggedIn)
        {
            InitializeComponent();
            viewModel = CrmContianer.container.GetInstance<ICrmViewModel>();
            DataContext = viewModel;
            viewModel.UpdateUser(loggedIn);
        }

        private void searchInputTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.SearchInputChanged(searchInputTbx.Text);
        }
    }
}
