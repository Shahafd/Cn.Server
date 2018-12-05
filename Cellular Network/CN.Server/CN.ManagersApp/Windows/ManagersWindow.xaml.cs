using CN.Common.Contracts.IViewModels;
using CN.Common.Models;
using CN.ManagersApp.Containers;
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

namespace CN.ManagersApp.Windows
{
    /// <summary>
    /// Interaction logic for ManagersWindow.xaml
    /// </summary>
    public partial class ManagersWindow : Window
    {
        IManagersViewModel viewModel { get; set; }
        public ManagersWindow(User user)
        {
            InitializeComponent();
            viewModel = ManContainer.container.GetInstance<IManagersViewModel>();
            DataContext = viewModel;
            viewModel.GetUser(user);
        }
    }
}
