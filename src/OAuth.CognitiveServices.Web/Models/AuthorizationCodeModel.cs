using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth.CognitiveServices.Web.Models
{
    public class AuthorizationCodeModel
    {
        [Required]
        public string client_id { get; set; }

        [Required]
        public string scope { get; set; }

        [Required]
        public string redirect_uri { get; set; }

        [Required]
        public string response_type { get; set; }
    }
}
