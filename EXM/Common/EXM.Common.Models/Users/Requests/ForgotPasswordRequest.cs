using System.ComponentModel.DataAnnotations;

namespace EXM.Common.Models.Users.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
