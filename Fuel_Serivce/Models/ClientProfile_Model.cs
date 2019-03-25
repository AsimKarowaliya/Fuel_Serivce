using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class ClientProfile_Model
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Address { get; set; }
        
        [Required]
        public string city { get; set; }

        [Required]
        public int zipcode { get; set; }
    }
}
