namespace Website_QLCC_RauSach.Models
{
	public class DonHangViewModel
	{
        public IEnumerable<DonHang> TatCaDonHang { get; set; }
        public IEnumerable<DonHang> ChoXacNhanDonHang { get; set; }
		public IEnumerable<DonHang> DangGiaoDonHang { get; set; }
		public IEnumerable<DonHang> DaGiaoDonHang { get; set; }
		public IEnumerable<DonHang> DaHuyDonHang { get; set; }
	}
}
