using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Fuel_Service.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string RetypedPassword { get; set; }

        public bool CheckUser()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Users WHERE Username = '" + UserName + "'";

            

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            if (dt.Rows.Count != 0)
            {

                return true;                           //user exists.
            }
            else
            {
                
                return false;                            //user dosen't exist
            }

        }

        
        public int SaveNewUser()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Users (UserType, Username, Password) VALUES ('Client', '" + UserName + "', '"+ Password +"')";


          
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}
