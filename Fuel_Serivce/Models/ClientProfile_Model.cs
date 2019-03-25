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
        public string Address1 { get; set; }

        [Required]
        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }


        [Required]
        public string Zipcode { get; set; }
    }
}
