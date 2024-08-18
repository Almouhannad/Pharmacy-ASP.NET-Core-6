using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Interfaces.IServices;

namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class MedicinesController : BaseController<Medicine, DisplayMedicine, CreateMedicineRequest, EditMedicineRequest>
    {
        public MedicinesController(IServicePool servicePool) : base(servicePool)
        {
        }

        #region Ingredients

        [HttpGet]
        public IActionResult Ingredients([FromRoute(Name = "id")] int medicineId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                int pageSize = 7;
                var responses = _servicePool.Medicines.GetIngredients(medicineId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Medicines.GetIngredients(medicineId).Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public IActionResult IngredientDetails([FromRoute(Name = "id")] int medicineId,
            [FromQuery(Name = "ingredientId")] int ingredientId)
        {
            try
            {
                return View(_servicePool.Medicines.GetIngredientById(medicineId, ingredientId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        #region Add ingredient
        [HttpGet]
        public IActionResult AddIngredient([FromRoute(Name = "id")] int medicineId)
        {
            try
            {
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                return View(_servicePool.Medicines.GetAddIngredientRequest(medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddIngredient([FromRoute(Name = "id")] int medicineId,
            [Bind("MedicineId", "IngredientId", "Ratio")] AddIngredientToMedicineRequest ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Medicines.AddIngredient(ingredient);
                    return RedirectToAction("Ingredients", new { id = medicineId });
                }
                return View(ingredient);
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
        }

        #endregion

        #region Edit ingredient
        [HttpGet]
        public IActionResult EditIngredient([FromRoute(Name = "id")] int medicineId,
            [FromQuery(Name = "ingredientId")] int ingredientId)
        {
            try
            {
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                ViewBag.Ingredient = _servicePool.Ingredients.GetById(ingredientId);
                return View(_servicePool.Medicines.GetEditIngredientRequest(medicineId, ingredientId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditIngredient([FromRoute(Name = "id")] int medicineId,
            [Bind("Id", "MedicineId", "IngredientId", "Ratio")] EditIngredientInMedicineRequest ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Medicines.UpdateIngredient(ingredient);
                    return RedirectToAction("Ingredients", new { id = medicineId });
                }
                return View(ingredient);
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }

        #endregion

        #region Delete ingredient
        [HttpGet]
        public IActionResult DeleteIngredient([FromRoute(Name = "id")] int medicineId,
            [FromQuery(Name = "ingredientId")] int ingredientId)
        {
            try
            {
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                return View(_servicePool.Medicines.GetDeleteIngredientRequest(medicineId, ingredientId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteIngredient([FromRoute(Name = "id")] int medicineId,
            [FromQuery(Name = "ingredientId")] int ingredientId)
        {
            try
            {
                await _servicePool.Medicines.DeleteIngredient(medicineId, ingredientId);
                return RedirectToAction("Ingredients", new { id = medicineId });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }

        #endregion

        #endregion

    }
}
