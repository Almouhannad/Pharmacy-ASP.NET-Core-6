using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Core.Entities.General
{
    public class Category : Base
    {
        public Category()
        {
            Medicines = new HashSet<Medicine>();
        }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("Category")]
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}