using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
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

        public int SaveClientProfile(int userkey)
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Profiles WHERE UserKey = '" + userkey + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();

            if (dt.Rows.Count != 0)
            {
                SqlConnection con2 = new SqlConnection(GetConString.ConString());
                string query2 = "UPDATE Profiles " +
                                " SET FullName = '"+Fullname+"', AddressOne ='"+Address1+"', AddressTwo ='"+Address2+"', City ='"+City+"', State='"+State+"', ZipCode='"+Zipcode+"'" +
                                " WHERE UserKey = '" + userkey + "'";
                
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                con2.Open();
                int i = cmd2.ExecuteNonQuery();
                con2.Close();
                return i;                           //user exists.
            }
            else
            {
                SqlConnection con3 = new SqlConnection(GetConString.ConString());
                string query3 = "INSERT INTO Profiles (UserKey, FullName, AddressOne, AddressTwo, City, State, ZipCode) VALUES ('"+userkey+"','" + Fullname + "', '" + Address1 + "', '" + Address2 + "', '" + City + "', '"+State+"', '" + Zipcode + "')";
                SqlCommand cmd3 = new SqlCommand(query3, con3);
                con3.Open();
                int j = cmd3.ExecuteNonQuery();
                con3.Close();
                return j;

                                           //user dosen't exist
            }

        }

        


    }
}
