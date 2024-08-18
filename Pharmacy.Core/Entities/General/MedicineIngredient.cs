using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class MedicineIngredient : Base
    {
        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int IngredientId { get; set; }

        [Required]
        public decimal Ratio { get; set; }

        [ForeignKey("IngredientId")]
        [InverseProperty("MedicineIngredients")]
        public virtual Ingredient Ingredient { get; set; } = null!;

        [ForeignKey("MedicineId")]
        [InverseProperty("MedicineIngredients")]
        public virtual Medicine Medicine { get; set; } = null!;

    }
}