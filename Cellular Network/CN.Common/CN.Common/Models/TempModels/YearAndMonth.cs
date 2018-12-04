using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class YearAndMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public YearAndMonth(int Year,int Month)
        {
            this.Year = Year;
            this.Month = Month;
        }
    }
}
