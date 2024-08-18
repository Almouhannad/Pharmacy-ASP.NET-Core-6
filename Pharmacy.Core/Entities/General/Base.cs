using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Core.Entities.General
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
