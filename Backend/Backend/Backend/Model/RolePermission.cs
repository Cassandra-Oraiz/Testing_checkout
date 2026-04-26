using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Backend.Model
{
    public class RolePermission
    {
        [Required]
        public required string Role_ID { get; set; } // Composite Key

        [Required]
        public required int Permission_ID { get; set; } // Composite Key Relation to Permission ID Superkey

        public IdentityRole? Role { get; set; } // For Navigation

        public Permission? Permission_Entity { get; set; } // For Navigation
    }
}