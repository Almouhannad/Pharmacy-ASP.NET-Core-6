using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Entities.ViewModels.Responses;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface IServicePool
    {
        #region Services
        public ICaseService Cases { get; }
        public ICategoryService Categories { get; }
        public IIngredientService Ingredients { get; }
        public IMedicineService Medicines { get; }
        public IPatientService Patients { get; }

        #endregion

        public IBaseService<B, R, C, E> Set<B, R, C, E>()
            where B : Base
            where R : ResponseBase<B>
            where C : CreateBase<B>
            where E : EditBase<B>;
    }
}
