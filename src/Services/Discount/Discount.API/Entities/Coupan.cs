﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Entities
{
    public class Coupan
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

    }
}
