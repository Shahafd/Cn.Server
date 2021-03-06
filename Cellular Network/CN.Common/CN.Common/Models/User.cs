﻿using CN.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class User
    {
        public User()
        {

        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
        [NotMapped]
        public int NumOfSales { get; set; }
    }
}
