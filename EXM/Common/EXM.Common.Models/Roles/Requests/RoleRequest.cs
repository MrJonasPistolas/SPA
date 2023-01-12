using System.ComponentModel.DataAnnotations;

namespace EXM.Common.Models.Roles.Requests
{
    public class RoleRequest
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
