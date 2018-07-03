using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Test(VoicePassage test)
        {

            return Ok();
        }


        public class VoicePassage
        {
            public string Title { get; set; }
            public string FileName { get; set; }
            public string Recording { get; set; }
        }
    }
}
