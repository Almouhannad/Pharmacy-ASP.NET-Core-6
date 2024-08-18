using Pharmacy.Core.Entities.ViewModels.Responses.Patients;
namespace Pharmacy.Core.Entities.SelectLists
{
    public class SelectPatientForCase
    {
        public IEnumerable<DisplayPatient> Patients { get; set; }
        public IEnumerable<SelectItem> GetSelectList()
        {
            var selectList = new List<SelectItem>();
            foreach (var patient in Patients)
            {
                selectList.Add(new SelectItem
                {
                    Key = patient.Id,
                    Value = patient.FirstName + " " + patient.LastName
                });
            }
            return selectList;
        }
    }
}
