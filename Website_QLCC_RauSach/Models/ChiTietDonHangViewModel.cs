namespace Website_QLCC_RauSach.Models
{
	public class ChiTietDonHangViewModel
	{
        public int? MaDh { get; set; }
        public string? Mamh { get; set; }
        public int? SoLuongDat { get; set; }
        public decimal? DonGia { get; set; }
        public string? TenMatHang { get; set; }
        public string? DonViTinh { get; set; }
        public string? MoTa { get; set; }
        public int? MaHamh { get; set; }
        public string? DuongDanHinhAnh { get; set; }
        public TrangThaiDonHang TrangThaiDh { get; set; }
    }

    public enum TrangThaiDonHang
    {
        DangGiaoHang,
        ChoXacNhan,
        DaGiaoHang,
        DaHuy
    }
}

