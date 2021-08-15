using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApplication.Models;

/*
    @Authors:
        Alex J Zoller

 */


namespace WeatherApplication.Controllers
{
    public class WeatherController : Controller
    {
        public IDataDAO weatherData;

        public WeatherController(IDataDAO dataServices)
        {
            weatherData = dataServices;
        }

        public IActionResult Index()
        {
            Logger.Debug("Entering Index@WeatherController ");
            return View("LocationsHome", weatherData.getAllData());
        }

        public IActionResult OpenWeather(string location)
        {
            Logger.Debug("Entering OpenWeather@WeatherController Location = " + location);
            return View("DisplayWeather", weatherData.getDataByLocation(location));
        }

    }
}
