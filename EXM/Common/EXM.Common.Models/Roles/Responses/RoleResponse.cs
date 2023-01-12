using System.ComponentModel.DataAnnotations;

namespace EXM.Common.Models.Roles.Responses
{
    public class RoleResponse
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
