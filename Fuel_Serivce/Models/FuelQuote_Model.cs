using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Models
{
    public class FuelQuote_Model
    {
        [Required]
        public double Gallons { get; set; }

        public string Address { get; set; }

        public string Date { get; set; }

        public double SuggestPrice { get;  set; }

        public double TotalPrice { get; set; }

        public DateTime viewDate { get; set; }

        public int SaveQuote(int userkey)
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Quotes (UserKey, OrderDate, Gallons, Total) VALUES ('" +userkey+"', '" + Date + "', '" + Gallons + "', '"+TotalPrice+"')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;

            
        }

        public int CheckSave()
        {
            int x;

            if (SuggestPrice == 0 | TotalPrice == 0 | Address == "")
            {
                x = -1;
            }
            else
            {
                x = 1;
            }


            return x;
        }
        
    }
}
