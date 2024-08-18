using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Interfaces.IServices.Notifications;

namespace Pharmacy.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class NotificationsController : Controller
    {
        private readonly IAddNewCaseNotificationService _service;

        public NotificationsController(IAddNewCaseNotificationService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult AllAddedCases([FromQuery(Name = "pageNumber")] int pageNumber = 1)
        {
            try
            {
                int pageSize = 7;
                var responses = _service.GetAll()
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.TotalPages = Math.Ceiling(_service.GetAll().Count() / (double)pageSize);

                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

        [HttpGet]
        public async Task<IActionResult> UnreadAddedCases()
        {
            try
            {
                var responses = _service.GetAllUnread();
                await _service.MarkAllAsRead();
                return View(responses);
            }
            catch (Exception)
            {
                return RedirectToAction("NotFound", "Errors");
            }

        }

    }
}
