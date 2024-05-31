using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class NhanVienSt
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public string MaSt { get; set; } = null!;

    public int MaTk { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<GioHang> GioHangs { get; set; } = new List<GioHang>();

    public virtual SieuThi MaStNavigation { get; set; } = null!;

    public virtual TaiKhoan MaTkNavigation { get; set; } = null!;
}
