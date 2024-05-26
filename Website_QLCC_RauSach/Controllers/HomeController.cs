﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyRauSachContext _context;

        public HomeController(ILogger<HomeController> logger, QuanLyRauSachContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập thông tin";
                return View();
            }

            var user = _context.TaiKhoans
                .FirstOrDefault(x => x.TenDangNhap == username && x.Matkhau == password);

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }
            else
            {
                HttpContext.Session.SetInt32("MaTaiKhoan", user.MaTk);
                HttpContext.Session.SetInt32("MaPhanQuyen", user.MaPhanQuyen);
                switch (user.MaPhanQuyen)
                {
                    case 1:
                        var tenadmin = _context.TaiKhoans.FirstOrDefault(x => x.MaPhanQuyen == user.MaPhanQuyen).TenDangNhap;
                        HttpContext.Session.SetString("TenNv", tenadmin);
                        return Redirect("/Admin/Home/Index");
                    case 2:
                        var tennvst = _context.NhanVienSts.FirstOrDefault(x => x.MaTk == user.MaTk).TenNv;
                        HttpContext.Session.SetString("TenNv", tennvst);


                        var maNVST = _context.NhanVienSts.FirstOrDefault(x => x.MaTk == user.MaTk).MaNv;
                        HttpContext.Session.SetString("MaNv", maNVST);
                        return RedirectToAction("Index", "Home");
                    case 3:
                        var tennvncc = _context.NhanVienNccs.FirstOrDefault(x => x.MaTk == user.MaTk).TenNv;
                        HttpContext.Session.SetString("TenNv", tennvncc);
                        return Redirect("/NhaCungCap/DonHang");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}
