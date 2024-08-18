using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Entities.ViewModels.Responses;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;
using System.Linq.Expressions;

namespace Pharmacy.Application.DomainServices
{
    public class BaseService<B, R, C, E> : IBaseService<B, R, C, E>
        where B : Base
        where R : ResponseBase<B>
        where C : CreateBase<B>
        where E : EditBase<B>

    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Queries

        public R GetById(int id)
        {
            try
            {
                var entitiy = _unitOfWork.Set<B>().GetById(id);
                var response = Activator.CreateInstance(typeof(R)) as R;
                return response.GetResponse(entitiy) as R;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<R> GetAll()
        {
            try
            {
                var entities = _unitOfWork.Set<B>().GetAll()
                    .OrderByDescending(e => e.Id);

                var responses = new List<R>();
                foreach (var entitiy in entities)
                {
                    var response = Activator.CreateInstance(typeof(R)) as R;
                    responses.Add(response.GetResponse(entitiy) as R);
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion

        #region Commands
        public async Task Add(C createRequest)
        {
            try
            {
                _unitOfWork.Set<B>().Add(createRequest.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task Edit(E editRequest)
        {
            try
            {
                _unitOfWork.Set<B>().Update(editRequest.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                _unitOfWork.Set<B>().Delete(id);
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Requests
        public virtual C GetCreateRequest()
        {
            try
            {
                var createRequest = Activator.CreateInstance(typeof(C)) as C;
                return createRequest.GetRequest() as C;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public virtual E GetEditRequest(int id)
        {
            try
            {
                var entity = _unitOfWork.Set<B>().GetById(id);
                var editRequest = Activator.CreateInstance(typeof(E)) as E;
                return editRequest.GetRequest(entity) as E;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public R GetDeleteRequest (int id)
        {
            try
            {
                var request = GetById(id);
                if (request == null)
                {
                    throw new NotFoundException();
                }
                return request;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }

}
