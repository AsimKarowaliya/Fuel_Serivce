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
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public IActionResult CheckUser()
        {
            LoginModel lmodel = new LoginModel();
            lmodel.UserName = HttpContext.Request.Form["username"].ToString();
            lmodel.Password = HttpContext.Request.Form["password"].ToString();

            int result = lmodel.CheckLogin();
            if (result > 0)
            {
                Globals.userkey = result;
                return View("Index");
            }
            else
            {

                ViewBag.Result = "Wrong Username or Password";
                return View("Login");
            }


        }


        public IActionResult Main()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Fuel_Quote_Form()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Fuel_Quote_Form(FuelQuote_Model vm)
        {
            return View();
        }
        public IActionResult Fuel_History()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Profile_Management()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Profile_Management(ClientProfile_Model client)
        {
            string name = "Asim";
            string address1 = "spring";
            string address2 = "idks";
            string city = "Houston";
            string state = "TX";
            string zipcode = "77379";
            if (ModelState.IsValid) {

                if (client.Fullname == name && client.Address1 == address1 && client.Address2 == address2 && client.City == city && client.State == state && client.Zipcode == zipcode)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "worng input");
            }


            return View(client);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetUserInfo()
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
