using CN.Common.Contracts.IViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CN.CRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ILoginViewModel viewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            viewModel = CrmContianer.container.GetInstance<ILoginViewModel>();
            DataContext = viewModel;
        }
    }
}
