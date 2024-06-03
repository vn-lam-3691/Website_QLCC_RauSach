using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
    [Area("NhaCungCap")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyRauSachContext _context;

        public HomeController(ILogger<HomeController> logger, QuanLyRauSachContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MaxGia = _context.MatHangs.Max(sp => sp.Dongia);
            ViewBag.MinGia = _context.MatHangs.Min(sp => sp.Dongia);
            ViewData["DanhMuc"] = _context.DanhMucs.ToList();
            ViewData["MatHangLastest"] = _context.HinhAnhMatHangs.Include(m => m.MaMhNavigation).OrderByDescending(m => m.MaMh).Take(3).ToList();
            var quanLyRauSachContext = _context.HinhAnhMatHangs.Include(m => m.MaMhNavigation);
            ViewBag.Count = quanLyRauSachContext.ToList().Count();

            var maNV = HttpContext.Session.GetString("MaNv");
            ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();

            return View(await quanLyRauSachContext.ToListAsync());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
