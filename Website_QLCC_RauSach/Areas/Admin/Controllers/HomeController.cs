using Microsoft.AspNetCore.Mvc;

namespace Website_QLCC_RauSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
