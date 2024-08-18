using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        #region Queries

        #endregion

        #region Commands
        public Patient CreatePatientAsync(string firstName, string lastName);
        #endregion
    }
}
