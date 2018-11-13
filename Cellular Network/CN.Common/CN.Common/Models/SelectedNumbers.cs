using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class SelectedNumbers
    {
        public SelectedNumbers()
        {

        }
        public SelectedNumbers(string FirstNumber,string SecondNumber,string ThirdNumber)
        {
            this.FirstNumber = FirstNumber;
            this.SecondNumber = SecondNumber;
            this.ThirdNumber = ThirdNumber;
        }
        public int ID { get; set; }
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string ThirdNumber { get; set; }
    }
}
