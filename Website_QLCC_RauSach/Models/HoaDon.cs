using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int MaDh { get; set; }

    public string? MaNvgh { get; set; }

    public DateOnly NgayTaoHd { get; set; }

    public decimal TongTien { get; set; }

    public string PhuongThucThanhToan { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual DonHang MaDhNavigation { get; set; } = null!;

    public virtual NhanVienNcc? MaNvghNavigation { get; set; }
}
