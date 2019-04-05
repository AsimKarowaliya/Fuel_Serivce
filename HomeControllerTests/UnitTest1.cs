using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuel_Serivce.Controllers;
using Fuel_Service.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace HomeControllerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void test_Index()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;
            
            
            Assert.IsNotNull(result);
            
        }

        [TestMethod]
        public void test_Login()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Login() as ViewResult;
            

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void test_Main()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Main() as ViewResult;


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void test_Fuel_Quote_Form()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Fuel_Quote_Form() as ViewResult;


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void test_Fuel_History()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Fuel_History() as ViewResult;


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void test_Profile_Management()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Profile_Management() as ViewResult;


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void test_Register()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Register() as ViewResult;


            Assert.IsNotNull(result);

        }

        [TestClass]
        public class modeltests
        {
            [TestMethod]
            public void test_LoginModel()
            {
                var login = new LoginModel();

                login.UserName = "user";
                login.Password = "password";

                Assert.IsNotNull(login.UserName);
                Assert.IsNotNull(login.Password);

            }

            [TestMethod]
            public void test_ClientProfile_Model()
            {
                var client = new ClientProfile_Model();

                client.Fullname = "asim";
                client.Address1 = "louetta";
                client.Address2 = "ash";
                client.City = "spring";
                client.State = "texas";
                client.Zipcode = "71234";

                Assert.AreEqual("asim", client.Fullname);
                Assert.AreEqual("louetta", client.Address1);
                Assert.AreEqual("ash", client.Address2);
                Assert.AreEqual("spring", client.City);
                Assert.AreEqual("texas", client.State);
                Assert.AreEqual("71234", client.Zipcode);

            }

            [TestMethod]
            public void test_FuelQuote_Model()
            {
                var fuelquote = new FuelQuote_Model();

                fuelquote.Gallons = 4;

                Assert.AreEqual(fuelquote.Gallons, 4);
                
            }


        }


    }

  
}
