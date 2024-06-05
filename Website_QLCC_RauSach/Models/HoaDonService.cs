using System.Net.WebSockets;

namespace Website_QLCC_RauSach.Models
{
	public class HoaDonService
	{
		private readonly QuanLyRauSachContext _context;

		public HoaDonService(QuanLyRauSachContext context)
		{
			_context = context;
		}

		public Orders GetDonHangInfo(int maDh)
		{
			var ttOrder = new Orders();

			var q1 = from dh in _context.DonHangs
					 join hd in _context.HoaDons on dh.MaDh equals hd.MaDh
					 where dh.MaDh == maDh
					 select new Order1
					 {
						 MaDh = hd.MaDh,
						 MaHd = hd.MaHd,
						 NgayDat = dh.NgayDat,
						 Pttt = hd.PhuongThucThanhToan,
						 Ghichu = dh.GhiChu,
						 Tongtien = hd.TongTien,
						 Diachinhan = dh.DiaChiNhan
					 };
			ttOrder.order1 = q1.FirstOrDefault();

			var q2 = from dh in _context.DonHangs
					 join nvncc in _context.NhanVienNccs on dh.MaNvncc equals nvncc.MaNv
					 join nvst in _context.NhanVienSts on dh.MaNvst equals nvst.MaNv
					 join st in _context.SieuThis on nvst.MaSt equals st.MaSt
					 join ncc in _context.NhaCungCaps on nvncc.MaNcc equals ncc.MaNcc
					 where dh.MaDh == maDh
					 select new Order2
					 {
						 TenNvst = nvst.TenNv,
						 TenNvncc = nvncc.TenNv,
						 TenSt = st.TenSt,
						 TenNcc = ncc.TenNcc,
						 MaSoThue = ncc.MaSoThue,
						 MaSoThueSt = st.MaSoThueSt
					 };
			ttOrder.order2 = q2.FirstOrDefault();

			var q3 = from hd in _context.HoaDons
					 join nvncc in _context.NhanVienNccs on hd.MaNvgh equals nvncc.MaNv
					 where hd.MaDh == maDh
					 select new
					 {
						 TenNvGh = nvncc.TenNv
					 };
			ttOrder.NhanVienGiaoHang = q3.FirstOrDefault().TenNvGh;

			var q4 = from dh in _context.DonHangs
					 join ctdh in _context.ChiTietDonHangs on dh.MaDh equals ctdh.MaDh
					 join mh in _context.MatHangs on ctdh.MaMh equals mh.MaMh
					 where dh.MaDh == maDh
					 select new DonHangDetailsViewModel
					 {
						 MaMh = mh.MaMh,
						 TenMh = mh.TenMh,
						 SoLuongDat = ctdh.SoLuongDat,
						 DonGia = ctdh.DonGia
					 };
			ttOrder.ChiTietDonHangs = q4.ToList();

			return ttOrder;
		}
	}

	public class Order1
	{
		public int MaDh { get; set; }
		public int MaHd { get; set; }
		public DateOnly NgayDat { get; set; }
		public string Pttt { get; set; }
		public string Ghichu { get; set; }
		public decimal Tongtien { get; set; }
		public string Diachinhan { get; set; }
	}

	public class Order2
	{
		public string TenNvst { get; set; }
		public string TenNvncc { get; set; }
		public string TenSt { get; set; }
		public string TenNcc { get; set; }
		public string MaSoThueSt { get; set; }
		public string MaSoThue { get; set; }
	}

	public class Orders
	{
		public Order1 order1 { get; set; }
		public Order2 order2 { get; set; }
		public string NhanVienGiaoHang { get; set; }
		public List<DonHangDetailsViewModel> ChiTietDonHangs { get; set; }
	}

	public class DonHangDetailsViewModel
	{
		public string MaMh { get; set; }
		public string TenMh { get; set; }
		public int SoLuongDat { get; set; }
		public decimal DonGia { get; set; }
	}
}
