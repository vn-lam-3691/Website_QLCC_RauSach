using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class DonHang
{
    public int MaDh { get; set; }

    public string MaNvst { get; set; } = null!;

    public string? MaNvncc { get; set; }

    public DateOnly NgayDat { get; set; }

    public DateOnly? NgayGiaoHang { get; set; }

    public DateTime TgtaoDon { get; set; }

    public DateTime? TgthanhToan { get; set; }

    public string DiaChiNhan { get; set; } = null!;

    public string? GhiChu { get; set; }

    public string HinhThucThanhToan { get; set; } = null!;

    public string TrangThaiDh { get; set; } = null!;

    public int TtdoiTra { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual NhanVienNcc? MaNvnccNavigation { get; set; }

    public virtual NhanVienSt MaNvstNavigation { get; set; } = null!;
}
