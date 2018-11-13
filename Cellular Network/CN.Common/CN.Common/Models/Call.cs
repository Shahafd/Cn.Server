﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class Call
    {
        public Call()
        {

        }
        public Call(int LineID,double Duration,double ExternalPrice,string DestinationNumber)
        {
            this.LineID = LineID;
            this.Duration = Duration;
            this.ExternalPrice = ExternalPrice;
            this.DestinationNumber = DestinationNumber;
        }
        public int ID { get; set; }
        public int LineID { get; set; }
        public double Duration { get; set; }
        public double ExternalPrice { get; set; }
        public string DestinationNumber { get; set; }

    }
}