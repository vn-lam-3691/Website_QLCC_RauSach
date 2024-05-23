using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class DanhMuc
{
    public int MaDanhMuc { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public virtual ICollection<MatHang> MatHangs { get; set; } = new List<MatHang>();
}
