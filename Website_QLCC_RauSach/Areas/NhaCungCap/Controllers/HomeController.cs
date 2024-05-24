using Microsoft.AspNetCore.Mvc;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
    [Area("NhaCungCap")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
