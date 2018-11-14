using CN.Common.Contracts.IViewModels;
using CN.Common.Models;
using CN.Simulator.Containers;
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

namespace CN.Simulator.Windows
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        public ISimulatorViewModel viewModel { get; set; }
        public SimulatorWindow(User user)
        {
            InitializeComponent();
            viewModel = SimulatorContainer.container.GetInstance<ISimulatorViewModel>();
            DataContext = viewModel;
        }
    }
}
