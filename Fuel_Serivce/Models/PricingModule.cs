using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class PricingModule
    {
        [Required]
        public int Gallons { get; set; }
    }
}
