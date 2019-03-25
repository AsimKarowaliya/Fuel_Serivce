using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fuel_Serivce.Models;
using Fuel_Service.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fuel_Serivce.Controllers
{
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
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel vm)
        {
            string username = "Asim";
            string password = "abc";
            if (ModelState.IsValid)
            {
                if (vm.UserName == username && vm.Password == password)
                {
                    return RedirectToAction("Profile_Management", "Home");
                }
                ModelState.AddModelError("", "Invalid login");
            }
            return View(vm);
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
            if (ModelState.IsValid){

              if(client.Fullname == name && client.Address1 == address1 && client.Address2 == address2 && client.City == city &&client.State == state && client.Zipcode == zipcode)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
