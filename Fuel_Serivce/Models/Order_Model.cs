using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class Order_Model
    {
        public int quoteid { get; set; }
        public int userkey { get; set; }
        public string orderdate { get; set; }
        public Int64 gallons { get; set; }
        public double total { get; set; }
        
    }
}
