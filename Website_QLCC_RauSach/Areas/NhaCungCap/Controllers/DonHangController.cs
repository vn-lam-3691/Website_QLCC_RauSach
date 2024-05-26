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

			var donHangDangGiao = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đang giao hàng").ToListAsync();

			var donHangDaGiao = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đã giao hàng").ToListAsync();

			var donHangDaHuy = await dbContext.DonHangs.Where(dh => dh.TrangThaiDh == "Đã hủy").ToListAsync();

			var donHangViewModel = new DonHangViewModel
			{
				TatCaDonHang = tatCaDonHang,
				ChoXacNhanDonHang = donHangDangChoXacNhan,
				DangGiaoDonHang = donHangDangGiao,
				DaGiaoDonHang = donHangDaGiao,
				DaHuyDonHang = donHangDaHuy
			};

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
        public async Task<IActionResult> CapNhatTrangThaiDonHang(int id, DonHang donHang)
        {
            var currentDonHang = await dbContext.DonHangs.FindAsync(id);

			switch(donHang.TrangThaiDh)
			{
				case "0":
					currentDonHang.TrangThaiDh = "Đang Giao Hàng";
					break;
				case "1":
					currentDonHang.TrangThaiDh = "Chờ xác nhận";
					break;
				case "2":
					currentDonHang.TrangThaiDh = "Đã giao hàng";
					break;
				case "3":
					currentDonHang.TrangThaiDh = "Đã hủy";
					break;
			}

			dbContext.Update(currentDonHang);
			await dbContext.SaveChangesAsync();

            return RedirectToAction("Index"); // Trả về view với model nếu có lỗi
        }
    } 
}
