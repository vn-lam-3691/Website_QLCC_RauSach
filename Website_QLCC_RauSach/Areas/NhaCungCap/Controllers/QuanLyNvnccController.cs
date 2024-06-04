using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Areas.NhaCungCap.Controllers
{
    [Area("NhaCungCap")]
    public class QuanLyNvnccController : Controller
    {
        private readonly QuanLyRauSachContext _context;
        public QuanLyNvnccController(QuanLyRauSachContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var maNCC = HttpContext.Session.GetString("MaNcc");
            var nhanVienNccs = await _context.NhanVienNccs.Include(nv => nv.MaTkNavigation)
                            .Where(nv => nv.MaNcc.Equals(maNCC)).ToListAsync();

            ViewBag.NCC = _context.NhaCungCaps.Where(ncc => ncc.MaNcc.Equals(maNCC)).Select(ncc => ncc.TenNcc).FirstOrDefault();

            return View(nhanVienNccs);
        }

        public async Task<IActionResult> Details(string id)
        {
            var nhanVienNcc = _context.NhanVienNccs.Include(nv => nv.MaTkNavigation)
                            .Where(nv => nv.MaNv == id).FirstOrDefault();
            return View(nhanVienNcc);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NhanVienNcc nv, string username, string password, string sdt, string email)
        {
            var usernameExist = _context.TaiKhoans.FirstOrDefault(tk => tk.TenDangNhap.Equals(username));
            if (usernameExist == null)
            {
                var newTaiKhoan = new TaiKhoan
                {
                    TenDangNhap = username,
                    Matkhau = password,
                    Email = email,
                    Sdt = sdt,
                    IsActive = 1,
                    MaPhanQuyen = 3
                };
                _context.Add(newTaiKhoan);
                _context.SaveChanges();

                var maNCC = HttpContext.Session.GetString("MaNcc");
                var currentNCC = _context.NhaCungCaps.Where(ncc => ncc.MaNcc.Equals(maNCC)).Select(ncc => ncc.MaNcc).FirstOrDefault();

                if (currentNCC != null)
                {
                    var newNV = new NhanVienNcc
                    {
                        MaNv = nv.MaNv,
                        TenNv = nv.TenNv,
                        ChucVu = nv.ChucVu,
                        MaNcc = currentNCC,
                        MaTk = newTaiKhoan.MaTk
                    };
                    _context.Add(newNV);
                    _context.SaveChanges();
                }
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!!!";
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var nvncc = _context.NhanVienNccs.Where(nv => nv.MaNv.Equals(id)).FirstOrDefault();
            if (nvncc != null) {
                _context.NhanVienNccs.Remove(nvncc);
                _context.SaveChanges();

                var taikhoan = _context.TaiKhoans.Where(tk => tk.MaTk.Equals(nvncc.MaTk)).FirstOrDefault();
                if (taikhoan != null)
                {
                    _context.TaiKhoans.Remove(taikhoan);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
