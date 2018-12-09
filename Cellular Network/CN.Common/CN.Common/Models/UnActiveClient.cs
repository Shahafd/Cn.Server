using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class UnActiveClient:Client
    {
        public DateTime DateUnactivated { get; set; }
        public UnActiveClient()
        {
            DateUnactivated = DateTime.Now;
        }
    }
}
