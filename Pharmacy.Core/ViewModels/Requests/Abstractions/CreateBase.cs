using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Abstractions
{
    public abstract class CreateBase <T> where T : Base
    {
        #region Model to view
        public abstract CreateBase<T> GetRequest();
        #endregion

        #region View to model
        public virtual T GetModel()
        {
            var model = Activator.CreateInstance(typeof(T)) as T;
            model.Id = 0;
            return model;
        }
        #endregion
    }
}
