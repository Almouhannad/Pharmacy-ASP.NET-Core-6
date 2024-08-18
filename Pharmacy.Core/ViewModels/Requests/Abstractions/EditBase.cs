using Pharmacy.Core.Entities.General;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Abstractions
{
    public abstract class EditBase<T> where T : Base
    {
        [Required]
        public int Id { get; set; }

        #region Model to view
        public abstract EditBase<T> GetRequest(T entity);
        #endregion

        #region View to model
        public virtual T GetModel()
        {
            var model = Activator.CreateInstance(typeof(T)) as T;
            model.Id = Id;
            return model;
        }
        #endregion
    }
}
