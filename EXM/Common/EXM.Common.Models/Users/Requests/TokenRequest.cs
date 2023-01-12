using System.ComponentModel.DataAnnotations;

namespace EXM.Common.Models.Users.Requests
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
