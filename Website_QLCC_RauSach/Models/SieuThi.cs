using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class SieuThi
{
    public string MaSt { get; set; } = null!;
    public string MaSoThueSt { get; set; } = null!;

    public string TenSt { get; set; } = null!;

    public virtual ICollection<DiaChiSt> DiaChiSts { get; set; } = new List<DiaChiSt>();

    public virtual ICollection<NhanVienSt> NhanVienSts { get; set; } = new List<NhanVienSt>();
}
