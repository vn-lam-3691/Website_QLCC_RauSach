using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Website_QLCC_RauSach.Models;

public partial class QuanLyRauSachContext : DbContext
{
    public QuanLyRauSachContext()
    {
    }

    public QuanLyRauSachContext(DbContextOptions<QuanLyRauSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietCungUng> ChiTietCungUngs { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DiaChiNcc> DiaChiNccs { get; set; }

    public virtual DbSet<DiaChiSt> DiaChiSts { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<HinhAnhMatHang> HinhAnhMatHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<MatHang> MatHangs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVienNcc> NhanVienNccs { get; set; }

    public virtual DbSet<NhanVienSt> NhanVienSts { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<SieuThi> SieuThis { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TknganHang> TknganHangs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=QuanLyRauSach;User ID=sa;Password=10302003;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietCungUng>(entity =>
        {
            entity.HasKey(e => new { e.MaNvncc, e.MaMh }).HasName("PK__ChiTietC__52DFF6B7144011D1");

            entity.ToTable("ChiTietCungUng");

            entity.Property(e => e.MaNvncc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNVNCC");
            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaMH");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.ChiTietCungUngs)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietCun__MaMH__59063A47");

            entity.HasOne(d => d.MaNvnccNavigation).WithMany(p => p.ChiTietCungUngs)
                .HasForeignKey(d => d.MaNvncc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietCu__MaNVN__5812160E");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => new { e.MaDh, e.MaMh }).HasName("PK__ChiTietD__A557DB9C0C42A5DE");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaMH");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaDH__5FB337D6");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDon__MaMH__60A75C0F");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B37508874AC187C1");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenDanhMuc).HasMaxLength(50);
        });

        modelBuilder.Entity<DiaChiNcc>(entity =>
        {
            entity.HasKey(e => e.MaDc).HasName("PK__DiaChiNC__27258664DA516EC9");

            entity.ToTable("DiaChiNCC");

            entity.Property(e => e.MaDc).HasColumnName("MaDC");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.PhuongXa).HasColumnType("ntext");
            entity.Property(e => e.QuanHuyen).HasColumnType("ntext");
            entity.Property(e => e.TenDuong).HasColumnType("ntext");
            entity.Property(e => e.ThanhPho).HasColumnType("ntext");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.DiaChiNccs)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiaChiNCC__MaNCC__4D94879B");
        });

        modelBuilder.Entity<DiaChiSt>(entity =>
        {
            entity.HasKey(e => e.MaDc).HasName("PK__DiaCHiST__27258664FC385D5B");

            entity.ToTable("DiaCHiST");

            entity.Property(e => e.MaDc).HasColumnName("MaDC");
            entity.Property(e => e.MaSt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaST");
            entity.Property(e => e.PhuongXa).HasColumnType("ntext");
            entity.Property(e => e.QuanHuyen).HasColumnType("ntext");
            entity.Property(e => e.TenDuong).HasColumnType("ntext");
            entity.Property(e => e.ThanhPho).HasColumnType("ntext");

            entity.HasOne(d => d.MaStNavigation).WithMany(p => p.DiaChiSts)
                .HasForeignKey(d => d.MaSt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiaCHiST__MaST__4AB81AF0");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDh).HasName("PK__DonHang__2725866112EE7532");

            entity.ToTable("DonHang");

            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.DiaChiNhan).HasColumnType("ntext");
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.MaNvncc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNVNCC");
            entity.Property(e => e.MaNvst)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNVST");
            entity.Property(e => e.TgtaoDon)
                .HasColumnType("datetime")
                .HasColumnName("TGTaoDon");
            entity.Property(e => e.TgthanhToan)
                .HasColumnType("datetime")
                .HasColumnName("TGThanhToan");
            entity.Property(e => e.TrangThaiDh)
                .HasMaxLength(50)
                .HasColumnName("TrangThaiDH");
            entity.Property(e => e.TtdoiTra).HasColumnName("TTDoiTra");

            entity.HasOne(d => d.MaNvnccNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNvncc)
                .HasConstraintName("FK__DonHang__MaNVNCC__5CD6CB2B");

            entity.HasOne(d => d.MaNvstNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaNvst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DonHang__MaNVST__5BE2A6F2");
        });

        modelBuilder.Entity<HinhAnhMatHang>(entity =>
        {
            entity.HasKey(e => e.MaHamh).HasName("PK__HinhAnhM__0281DD55DEB3E326");

            entity.ToTable("HinhAnhMatHang");

            entity.Property(e => e.MaHamh).HasColumnName("MaHAMH");
            entity.Property(e => e.DuongDanHinhAnh).HasColumnType("text");
            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaMH");

            entity.HasOne(d => d.MaMhNavigation).WithMany(p => p.HinhAnhMatHangs)
                .HasForeignKey(d => d.MaMh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HinhAnhMat__MaMH__5535A963");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E0D5FE2B63");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.GhiChu).HasColumnType("ntext");
            entity.Property(e => e.MaDh).HasColumnName("MaDH");
            entity.Property(e => e.MaNvgh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNVGH");
            entity.Property(e => e.NgayTaoHd).HasColumnName("NgayTaoHD");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaDhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaDH__6383C8BA");

            entity.HasOne(d => d.MaNvghNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNvgh)
                .HasConstraintName("FK__HoaDon__MaNVGH__6477ECF3");
        });

        modelBuilder.Entity<MatHang>(entity =>
        {
            entity.HasKey(e => e.MaMh).HasName("PK__MatHang__2725DFD9F2823F91");

            entity.ToTable("MatHang");

            entity.Property(e => e.MaMh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaMH");
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenMh)
                .HasMaxLength(50)
                .HasColumnName("TenMH");
            entity.Property(e => e.TgbaoQuan)
                .HasMaxLength(50)
                .HasColumnName("TGBaoQuan");

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.MatHangs)
                .HasForeignKey(d => d.MaDm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatHang__MaDM__52593CB8");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A185DEB7D594E24");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.AnhGpkd)
                .IsUnicode(false)
                .HasColumnName("AnhGPKD");
            entity.Property(e => e.MaGpkd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MaGPKD");
            entity.Property(e => e.MaSoThue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhanVienNcc>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A6C0824BC");

            entity.ToTable("NhanVienNCC");

            entity.HasIndex(e => e.MaTk, "UQ__NhanVien__27250071776893EE").IsUnique();

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.NhanVienNccs)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVienN__MaNCC__4316F928");

            entity.HasOne(d => d.MaTkNavigation).WithOne(p => p.NhanVienNcc)
                .HasForeignKey<NhanVienNcc>(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVienNC__MaTK__440B1D61");
        });

        modelBuilder.Entity<NhanVienSt>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70ABB0E8357");

            entity.ToTable("NhanVienST");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.MaSt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaST");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");

            entity.HasOne(d => d.MaStNavigation).WithMany(p => p.NhanVienSts)
                .HasForeignKey(d => d.MaSt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVienST__MaST__46E78A0C");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.NhanVienSts)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVienST__MaTK__47DBAE45");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.MaPhanQuyen).HasName("PK__PhanQuye__529AB12B41052F9A");

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.TenPhanQuyen).HasMaxLength(50);
        });

        modelBuilder.Entity<SieuThi>(entity =>
        {
            entity.HasKey(e => e.MaSt).HasName("PK__SieuThi__27250818E61AD435");

            entity.ToTable("SieuThi");

            entity.Property(e => e.MaSt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaST");
            entity.Property(e => e.TenSt)
                .HasMaxLength(50)
                .HasColumnName("TenST");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TaiKhoan__2725007033383BDD");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.AnhDaiDien).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Matkhau)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.MaPhanQuyenNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaPhanQuyen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TaiKhoan__MaPhan__38996AB5");
        });

        modelBuilder.Entity<TknganHang>(entity =>
        {
            entity.HasKey(e => e.MaSoThe).HasName("PK__TKNganHa__7125D40BFDD8EB7D");

            entity.ToTable("TKNganHang");

            entity.Property(e => e.MaSoThe)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MaNh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNH");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.TenChuThe).HasMaxLength(50);
            entity.Property(e => e.TenNh)
                .HasMaxLength(50)
                .HasColumnName("TenNH");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TknganHangs)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TKNganHang__MaTK__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
