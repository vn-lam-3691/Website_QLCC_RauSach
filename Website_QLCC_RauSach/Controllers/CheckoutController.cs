using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly QuanLyRauSachContext _context;

		public CheckoutController(QuanLyRauSachContext context)
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

                var items = _context.GioHangs
                    .Include(gh => gh.MaMhNavigation)
                    .Where(gh => gh.MaNvst.Equals(maNV)).ToList();

                ViewBag.NhanVienDatHang = _context.NhanVienSts
                    .Include(nv => nv.MaTkNavigation)
                    .Include(nv => nv.MaStNavigation)
                    .Include(nv => nv.MaStNavigation.DiaChiSts)
                    .FirstOrDefault(nv => nv.MaNv.Equals(maNV));

                ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();

                return View(items);
            }
		}

        [HttpPost]
        public async Task<IActionResult> Payment(string manvst, string DiaChi, string? ghichu, decimal tongtien)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var donhang = new DonHang
                    {
                        MaNvst = manvst,
                        MaNvncc = null,
                        NgayDat = DateOnly.FromDateTime(DateTime.Now),
                        NgayGiaoHang = null,
                        TgtaoDon = DateTime.Now,
                        TgthanhToan = null,
                        DiaChiNhan = DiaChi,
                        GhiChu = ghichu,
                        HinhThucThanhToan = "Thanh toán khi nhận hàng",
                        TrangThaiDh = "Chờ xác nhận",
                        TtdoiTra = 1
                    };
                    _context.Add(donhang);
                    await _context.SaveChangesAsync();


                    var giohang = _context.GioHangs
                        .Include(gh => gh.MaMhNavigation)
                        .Where(gh => gh.MaNvst.Equals(manvst)).ToList();
                    foreach (var item in giohang)
                    {
                        var chitietdonhang = new ChiTietDonHang
                        {
                            MaDh = donhang.MaDh,
                            MaMh = item.MaMh,
                            SoLuongDat = item.SoLuong,
                            DonGia = item.MaMhNavigation.Dongia
                        };
                        _context.Add(chitietdonhang);
                        await _context.SaveChangesAsync();
                    }


                    var hoadon = new HoaDon
                    {
                        MaDh = donhang.MaDh,
                        MaNvgh = null,
                        NgayTaoHd = DateOnly.FromDateTime(DateTime.Now),
                        TongTien = tongtien,
                        PhuongThucThanhToan = "Thanh toán khi nhận hàng",
                        GhiChu = ghichu
                    };
                    _context.Add(hoadon);
                    await _context.SaveChangesAsync();


                    var gioHangDelete = from gh in _context.GioHangs
                                        where gh.MaNvst.Equals(manvst)
                                        select gh;
                    foreach (var item in gioHangDelete)
                    {
                        _context.GioHangs.Remove(item);
                    }
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    TempData["PaymentSuccess"] = "success";
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return RedirectToAction("Index", "Cart");
                }
            }

            return RedirectToAction("Index", "Home");
        }
	}
}
