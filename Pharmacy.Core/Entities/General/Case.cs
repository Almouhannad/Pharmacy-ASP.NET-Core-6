using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class Case : Base
    {
        public Case()
        {
            MedicineCases = new HashSet<MedicineCase>();
        }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("Cases")]
        public virtual Patient Patient { get; set; } = null!;

        [InverseProperty("Case")]
        public virtual ICollection<MedicineCase> MedicineCases { get; set; }
    }
}
