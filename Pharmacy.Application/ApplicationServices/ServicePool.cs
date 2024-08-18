using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Responses;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;

namespace Pharmacy.Application.DomainServices
{
    public class ServicePool : IServicePool
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Services
        public ICaseService Cases { get; private set; }

        public ICategoryService Categories { get; private set; }

        public IIngredientService Ingredients { get; private set; }

        public IMedicineService Medicines { get; private set; }

        public IPatientService Patients { get; private set; }

        #endregion


        public ServicePool(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            #region Initialize services
            Cases = new CaseService(unitOfWork);
            Categories = new CategoryService(unitOfWork);
            Ingredients = new IngredientService(unitOfWork);
            Medicines = new MedicineService(unitOfWork);
            Patients = new PatientService(unitOfWork);
            #endregion
        }

        #warning Open-closed principle violation 
        // Better to use factory pattern
        public IBaseService<B, R, C, E> Set<B, R, C, E>()
            where B : Base
            where R : ResponseBase<B>
            where C : CreateBase <B>
            where E : EditBase<B>
        {
            if (typeof(B) == typeof(Case))
                return (IBaseService<B, R, C, E>)Cases;

            else if (typeof(B) == typeof(Category))
                return (IBaseService<B, R, C, E>)Categories;

            else if (typeof(B) == typeof(Ingredient))
                return (IBaseService<B, R, C, E>)Ingredients;

            else if (typeof(B) == typeof(Medicine))
                return (IBaseService<B, R, C, E>)Medicines;

            else if (typeof(B) == typeof(Patient))
                return (IBaseService<B, R, C, E>)Patients;

            else
                throw new InvalidOperationException("No such sevice");
        }

    }
}
