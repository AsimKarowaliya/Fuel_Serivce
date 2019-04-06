using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Fuel_Service.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int CheckLogin()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Users WHERE Username = '" + UserName + "' AND Password = '" + Password + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            int x = 0;

            
            if (dt.Rows.Count != 0)
            {
                x = Convert.ToInt32(dt.Rows[0][0].ToString());
                return x;
            }
            else
            {
                return 0;
            }
        }
    }
}
