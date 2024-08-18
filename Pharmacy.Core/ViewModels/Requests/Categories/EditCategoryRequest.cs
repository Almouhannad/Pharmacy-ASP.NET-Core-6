using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Categories
{
    public class EditCategoryRequest : EditBase<Category>
    {

        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        #region Model to view
        public override EditCategoryRequest GetRequest(Category entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditCategoryRequest
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return request;
        }
        #endregion

        #region View to model
        public override Category GetModel()
        {
            var model = base.GetModel();
            model.Name = Name;
            return model;
        }
        #endregion
    }
}
