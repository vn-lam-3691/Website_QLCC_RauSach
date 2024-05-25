﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;

namespace Website_QLCC_RauSach.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<HomeController> _logger;
        private readonly QuanLyRauSachContext _context;

        public AccountController(ILogger<HomeController> logger, QuanLyRauSachContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var maNV = HttpContext.Session.GetString("MaNv");
            // tat ca don hang
            var donHangsAll = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV)).ToList();
            ViewBag.donHangsAll = donHangsAll;

            // don hang dang giao
            var donHangsDG = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV) && dh.TrangThaiDh.Equals("Đang vận chuyển")).ToList();
            ViewBag.donHangsDG = donHangsDG;

            // don hang cho xac nhan
            var donHangsCXN = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV) && dh.TrangThaiDh.Equals("Chờ xác nhận")).ToList();
            ViewBag.donHangsCXN = donHangsCXN;

            // don hang dang xu ly
            var donHangsDXL = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV) && dh.TrangThaiDh.Equals("Đang xử lý")).ToList();
            ViewBag.donHangsDXL = donHangsDXL;

            // don hang hoan thanh
            var donHangsHT = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV) && dh.TrangThaiDh.Equals("Hoàn thành")).ToList();
            ViewBag.donHangsHT = donHangsHT;

            // don hang da huy
            var donHangsDH = _context.DonHangs.Include(dh => dh.ChiTietDonHangs)
                            .ThenInclude(ct => ct.MaMhNavigation)
                            .ThenInclude(mh => mh.HinhAnhMatHangs)
                            .Where(dh => dh.MaNvst.Equals(maNV) && dh.TrangThaiDh.Equals("Đã hủy")).ToList();
            ViewBag.donHangsDH = donHangsDH;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
