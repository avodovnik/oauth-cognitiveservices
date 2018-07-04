using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth.CognitiveServices.Web.Models;

namespace OAuth.CognitiveServices.Web.Controllers
{
    [Route("api/auth")]
    public class AuthorizationController : Controller
    {
        [HttpGet]
        [Route("code")]
        public ActionResult Authorize(AuthorizationCodeModel model)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed);
            }

            // TODO: do validation of the client id, scope, callback id, etc., and then let the user login

            return RedirectToAction("Choose", "Home");

            // ignore all this for now

            UriBuilder builder = new UriBuilder(model.redirect_uri);

            var qs = QueryString.FromUriComponent(builder.Query);
            //qs = qs.Add("code", Guid.NewGuid().ToString("N"));
            qs = qs.Add("code", "test123");
            qs= qs.Add("state", DateTime.UtcNow.Ticks.ToString());

            builder.Query = qs.ToUriComponent();

            
            return Redirect(builder.ToString());
        }

        /// <summary>
        /// Get the token back for the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        [Route("token")]
        public ActionResult Token(TokenRequestModel model)
        {
            return Json(new { message = "OK" });
        }

        /// <summary>
        /// Complete user verification
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// [HttpGet]
        [Route("done")]
        public ActionResult Done(string userId ="")
        {
            return Content("Thanks!");
        }

        public class TokenRequestModel
        {
            public string grant_type { get; set; }
            public string code { get; set; }
            public string redirect_uri { get; set; }
            public string client_id { get; set; }
        }
    }
}
