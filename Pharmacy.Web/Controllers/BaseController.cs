using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Entities.ViewModels.Responses;
using Pharmacy.Core.Interfaces.IServices;

namespace Pharmacy.Web.Controllers
{
    public class BaseController<B, R, C, E> : Controller
        where B : Base
        where R : ResponseBase<B>
        where C : CreateBase<B>
        where E : EditBase<B>
    {
        protected readonly IServicePool _servicePool;
        public BaseController(IServicePool servicePool)
        {
            _servicePool = servicePool;
        }


        #region Basic
        public virtual IActionResult Index([FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                int pageSize = 7;
                var responses = _servicePool.Set<B, R, C, E>().GetAll()
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Set<B, R, C, E>().GetAll().Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }
        public virtual IActionResult Details(int id)
        {
            try
            {
                return View(_servicePool.Set<B, R, C, E>().GetById(id));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        #region Create
        public virtual IActionResult Create()
        {
            try
            {
                return View(_servicePool.Set<B, R, C, E>().GetCreateRequest());
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(C createRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Set<B, R, C, E>().Add(createRequest);
                    return RedirectToAction(nameof(Index));
                }
                return View(createRequest);
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }

        #endregion

        #region Edit
        public virtual IActionResult Edit(int id)
        {
            try
            {
                return View(_servicePool.Set<B, R, C, E>().GetEditRequest(id));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(E editRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Set<B, R, C, E>().Edit(editRequest);
                    return RedirectToAction(nameof(Index));
                }
                return View(editRequest);
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }
        #endregion

        #region Delete
        public virtual IActionResult Delete(int id)
        {
            try
            {
                return View(_servicePool.Set<B, R, C, E>().GetDeleteRequest(id));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                await _servicePool.Set<B, R, C, E>().Delete(id);
                return RedirectToAction(nameof(Index));
            }

            catch(Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
        }
        #endregion

        #endregion


    }
}
