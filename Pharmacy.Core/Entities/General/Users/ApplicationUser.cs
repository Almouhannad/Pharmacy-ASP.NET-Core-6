using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General.Users
{
    public class ApplicationUser : IdentityUser
    {

        public string? Name { get; set; } = null;

        [ForeignKey("Patient")]
        public int? PatientId { get; set; } = null;
        public Patient? Patient { get; set; } = null;
    }
}
