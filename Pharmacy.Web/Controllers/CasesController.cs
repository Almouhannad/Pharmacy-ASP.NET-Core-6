using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Interfaces.IServices;


namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CasesController : BaseController<Case, DisplayCase, CreateCaseRequest, EditCaseRequest>
    {
        public CasesController(IServicePool servicePool) : base(servicePool)
        {
        }

        #region Medicines

        [HttpGet]
        public IActionResult Medicines([FromRoute(Name = "id")] int caseId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                int pageSize = 7;
                var responses = _servicePool.Cases.GetMedicines(caseId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Cases.GetMedicines(caseId).Count() / (double)pageSize);
                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public IActionResult MedicineDetails([FromRoute(Name = "id")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                return View(_servicePool.Cases.GetMedicineById(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
        }

        #region Add medicine
        [HttpGet]
        public IActionResult AddMedicine([FromRoute(Name = "id")] int caseId)
        {
            try
            {
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                return View(_servicePool.Cases.GetAddMedicineRequest(caseId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddMedicine([FromRoute(Name = "id")] int caseId,
            [Bind("CaseId", "MedicineId", "Times")] AddMedicineToCaseRequest medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Cases.AddMedicine(medicine);
                    return RedirectToAction("Medicines", new { id = caseId });
                }
                return View(medicine);

            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }

        #endregion

        #region Edit medicine
        [HttpGet]
        public IActionResult EditMedicine([FromRoute(Name = "id")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                return View(_servicePool.Cases.GetEditMedicineRequest(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditMedicine([FromRoute(Name = "id")] int caseId,
            [Bind("Id", "CaseId", "MedicineId", "Times")] EditMedicineInCaseRequest medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Cases.UpdateMedicine(medicine);
                    return RedirectToAction("Medicines", new { id = caseId });
                }
                return View(medicine);
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }



        }

        #endregion

        #region Delete medicine
        [HttpGet]
        public IActionResult DeleteMedicine([FromRoute(Name = "id")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                return View(_servicePool.Cases.GetDeleteMedicineRequest(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteMedicine([FromRoute(Name = "id")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                await _servicePool.Cases.DeleteMedicine(caseId, medicineId);
                return RedirectToAction("Medicines", new { id = caseId });
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
