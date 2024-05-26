using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class NhanVienNcc
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public string MaNcc { get; set; } = null!;

    public int MaTk { get; set; }

    public virtual ICollection<ChiTietCungUng> ChiTietCungUngs { get; set; } = new List<ChiTietCungUng>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;

    public virtual TaiKhoan MaTkNavigation { get; set; } = null!;
}
