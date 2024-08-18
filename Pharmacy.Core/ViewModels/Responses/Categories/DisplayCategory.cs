using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Categories
{
    public class DisplayCategory : ResponseBase<Category>
    {
        public string Name { get; set; }

        #region Model to view
        public override DisplayCategory GetResponse(Category entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayCategory
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return response;
        }
        #endregion

    }
}
