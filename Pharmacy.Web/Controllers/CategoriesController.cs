using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Categories;
using Pharmacy.Core.Entities.ViewModels.Responses.Categories;
using Pharmacy.Core.Interfaces.IServices;


namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : BaseController<Category, DisplayCategory, CreateCategoryRequest, EditCategoryRequest>
    {
        public CategoriesController(IServicePool servicePool) : base(servicePool)
        {
        }

        #region Medicines
        [HttpGet]
        public IActionResult Medicines([FromRoute(Name = "id")] int categoryId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                ViewBag.Category = _servicePool.Categories.GetById(categoryId);
                int pageSize = 7;
                var responses = _servicePool.Categories.GetMedicines(categoryId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Categories.GetMedicines(categoryId).Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public IActionResult MedicineDetails ([FromRoute(Name ="id")] int categoryId,
            [FromQuery(Name ="medicineId")] int medicineId)
        {
            try
            {
                return View(_servicePool.Categories.GetMedicineById(categoryId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }
        #endregion


    }
}
