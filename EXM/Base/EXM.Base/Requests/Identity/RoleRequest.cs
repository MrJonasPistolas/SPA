using System.ComponentModel.DataAnnotations;

namespace EXM.Base.Requests.Identity
{
    public class RoleRequest
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
