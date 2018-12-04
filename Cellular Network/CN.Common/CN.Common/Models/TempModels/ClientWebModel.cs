using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CN.Common.Models.TempModels
{
    public class ClientWebModel
    {
        public string ClientName { get; set; }
        public List<SelectListItem> Lines { get; set; }
        public string SelectedLine { get; set; }
        public ClientWebModel()
        {

        }
        public ClientWebModel(string ClientName, List<string> Lines)
        {
            this.Lines = new List<SelectListItem>();
            for (int i = 0; i < Lines.Count; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = Lines[i];
                item.Value = Lines[i];
                this.Lines.Add(item);
            }
        }
    }
}
