using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;
using System.Linq.Expressions;

namespace Pharmacy.Infrastructure.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(PharmacyDbContext context) : base(context)
        {
        }

        #region Queries
        public override IEnumerable<Medicine> GetAll()
        {
            try
            {
                return FindAll(e => true, new Expression<Func<Medicine, object>>[] { e => e.Category });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public override Medicine GetById(int id)
        {
            try
            {
                return Find(e => e.Id == id, new Expression<Func<Medicine, object>>[] { e => e.Category });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<MedicineIngredient> GetIngredients(int medicineId)
        {
            try
            {
                var medicineIngredientRepository = new BaseRepository<MedicineIngredient>(_context);
                return medicineIngredientRepository.FindAll(e => e.MedicineId == medicineId,
                    new Expression<Func<MedicineIngredient, object>>[] { e => e.Ingredient });
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Commands
        public void AddIngredient(MedicineIngredient ingredient)
        {
            try
            {
                var medicineIngredientRepository = new BaseRepository<MedicineIngredient>(_context);
                medicineIngredientRepository.Add(ingredient);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public void EditIngredient(MedicineIngredient ingredient)
        {
            try
            {
                var medicineIngredientRepository = new BaseRepository<MedicineIngredient>(_context);
                medicineIngredientRepository.Update(ingredient);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void DeleteIngredient(int medicineId, int ingredientId)
        {
            try
            {
                var medicineIngredientRepository = new BaseRepository<MedicineIngredient>(_context);
                var id = medicineIngredientRepository.Find(e => e.MedicineId == medicineId && e.IngredientId == ingredientId).Id;
                medicineIngredientRepository.Delete(id);
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

    }
}
