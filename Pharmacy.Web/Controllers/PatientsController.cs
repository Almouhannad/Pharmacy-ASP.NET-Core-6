using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.General.Users;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Requests.Patients;
using Pharmacy.Core.Entities.ViewModels.Responses.Patients;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IServices.Notifications;
using Pharmacy.Web.Helpers;

namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class PatientsController : BaseController<Patient, DisplayPatient, CreatePatientRequest, EditPatientRequest>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddNewCaseNotificationService _notificationService;
        public PatientsController(IServicePool servicePool,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IAddNewCaseNotificationService notificationService) : base(servicePool)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        #region Basic
        [Authorize(Roles = "admin")]
        public override IActionResult Index([FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            return base.Index(pageNumber);
        }
        [Authorize(Roles = "admin")]
        // No creation allowed (Patient only register)
        public override IActionResult Create()
        {
            return RedirectToAction("NotFound", "Errors");
        }
        [Authorize(Roles = "admin")]

        #region Edit
        [Authorize(Roles = "admin")]
        public override IActionResult Edit(int id)
        {
            return base.Edit(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public override async Task<IActionResult> Edit(EditPatientRequest editRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Patients.Edit(editRequest);
                    var user = _userManager.Users.Where(e => e.PatientId == editRequest.Id).First();
                    if (user != null)
                    {
                        user.Name = editRequest.FirstName + " " + editRequest.LastName;
                        await _userManager.UpdateAsync(user);
                    }
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

        [Authorize(Roles = "admin")]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
        #endregion

        #region Cases

        [HttpGet]
        public async Task<IActionResult> Cases([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }

                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                int pageSize = 7;
                var responses = _servicePool.Patients.GetCases(patientId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Patients.GetCases(patientId).Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        #region Add case

        [HttpGet]
        public async Task<IActionResult> AddCase([FromRoute(Name = "id")] int patientId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                return View(_servicePool.Patients.GetAddCaseRequest(patientId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddCase([FromRoute(Name = "id")] int patientId,
            [Bind("PatientId", "Name")] AddCaseToPatientRequest case_)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Patients.AddCase(case_);

                    #region Logging
                    if (!_httpContextAccessor.HttpContext.User.IsInRole("admin"))
                    {
                        var patient = _servicePool.Patients.GetById(patientId);
                        await _notificationService.Insert(patient.FirstName, patient.LastName,
                            case_.Name, DateTime.Now);
                    }
                    #endregion

                    return Json(
                        new
                        {
                            isValid = true,
                            html = RenderHelper.RenderRazorViewToString(this, "_CasesPartial", _servicePool.Patients.GetCases(patientId).Take(5))
                        });
                }
                return Json(
                    new
                    {
                        isValid = false,
                        html = RenderHelper.RenderRazorViewToString(this, "AddCase", case_)
                    });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
        }

        #endregion

        #region Edit case

        [HttpGet]
        public async Task<IActionResult> EditCase([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                return View(_servicePool.Patients.GetEditCaseRequest(patientId, caseId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditCase([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [Bind("Id", "PatientId", "Name")] EditCaseInPatientRequest case_)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Patients.EditCase(case_);
                    return Json(
                        new
                        {
                            isValid = true,
                            html = RenderHelper.RenderRazorViewToString(this, "_CasesPartial", _servicePool.Patients.GetCases(patientId).Take(5))
                        });
                }

                return Json(
                    new
                    {
                        isValid = false,
                        html = RenderHelper.RenderRazorViewToString(this, "EditCase", case_)
                    });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }

        #endregion

        #region Delete case

        [HttpGet]
        public async Task<IActionResult> DeleteCase([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                return View(_servicePool.Patients.GetDeleteCaseRequest(patientId, caseId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteCase([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId)
        {
            try
            {
                await _servicePool.Patients.DeleteCase(caseId);
                return RedirectToAction(nameof(Cases), new { id = patientId });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
        }

        #endregion

        #endregion

        #region Medicines

        [HttpGet]
        public async Task<IActionResult> CaseMedicines([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                int pageSize = 7;
                var responses = _servicePool.Patients.GetMedicinesInCase(caseId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Patients.GetMedicinesInCase(caseId).Count() / (double)pageSize);
                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public async Task<IActionResult> CaseMedicineDetails([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                return View(_servicePool.Patients.GetMedicineInCaseById(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }


        #region Add medicine

        [HttpGet]
        public async Task<IActionResult> AddCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                return View(_servicePool.Patients.GetAddMedicineRequest(caseId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [Bind("CaseId", "MedicineId", "Times")] AddMedicineToCaseRequest medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Patients.AddMedicineToCase(medicine);

                    return Json(
                        new
                        {
                            isValid = true,
                            html = RenderHelper.RenderRazorViewToString(this, "_CaseMedicinesPartial", _servicePool.Patients.GetMedicinesInCase(caseId).Take(5),
                            _servicePool.Patients.GetById(patientId))
                        });

                }
                return Json(
                    new
                    {
                        isValid = false,
                        html = RenderHelper.RenderRazorViewToString(this, "AddCaseMedicine", _servicePool.Patients.GetAddMedicineRequest(caseId), _servicePool.Patients.GetById(patientId))
                    });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }

        }



        #endregion

        #region Edit medicine
        [HttpGet]
        public async Task<IActionResult> EditCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);

                return View(_servicePool.Patients.GetEditMedicineRequest(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId,
            [Bind("Id", "CaseId", "MedicineId", "Times")] EditMedicineInCaseRequest medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePool.Patients.EditMedicineInCase(medicine);
                    return Json(
                        new
                        {
                            isValid = true,
                            html = RenderHelper.RenderRazorViewToString(this, "_CaseMedicinesPartial", _servicePool.Patients.GetMedicinesInCase(caseId).Take(5),
                            _servicePool.Patients.GetById(patientId))
                        });
                }
                return Json(
                    new
                    {
                        isValid = false,
                        html = RenderHelper.RenderRazorViewToString(this, "EditCaseMedicine", _servicePool.Patients.GetEditMedicineRequest(caseId, medicineId), _servicePool.Patients.GetById(patientId))
                    });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }
        }


        #endregion

        #region Delete medicine

        [HttpGet]
        public async Task<IActionResult> DeleteCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);
                return View(_servicePool.Patients.GetDeleteMedicineRequest(caseId, medicineId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteCaseMedicine([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId)
        {
            try
            {
                await _servicePool.Patients.DeleteMedicineFromCase(caseId, medicineId);
                return RedirectToAction(nameof(CaseMedicines), new { id = patientId, caseId = caseId });
            }
            catch (Exception)
            {
                return RedirectToAction("InternalServerError", "Errors");
            }


        }
        #endregion

        #endregion

        #region Ingredients
        public async Task<IActionResult> MedicineIngredients([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId,
            [FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);
                ViewBag.Medicine = _servicePool.Medicines.GetById(medicineId);

                int pageSize = 7;
                var responses = _servicePool.Patients.GetIngredients(medicineId)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_servicePool.Patients.GetIngredients(medicineId).Count() / (double)pageSize);
                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        public async Task<IActionResult> MedicineIngredientDetails([FromRoute(Name = "id")] int patientId,
            [FromQuery(Name = "caseId")] int caseId,
            [FromQuery(Name = "medicineId")] int medicineId,
            [FromQuery(Name = "ingredientId")] int ingredientId)
        {
            try
            {
                var validUser = await CheckUser(patientId);
                if (!validUser)
                {
                    return RedirectToAction("AccessDenied", "Account");
                }

                ViewBag.Patient = _servicePool.Patients.GetById(patientId);
                ViewBag.Case = _servicePool.Cases.GetById(caseId);

                return View(_servicePool.Patients.GetIngredientById(medicineId, ingredientId));
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }
            
        }
        #endregion

        #region Check user
        public async Task<bool> CheckUser(int patientId)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(ApplicationUserRoles.Admin))
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                return user.PatientId == patientId;
            }
            return true;
        }
        #endregion


    }
}
