using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<IActionResult> Test()
        {
            var file = Request.Form.Files.First();
            //using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(test.Recording)))
            //{
            Microsoft.ProjectOxford.SpeakerRecognition.SpeakerVerificationServiceClient client
                = new Microsoft.ProjectOxford.SpeakerRecognition.SpeakerVerificationServiceClient(
                    this.config["CognitiveServices:SpeakerRecognitionKey"]);

            var result = await client.VerifyAsync(file.OpenReadStream(), Guid.Parse("fb786241-9f01-41cc-a585-50b65bd52c38"));
            //}


            return Ok();
        }

        //    
        public class VoicePassage
        {
            public string Title { get; set; }
            public Stream FileName { get; set; }
            public string Recording { get; set; }
        }
    }
}
