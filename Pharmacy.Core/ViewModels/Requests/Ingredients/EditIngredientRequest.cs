using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Ingredients
{
    public class EditIngredientRequest : EditBase<Ingredient>
    {

        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s\.,:;!?()\-_]+$",
            ErrorMessage = "Invalid description. Only English letters, numbers, and punctuation marks are allowed.")]
        public string? Description { get; set; } = null;


        #region Model to view
        public override EditIngredientRequest GetRequest(Ingredient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditIngredientRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
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
