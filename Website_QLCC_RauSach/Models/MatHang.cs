using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class MatHang
{
    public string? MaMh { get; set; } = null!;

    public int MaDm { get; set; }

    public string TenMh { get; set; } = null!;

    public string? MoTa { get; set; }

    public string DonViTinh { get; set; } = null!;

    public decimal Dongia { get; set; }

    public int SoLuong { get; set; }

    public string TgbaoQuan { get; set; } = null!;

    public int TinhTrang { get; set; }
    public virtual ICollection<ChiTietCungUng> ChiTietCungUngs { get; set; } = new List<ChiTietCungUng>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<HinhAnhMatHang> HinhAnhMatHangs { get; set; } = new List<HinhAnhMatHang>();

    public virtual DanhMuc MaDmNavigation { get; set; } = null!;
   
}
