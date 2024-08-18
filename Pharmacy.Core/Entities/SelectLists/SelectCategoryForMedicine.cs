using Pharmacy.Core.Entities.ViewModels.Responses.Categories;

namespace Pharmacy.Core.Entities.SelectLists
{
    public class SelectCategoryForMedicine
    {

        public IEnumerable<DisplayCategory> Categories { get; set; }
        public IEnumerable<SelectItem> GetSelectList()
        {
            var selectList = new List<SelectItem>();
            foreach (var category in Categories)
            {
                selectList.Add(new SelectItem
                {
                    Key = category.Id,
                    Value = category.Name
                });
            }
            return selectList;
        }
    }
}
