using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class TknganHang
{
    public string MaSoThe { get; set; } = null!;

    public string MaNh { get; set; } = null!;

    public string TenNh { get; set; } = null!;

    public string TenChuThe { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly NgayHetHan { get; set; }

    public int MaTk { get; set; }

    public virtual TaiKhoan MaTkNavigation { get; set; } = null!;
}
