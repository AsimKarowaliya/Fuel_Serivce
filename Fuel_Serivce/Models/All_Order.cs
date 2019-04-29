using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class All_Order
    {
        public int quoteid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string orderdate { get; set; }
       
        public Int64 gallons { get; set; }
        public double total { get; set; }

    }
}
