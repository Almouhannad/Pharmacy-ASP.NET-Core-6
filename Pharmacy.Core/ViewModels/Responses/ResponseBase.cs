using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Entities.ViewModels.Responses
{
    public abstract class ResponseBase<T> where T : Base
    {
        public int Id { get; set; }

        #region Model to view
        public abstract ResponseBase<T> GetResponse(T entity);

        #endregion


    }
}
