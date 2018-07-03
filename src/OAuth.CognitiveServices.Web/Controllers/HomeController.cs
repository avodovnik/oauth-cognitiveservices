using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OAuth.CognitiveServices.Web.Models;

namespace OAuth.CognitiveServices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;

        public HomeController(IConfiguration config)
        {
            this.config = config;
        }
        public IActionResult Choose()
        {
            return View();
        }

        public IActionResult Face()
        {
            return Content(this.config["CognitiveServices:SpeakerRecognitionKey"]);
            return Content("Face API");
        }

        public IActionResult Voice()
        {
            return View();
        }
    }
}
