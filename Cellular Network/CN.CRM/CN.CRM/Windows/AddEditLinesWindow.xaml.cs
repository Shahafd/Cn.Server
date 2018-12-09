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
    /// Interaction logic for AddEditLinesWindow.xaml
    /// </summary>
    public partial class AddEditLinesWindow : Window
    {
        public IAddEditLineViewModel viewModel { get; set; }
        public AddEditLinesWindow(Client client, bool newLine, User loggedInUser = null)
        {
            InitializeComponent();
            viewModel = CrmContianer.container.GetInstance<IAddEditLineViewModel>();
            DataContext = viewModel;
            viewModel.GetData(client, newLine);
            if (loggedInUser != null)
            {
                viewModel.GetLoggedInUser(loggedInUser);
            }
        }
    }
}
