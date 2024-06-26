﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Website_QLCC_RauSach.Models
{
	public class DonHangViewModel
	{
        public IEnumerable<DonHang> TatCaDonHang { get; set; }
        public IEnumerable<DonHang> ChoXacNhanDonHang { get; set; }
        public IEnumerable<DonHang> DangXuLyDonHang { get; set; }
        public IEnumerable<DonHang> DangVanChuyenDonHang { get; set; }
		public IEnumerable<DonHang> DaHoanThanhDonHang { get; set; }
		public IEnumerable<DonHang> DaHuyDonHang { get; set; }

		public SelectList GetSelectListItems(List<NhanVienNcc> nhanVienNccs)
		{
			return new SelectList(nhanVienNccs, "MaNv", "TenNv");
		}
	}
}