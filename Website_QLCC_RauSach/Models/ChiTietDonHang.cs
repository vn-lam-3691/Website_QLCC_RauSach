using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class ChiTietDonHang
{
    public int MaDh { get; set; }

    public string MaMh { get; set; } = null!;

    public int SoLuongDat { get; set; }

    public decimal DonGia { get; set; }

    public virtual DonHang MaDhNavigation { get; set; } = null!;

    public virtual MatHang MaMhNavigation { get; set; } = null!;
}
