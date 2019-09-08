using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fuel_Serivce.Models;
using Fuel_Service.Models;
using Microsoft.AspNetCore.Authorization;
using Fuel_Service.Repository;
using System.Data.SqlClient;
using System.Data;




namespace Fuel_Serivce.Controllers
{
    
    public static class Globals
    {
        public static int userkey;
    }

  

    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Profiles WHERE UserKey = '" + Globals.userkey + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string name = "Client";

            if (reader.Read())
            {
                name = reader.GetString(1);

            }
            con.Close();

            if (name != "Client")
            {
                ViewBag.Result = name;
            }
            else
            {
                ViewBag.Result = "Please complete your Profile";
            }

            string y = Globals.userkey.ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()        //IN LOGIN
        {

            return View();
        }


        [HttpPost]
        public IActionResult CheckUser()    //IN LOGIN
        {
            LoginModel lmodel = new LoginModel();
            lmodel.UserName = HttpContext.Request.Form["username"].ToString();
            lmodel.Password = HttpContext.Request.Form["password"].ToString();
            string usertype;
            int result;



            (result, usertype) = lmodel.CheckLogin();
            if (result > 0)
            {
                if (usertype == "Admin")
                {
                    Globals.userkey = result;

                    SqlConnection con = new SqlConnection(GetConString.ConString());


                    string query = "SELECT QuoteID, FullName, AddressOne, OrderDate, Gallons, Total" +
                                   " FROM Profiles" +
                                   " INNER JOIN Quotes" +
                                   " ON Profiles.UserKey = Quotes.UserKey";


                    SqlCommand cmd = new SqlCommand(query, con);

                    var listoforders = new List<All_Order>();

                    using (con)
                    {
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();


                        while (rdr.Read())
                        {
                            var order = new All_Order();
                            order.quoteid = (int)rdr["QuoteID"];
                            order.name = (string)rdr["FullName"];
                            order.address = (string)rdr["AddressOne"];
                            order.orderdate = (string)rdr["OrderDate"];
                            order.gallons = (Int64)rdr["Gallons"];

                            object value = rdr.GetValue(rdr.GetOrdinal("Total"));
                            order.total = Convert.ToDouble(value);

                            listoforders.Add(order);
                        }

                    }

                    return View("CompanyHistory", listoforders);
                }
                else
                {
                    Globals.userkey = result;
                    return View("Index");
                }
               
            }
            else
            {

                ViewBag.Result = "Wrong Username or Password";
                return View("Login");
            }


        }

        public IActionResult CompanyHistory()
        {
            return View();

        }

        public IActionResult Main()
        {
            return View();
        }
        

        public IActionResult Fuel_Quote_Form()      //Fuel Quote From
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Profiles WHERE UserKey = '" + Globals.userkey + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            

            var quote = new FuelQuote_Model();
            if (reader.Read())
            {
                quote.Address = reader.GetString(2) + " " + reader.GetString(5);
                
            }
            quote.Gallons = 0;
            quote.Date = DateTime.Now.ToString("dd/MM/yyyy");
            quote.viewDate = DateTime.Now;
            

