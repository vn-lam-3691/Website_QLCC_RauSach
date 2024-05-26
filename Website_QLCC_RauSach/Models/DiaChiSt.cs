using System;
using System.Collections.Generic;

namespace Website_QLCC_RauSach.Models;

public partial class DiaChiSt
{
    public int MaDc { get; set; }

    public string TenDuong { get; set; } = null!;

    public string PhuongXa { get; set; } = null!;

    public string QuanHuyen { get; set; } = null!;

    public string ThanhPho { get; set; } = null!;

    public string MaSt { get; set; } = null!;

    public virtual SieuThi MaStNavigation { get; set; } = null!;
}
