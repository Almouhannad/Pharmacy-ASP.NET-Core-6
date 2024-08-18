using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Ingredients
{
    public class CreateIngredientRequest : CreateBase<Ingredient>
    {

        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s\.,:;!?()\-_]+$",
            ErrorMessage = "Invalid description. Only English letters, numbers, and punctuation marks are allowed.")]

        public string? Description { get; set; } = null;


        #region Model to view
        public override CreateIngredientRequest GetRequest()
        {
            var request = new CreateIngredientRequest();
            return request;
        }
        #endregion

        #region View to model
        public override Ingredient GetModel()
        {
            var model = base.GetModel();
            model.Name = Name;
            model.Description = Description;
            return model;
        }
        #endregion

    }
}
