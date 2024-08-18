using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Interfaces.IServices;
namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class IngredientsController : BaseController<Ingredient, DisplayIngredient, CreateIngredientRequest, EditIngredientRequest>
    {
        public IngredientsController(IServicePool servicePool) : base(servicePool)
        {
        }


        #region Medicines
        [HttpGet]
        public IActionResult Medicines([FromRoute(Name = "id")] int ingredientId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                ViewBag.Ingredient = _servicePool.Ingredients.GetById(ingredientId);
                int pageSize = 7;
                var responses = _servicePool.Ingredients.GetMedicines(ingredientId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Ingredients.GetMedicines(ingredientId).Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public IActionResult MedicineDetails ([FromRoute(Name = "id")] int ingredientId,
            [FromQuery(Name ="medicineId")] int medicineId)
        {
            try
            {
                return View(_servicePool.Ingredients.GetMedicineById(ingredientId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }
        #endregion

    }
}
