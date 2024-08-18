using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Entities.ViewModels.Responses;
using System.Linq.Expressions;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface IBaseService<B, R, C, E>
        where B : Base
        where R : ResponseBase<B>
        where C : CreateBase<B>
        where E : EditBase <B>
    {

        #region Queries
        public R GetById(int id);
        public IEnumerable<R> GetAll();

        #endregion

        #region Commands
        public Task Add(C createRequest);
        public Task Edit(E editRequest);
        public Task Delete(int id);
        #endregion

        #region Requests
        public C GetCreateRequest();
        public E GetEditRequest(int id);
        public R GetDeleteRequest(int id);
        #endregion

    }
}
