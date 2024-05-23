using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class TaiKhoan
{
    public int MaTk { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string? AnhDaiDien { get; set; }

    public int IsActive { get; set; }

    public int MaPhanQuyen { get; set; }

    public virtual PhanQuyen MaPhanQuyenNavigation { get; set; } = null!;

    public virtual NhanVienNcc? NhanVienNcc { get; set; }

    public virtual ICollection<NhanVienSt> NhanVienSts { get; set; } = new List<NhanVienSt>();

    public virtual ICollection<TknganHang> TknganHangs { get; set; } = new List<TknganHang>();
}
