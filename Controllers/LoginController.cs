using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApplication.Models;
using WeatherApplication.Services.LoginData;

namespace WeatherApplication.Controllers
{
    public class LoginController : Controller
    {
        public IUserDAO ss { get; set; }

        public LoginController(IUserDAO data)
        {
            ss = data;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
           
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ProcessLogin(string username, string password)
        {
            Logger.Debug("Entering ProcessLogin@LoginController");
            string outcome = "LoginSuccess";
            if (!ss.login(username, password))
                outcome = "Login";

            Logger.Error("Error: with logging in ProcessLogin@LoginController");
            return View(outcome);
        }

        public IActionResult ProcessRegister(string email, string username, string password)
        {
            Logger.Debug("Entering ProcessRegister@LoginController");
            if (!ss.registerNewUser(email, username, password))
                return View("Register");

            Logger.Error("Error: with registering ProcessRegister@LoginController  email: " + email + ", username: " + username + ", password: " + password);
            return View("Login");
        }


    }
}
