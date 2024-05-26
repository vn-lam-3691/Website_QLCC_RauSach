using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class HinhAnhMatHang
{
    public int MaHamh { get; set; }

    public string? DuongDanHinhAnh { get; set; }

    public string MaMh { get; set; } = null!;

    public virtual MatHang MaMhNavigation { get; set; } = null!;
}
