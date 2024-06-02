using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
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

                ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();

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

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromQuery] string mnvst, [FromForm] List<string> maMh, [FromForm] List<int> soLuongDat)
        {
            for (var index = 0; index < maMh.Count; index++)
            {
                var item = await _context.GioHangs.FirstOrDefaultAsync(gh => gh.MaNvst == mnvst && gh.MaMh == maMh[index]);
                item.SoLuong = soLuongDat[index];
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteCartItem([FromQuery] string mnvst, [FromQuery] string maMh)
        {
            var item = await _context.GioHangs.FirstOrDefaultAsync(gh => gh.MaNvst == mnvst && gh.MaMh == maMh);
            _context.GioHangs.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
