using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Controllers
{
    public class NVST : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyRauSachContext _context;

        public NVST(ILogger<HomeController> logger, QuanLyRauSachContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult HoaDon()
        {
            return View(_context.HoaDons.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string MaDh, string MaNvgh, string TongTien, string GhiChu)
        {
            HoaDon hd = new HoaDon();
            hd.MaDh = int.Parse(MaDh);
            hd.MaNvgh = null;
            hd.PhuongThucThanhToan = "Thanh toán khi nhận hàng";
            hd.TongTien = int.Parse(TongTien);
            hd.GhiChu = GhiChu;
            _context.HoaDons.Add(hd);
            _context.SaveChanges();
            return RedirectToAction("HoaDon", "NVST");
        }
        public IActionResult Edit(int MaHd)
        {
            ViewBag.MaHd = MaHd;
            return View(_context.HoaDons.FirstOrDefault(t => t.MaHd == MaHd));
        }
        [HttpPost]
        public IActionResult Edit(int MaHd, string MaDh, string MaNvgh, string TongTien, string GhiChu)
        {
            ViewBag.MaHd = MaHd;
            var hd = _context.HoaDons.FirstOrDefault(h => h.MaHd == MaHd);

            if (MaDh != null)
                hd.MaDh = int.Parse(MaDh);
            hd.MaNvgh = MaNvgh;
            hd.PhuongThucThanhToan = "Thanh toán khi nhận hàng";
            hd.TongTien = int.Parse(TongTien);
            hd.GhiChu = GhiChu;
            _context.SaveChanges();

            return RedirectToAction("HoaDon", "NVST");
        }
        public IActionResult Delete(int MaHd)
        {
            var hd = _context.HoaDons.Find(MaHd); // Tìm đối tượng HoaDon cần xóa dựa trên id hoặc bất kỳ điều kiện nào khác
            if (hd != null)
            {
                _context.HoaDons.Remove(hd); // Xóa đối tượng HoaDon khỏi context
                _context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
            }
            return RedirectToAction("HoaDon", "NVST");
        }
    }
}
