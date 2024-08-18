using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class Ingredient : Base
    {
        public Ingredient()
        {
            MedicineIngredients = new HashSet<MedicineIngredient>();
        }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [InverseProperty("Ingredient")]
        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; }
    }
}