            con.Close();
            return View(quote);

            
        }

        [HttpPost]
        public IActionResult GetPrice()         //Fuel Quote From
        {
            var quote = new FuelQuote_Model();
            quote.Gallons = Convert.ToDouble(HttpContext.Request.Form["Gallons"]);
            quote.Address = HttpContext.Request.Form["Address"].ToString();
            quote.viewDate = Convert.ToDateTime(HttpContext.Request.Form["Date"]);

            var pricemodule = new PricingModule();
            double calc_suggestprice, calc_totalprice;
            


            (calc_suggestprice, calc_totalprice) = pricemodule.CalculatePrice(quote.Gallons, quote.Address, quote.viewDate, Globals.userkey);

            quote.SuggestPrice = calc_suggestprice;
            quote.TotalPrice = calc_totalprice;
            
            
            return this.View("Fuel_Quote_Form", quote);
        }

        public IActionResult CheckOut()
        {
            var tuple = new Tuple<FuelQuote_Model, PricingModule>(new FuelQuote_Model(), new PricingModule());
            tuple.Item1.Gallons = Convert.ToDouble(HttpContext.Request.Form["Gallons"]);
            tuple.Item1.Address = HttpContext.Request.Form["Address"].ToString();
            tuple.Item1.Date = (Convert.ToDateTime(HttpContext.Request.Form["Date"])).ToString("MM/dd/yyyy");
            tuple.Item1.SuggestPrice = Convert.ToDouble(HttpContext.Request.Form["sprice"]);
            tuple.Item1.TotalPrice = Convert.ToDouble(HttpContext.Request.Form["tprice"]);
            tuple.Item1.viewDate = Convert.ToDateTime(HttpContext.Request.Form["Date"]);

            int cansubmit = tuple.Item1.CheckSave();

            if (cansubmit == -1)
            {
                ViewBag.Result = "Please complete profile and use Get Price before checking out.";
                return View("Fuel_Quote_Form", tuple.Item1);
            }

            tuple.Item1.SaveQuote(Globals.userkey);

            return View("Receipt",tuple);
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {

            var quote = new FuelQuote_Model();
            quote.Gallons = Convert.ToDouble(HttpContext.Request.Form["Gallons"]);
            quote.Address = HttpContext.Request.Form["Address"].ToString();
            quote.Date = (Convert.ToDateTime(HttpContext.Request.Form["Date"])).ToString("MM/dd/yyyy");
            quote.SuggestPrice = Convert.ToDouble(HttpContext.Request.Form["sprice"]);
            quote.TotalPrice = Convert.ToDouble(HttpContext.Request.Form["tprice"]);
            quote.viewDate = Convert.ToDateTime(HttpContext.Request.Form["Date"]);

            int result = quote.SaveQuote(Globals.userkey);
            if (result > 0)
            {
                ViewBag.Result = "Order Submited!";
            }

            return this.View("Receipt", quote);
        }



        public IActionResult Fuel_History()
        {
            
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Quotes WHERE UserKey = '" + Globals.userkey + "'";
            SqlCommand cmd = new SqlCommand(query, con);

            var model = new List<Order_Model>();
            using (con)
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var order = new Order_Model();
                    order.quoteid = (int)rdr["QuoteID"];
                    order.orderdate = (string)rdr["OrderDate"];
                    order.gallons = (Int64)rdr["Gallons"];

                    object value = rdr.GetValue(rdr.GetOrdinal("Total"));
                    order.total = Convert.ToDouble(value);

                    model.Add(order);
                }

            }

            return View(model);
        }

        
        public IActionResult Profile_Management(ClientProfile_Model newclient)   //IN PROFILE DEFAULT
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "SELECT * FROM Profiles WHERE UserKey = '" + Globals.userkey + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
           
            
            var client = new ClientProfile_Model();

            if (reader.Read())
            {
                client.Fullname = reader.GetString(1);
                client.Address1 = reader.GetString(2);
                client.Address2 = reader.GetString(3);
                client.City = reader.GetString(4);
                client.State = reader.GetString(5);
                client.Zipcode = reader.GetString(6);
            }

            con.Close();
            return View(client);
        }

        [HttpPost]
        public IActionResult GetClientInfo()         //IN PROFILE
        {
            ClientProfile_Model client = new ClientProfile_Model();
            client.Fullname = HttpContext.Request.Form["fullname"].ToString();
            client.Address1 = HttpContext.Request.Form["addressline1"].ToString();
            client.Address2 = HttpContext.Request.Form["addressline2"].ToString();
            client.City = HttpContext.Request.Form["cityname"].ToString();
            client.State = HttpContext.Request.Form["state"].ToString();
            client.Zipcode = HttpContext.Request.Form["zipcode"].ToString();

            int result = client.SaveClientProfile(Globals.userkey);

            if (result > 0)
            {
                ViewBag.Result = "Data saved successfully!";
            }

            return View("Profile_Management", client);
        }

        


        public IActionResult Register()                 //IN REGISTER
        {
            
            return View();
        }
        
        [HttpPost]
        public IActionResult GetUserInfo()              //IN REGISTER
        {
            
            RegisterModel newuser = new RegisterModel();
            newuser.UserName = HttpContext.Request.Form["UserName"].ToString();
            newuser.Password = HttpContext.Request.Form["Password"].ToString();
            newuser.RetypedPassword = HttpContext.Request.Form["retype_password"].ToString();
            
            bool userexists = newuser.CheckUser();
            
            if (newuser.Password != newuser.RetypedPassword || userexists)
            {
                ViewBag.Result = "Passwords do not match! OR Username already Exists!";
                return View("Register");

            }
            else
            {
                int result = newuser.SaveNewUser();
               
                if (result > 0)
                {
                    ViewBag.Result = "Data saved successfully!";
                }
                else
                {
                    ViewBag.Result = "Something went wrong!";
                }
                return View("Login");
                

            }
            

            
            

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
