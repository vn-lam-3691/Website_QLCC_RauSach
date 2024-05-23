using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class PhanQuyen
{
    public int MaPhanQuyen { get; set; }

    public string TenPhanQuyen { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
