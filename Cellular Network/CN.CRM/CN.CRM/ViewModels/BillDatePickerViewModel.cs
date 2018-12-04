using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CN.CRM.ViewModels
{
    public class BillDatePickerViewModel : IBillDatePickerViewModel
    {
        IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        public ObservableCollection<SelectableObject<string>> cbObjects { get; set; }


        public BillDatePickerViewModel(ILogger logger, IHttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            cbObjects = new ObservableCollection<SelectableObject<string>> { new SelectableObject<string>("111", false), new SelectableObject<string>("222", false), new SelectableObject<string>("333", false) };
        }

        /////////////////////////////////////////////////////////
        public void OnCbObjectCheckBoxChecked(object sender, EventArgs e)
        {
            //logger.Print("in viewmodel-OnCbObjectCheckBoxChecked ");
            SelectableObject<string> s = (SelectableObject<string>)sender;

        }
        public void OnCbObjectCheckBoxUnChecked(object sender, EventArgs e)
        {
            //logger.Print("in viewmodel-OnCbObjectCheckBoxChecked ");


        }

        public void OnCbObjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedItem = null;
        }
        ////////////////////////////////////////////////////////






        ////////////////////////////////////////////////
        public class SelectableObject<T>
        {
            public bool IsSelected { get; set; }
            public T ObjectData { get; set; }

            public SelectableObject(T objectData)
            {
                ObjectData = objectData;
            }

            public SelectableObject(T objectData, bool isSelected)
            {
                IsSelected = isSelected;
                ObjectData = objectData;
            }
        }
        /////////////////////////////////////////////////////////

    }
}

