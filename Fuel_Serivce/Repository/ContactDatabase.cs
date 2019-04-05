using Fuel_Service.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;

namespace Fuel_Service.Repository
{
    //Layer connecting with Database to execute any queries.
    public class ContactDatabase
    {
        public static bool RegisterUser(RegisterModel newuser){ //Function to register new user and add them to Database
         
            var connectionString = "Data Source=fuelservice.database.windows.net;Initial Catalog=FuelService;Integrated Security=True; User ID=Asimk786;Password=Asim231725786";                        //create connectionstring connecting to FuelService Database
            var query = "INSERT INTO Users (UserType, Username, Password) VALUES ('@UserType', '@Username', '@Password')";      //writing query
            
            query = query.Replace("@UserType", "Client").Replace("@Username", newuser.UserName).Replace("@Password", newuser.Password);

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch(Exception)
            {
                //throw something
                
                return false;
            }

        }


    }
}
