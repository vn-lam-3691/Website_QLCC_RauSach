using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website_QLCC_RauSach.Models;

public class GioHang
{
    public string MaNvst { get; set; } = null!;

    public string MaMh { get; set; } = null!;

    public int SoLuong { get; set; }

    public virtual NhanVienSt MaNvstNavigation { get; set; } = null!;

    public virtual MatHang MaMhNavigation { get; set; } = null!;
}

