using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
	[Area("NhaCungCap")]
	public class DonHangController : Controller
	{
		private readonly QuanLyRauSachContext dbContext;

		public DonHangController(QuanLyRauSachContext dbContext)
        {
			this.dbContext = dbContext;
		}

		public async Task<IActionResult> Index()
		{
			var tatCaDonHang = await dbContext.DonHangs.ToListAsync();

			var donHangDangChoXacNhan = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Chờ xác nhận").ToListAsync();

			var donHangDangXuLy = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đang xử lý").ToListAsync();

			var donHangDangVanChuyen = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đang vận chuyển").ToListAsync();

            var donHangDaHoanThanh = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Hoàn thành").ToListAsync();

			var donHangDaHuy = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đã hủy").ToListAsync();

			var donHangViewModel = new DonHangViewModel
			{
				TatCaDonHang = tatCaDonHang,
				ChoXacNhanDonHang = donHangDangChoXacNhan,
				DangXuLyDonHang = donHangDangXuLy,
				DangVanChuyenDonHang = donHangDangVanChuyen,
				DaHoanThanhDonHang = donHangDaHoanThanh,
				DaHuyDonHang = donHangDaHuy
			};

			ViewBag.DsNhanVienNcc = donHangViewModel.GetSelectListItems(await dbContext.NhanVienNccs.ToListAsync());

			return View("~/Areas/NhaCungCap/Views/Home/Index.cshtml", donHangViewModel);
		}

		public async Task<IActionResult> ChiTietDonHang(int id)
		{
			var query = from dh in dbContext.DonHangs
						join ctdh in dbContext.ChiTietDonHangs on id equals ctdh.MaDh
						join mh in dbContext.MatHangs on ctdh.MaMh equals mh.MaMh
						join hinhAnh in dbContext.HinhAnhMatHangs on mh.MaMh equals hinhAnh.MaMh
                        where dh.MaDh == id
                        select new ChiTietDonHangViewModel
						{
							MaDh = dh.MaDh,
							Mamh = ctdh.MaMh,
							SoLuongDat = ctdh.SoLuongDat,
							DonGia = ctdh.DonGia,
							TenMatHang = mh.TenMh,
							DonViTinh = mh.DonViTinh,
							MoTa = mh.MoTa,
							MaHamh = hinhAnh.MaHamh,
							DuongDanHinhAnh = hinhAnh.DuongDanHinhAnh
						};

			var result = await query.ToListAsync();

			return View("~/Areas/NhaCungCap/Views/Home/ChiTietDonHang.cshtml" , result);
		}

        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThaiDonHang(int id, string? maNvGh)
        {
            var currentDonHang = await dbContext.DonHangs.FindAsync(id);

			switch (currentDonHang.TrangThaiDh)
			{
				case "Chờ xác nhận":
					currentDonHang.TrangThaiDh = "Đang xử lý";
					break;
				case "Đang xử lý":
					currentDonHang.TrangThaiDh = "Đang vận chuyển";
					break;
			}

			var currentNvncc = HttpContext.Session.GetString("MaNv");
			currentDonHang.MaNvncc = currentNvncc;

			var hoadon = dbContext.HoaDons.Where(hd => hd.MaDh == id).FirstOrDefault();
			if (hoadon.MaNvgh == null)
			{
				hoadon.MaNvgh = maNvGh;
				dbContext.Update(hoadon);
			}

			dbContext.Update(currentDonHang);
			await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    } 
}
