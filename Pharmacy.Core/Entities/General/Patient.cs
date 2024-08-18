using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class Patient : Base
    {
        public Patient()
        {
            Cases = new HashSet<Case>();
        }

        [Display(Name = "First name")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [InverseProperty("Patient")]
        public virtual ICollection<Case> Cases { get; set; }

    }
}
