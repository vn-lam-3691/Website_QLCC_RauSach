using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class ChiTietCungUng
{
    public string MaNvncc { get; set; } = null!;

    public string MaMh { get; set; } = null!;

    public int SoLuongCungUng { get; set; }

    public virtual MatHang MaMhNavigation { get; set; } = null!;

    public virtual NhanVienNcc MaNvnccNavigation { get; set; } = null!;
}
