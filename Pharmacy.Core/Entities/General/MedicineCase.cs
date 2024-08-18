using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class MedicineCase : Base
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int CaseId { get; set; }

        [Required]
        public int Times { get; set; }

        [ForeignKey("CaseId")]
        [InverseProperty("MedicineCases")]
        public virtual Case Case { get; set; } = null!;

        [ForeignKey("MedicineId")]
        [InverseProperty("MedicineCases")]
        public virtual Medicine Medicine { get; set; } = null!;
    }
}