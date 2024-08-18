using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Responses;
using Pharmacy.Core.Exceptions;

namespace Pharmacy.Core.Entities.ViewModels.Response.Ingredients
{
    public class DisplayIngredient : ResponseBase<Ingredient>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region Model to view
        public override DisplayIngredient GetResponse(Ingredient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayIngredient
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
            return response;
        }
        #endregion

    }
}
