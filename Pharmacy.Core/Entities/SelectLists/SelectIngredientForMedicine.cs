using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Categories;

namespace Pharmacy.Core.Entities.SelectLists
{
    public class SelectIngredientForMedicine
    {
        public IEnumerable<DisplayIngredient> Ingredients { get; set; }
        public IEnumerable<SelectItem> GetSelectList()
        {
            var selectList = new List<SelectItem>();
            foreach (var ingredient in Ingredients)
            {
                selectList.Add(new SelectItem
                {
                    Key = ingredient.Id,
                    Value = ingredient.Name
                });
            }
            return selectList;
        }
    }
}
