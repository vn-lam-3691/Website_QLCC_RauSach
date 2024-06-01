using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Controllers
{
    public class CartController : Controller
    {
        private readonly QuanLyRauSachContext _context;

        public CartController(QuanLyRauSachContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("MaTaiKhoan") == null)
                return RedirectToAction("Login", "Home");
            else
            {
                var maNV = HttpContext.Session.GetString("MaNv");

                var gioHangs = _context.GioHangs.Include(gh => gh.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(gh => gh.MaNvst.Equals(maNV)).ToList();
                return View(gioHangs);
            }
        }

        [HttpPost]
        public IActionResult AddToCart(String maMH, int slg)
        {
            var maNV = HttpContext.Session.GetString("MaNv");
            if (maNV != null)
            {
                var sp = _context.GioHangs.FirstOrDefault(gh => gh.MaNvst.Equals(maNV) && gh.MaMh.Equals(maMH));
                if (sp != null)
                {
                    sp.SoLuong += slg;
                }
                else
                {
                    var newCartItem = new GioHang
                    {
                        MaNvst = maNV,
                        MaMh = maMH,
                        SoLuong = slg
                    };

                    _context.Add(newCartItem);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
