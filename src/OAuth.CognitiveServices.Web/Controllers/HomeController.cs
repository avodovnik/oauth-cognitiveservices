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
using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Verification;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using OAuth.CognitiveServices.Web.Models;

namespace OAuth.CognitiveServices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly SpeakerVerificationServiceClient client;

        public HomeController(IConfiguration config, SpeakerVerificationServiceClient client)
        {
            this.config = config;
            this.client = client;
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

        [HttpGet]
        [Route("voice/enrollment")]
        public async Task<IActionResult> VoiceEnrollment()
        {
            var phrases = await this.client.GetPhrasesAsync("en-US");

            return View(phrases);
        }

        [HttpPost]
        [Route("voice/enrollment")]
        public JsonResult VoiceEnrollmentPost()
        {
            return Json(new { result = "you're a genius" });
        }
    
        public async Task<IActionResult> Test()
        {
            try
            {
                var file = Request.Form.Files.First();

                int outRate = 44000;
                var source = new RawSourceWaveStream(file.OpenReadStream(), new WaveFormat(outRate, 2));
                using (var wavFileReader = new WaveFileReader(source))
                {
                    var resampler = new WdlResamplingSampleProvider(wavFileReader.ToSampleProvider(), 16000);
                    var monoSource = resampler.ToMono().ToWaveProvider16();

                    
                    using (var outputStream = new MemoryStream())
                    {
                        WaveFileWriter.WriteWavFileToStream(outputStream, monoSource);

                        outputStream.Seek(0, SeekOrigin.Begin);

                        var result = await client.VerifyAsync(outputStream, Guid.Parse("fb786241-9f01-41cc-a585-50b65bd52c38"));

                        if (result.Result == Result.Accept)
                        {
                            // verification successful
                        }
                    }
                }
            }
            catch (Exception e)
            {
               int x = 1;
            }

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
