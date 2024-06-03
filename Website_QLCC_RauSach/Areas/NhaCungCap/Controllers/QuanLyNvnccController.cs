using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
    [Area("NhaCungCap")]
    public class QuanLyNvnccController : Controller
    {
        private readonly QuanLyRauSachContext _context;
        public QuanLyNvnccController(QuanLyRauSachContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var maNCC = HttpContext.Session.GetString("MaNcc");
            var nhanVienNccs = await _context.NhanVienNccs.Include(nv => nv.MaTkNavigation)
                            .Where(nv => nv.MaNcc.Equals(maNCC)).ToListAsync();

            ViewBag.NCC = _context.NhaCungCaps.Where(ncc => ncc.MaNcc.Equals(maNCC)).Select(ncc => ncc.TenNcc).FirstOrDefault();

            return View(nhanVienNccs);
        }


        // GET: QuanLyNvnccController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLyNvnccController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuanLyNvnccController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLyNvnccController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuanLyNvnccController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLyNvnccController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLyNvnccController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
