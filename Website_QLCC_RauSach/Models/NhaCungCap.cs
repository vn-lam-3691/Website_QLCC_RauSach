using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class NhaCungCap
{
    public string MaNcc { get; set; } = null!;

    public string TenNcc { get; set; } = null!;

    public string MoTa { get; set; } = null!;

    public string MaSoThue { get; set; } = null!;

    public string MaGpkd { get; set; } = null!;

    public string? AnhGpkd { get; set; }

    public DateOnly NgayDangKy { get; set; }

    public virtual ICollection<DiaChiNcc> DiaChiNccs { get; set; } = new List<DiaChiNcc>();

    public virtual ICollection<NhanVienNcc> NhanVienNccs { get; set; } = new List<NhanVienNcc>();
}
