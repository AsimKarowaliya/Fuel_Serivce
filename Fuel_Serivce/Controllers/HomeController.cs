using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fuel_Serivce.Models;
using Fuel_Service.Models;

namespace Fuel_Serivce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message("Hello");
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
           
        public IActionResult Fuel_Quote_Form()
        {
            return View();
        }

        public IActionResult Fuel_History()
        {
            return View();
        }
        public IActionResult Profile_Management()
        {
            return View();
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
