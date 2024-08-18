using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;

namespace Pharmacy.Core.Entities.SelectLists
{
    public class SelectMedicineForCase
    {
        public IEnumerable<DisplayMedicine> Medicines { get; set; }
        public IEnumerable<SelectItem> GetSelectList()
        {
            var selectList = new List<SelectItem>();
            foreach (var medicine in Medicines)
            {
                selectList.Add(new SelectItem
                {
                    Key = medicine.Id,
                    Value = medicine.TradeName
                });
            }
            return selectList;
        }
    }
}
