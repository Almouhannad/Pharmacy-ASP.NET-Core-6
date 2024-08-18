using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class Medicine : Base
    {
        public Medicine()
        {
            MedicineCases = new HashSet<MedicineCase>();
            MedicineIngredients = new HashSet<MedicineIngredient>();
        }

        [StringLength(50)]
        public string TradeName { get; set; } = null!;

        [StringLength(50)]
        public string ScientificName { get; set; } = null!;

        [StringLength(50)]
        public string Company { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(50)]
        public string Form { get; set; } = null!;

        [Required]
        public decimal Dose { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Medicines")]
        public virtual Category Category { get; set; } = null!;

        [InverseProperty("Medicine")]
        public virtual ICollection<MedicineCase> MedicineCases { get; set; }

        [InverseProperty("Medicine")]
        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; }
    }
}