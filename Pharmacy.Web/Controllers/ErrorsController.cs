using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Web.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InternalServerError()
        {
            return View();
        }


    }
}
