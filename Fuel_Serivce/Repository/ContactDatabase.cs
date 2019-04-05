using Fuel_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuel_Service.Repository
{
    //Layer connecting with Database to execute any queries.
    public class ContactDatabase
    {
        public static bool RegisterUser(RegisterModel newuser)  //Function to register new user and add them to Database
            
        {
           
            var connectionString = "Data Source=.;Initial Catalog=FuelService;Integrated Security=True";                        //create connectionstring connecting to FuelService Database
            var query = "INSERT INTO Users (UserType, Username, Password) VALUES ('@UserType', '@Username', '@Password')";      //

            query = query.Replace("@UserType", "Client").Replace("@Username", newuser.UserName).Replace("@Password", newuser.Password); 


        }





    }
}
