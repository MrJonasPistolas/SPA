using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
