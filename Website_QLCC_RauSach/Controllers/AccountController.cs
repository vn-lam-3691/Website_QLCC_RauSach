using Microsoft.AspNetCore.Mvc;
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

            ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();
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

        [HttpPost]
        public IActionResult NhapHang(int maDH)
        {
            var donhang = _context.DonHangs.FirstOrDefault(dh => dh.MaDh == maDH);
            if(donhang != null)
            {
                donhang.TrangThaiDh = "Hoàn thành";
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public IActionResult HuyDon(int maDHHuy, String GhiChu)
        {
            Console.WriteLine(maDHHuy);
            var donhang = _context.DonHangs.FirstOrDefault(dh => dh.MaDh == maDHHuy);
            if (donhang != null)
            {
                donhang.TrangThaiDh = "Đã hủy";
                donhang.GhiChu = GhiChu;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Account");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult StaffManagement()
        {
            var maNV = HttpContext.Session.GetString("MaNv");

            var currentSt = _context.NhanVienSts
                .Include(st => st.MaStNavigation)
                .Where(st => st.MaNv == maNV)
                .Select(st => st.MaStNavigation.MaSt)
                .FirstOrDefault();

            var nhanviens = _context.NhanVienSts
                .Include(n => n.MaTkNavigation)
                .Where(n => n.MaSt == currentSt)
                .ToList();

            return View(nhanviens);
        }

        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(NhanVienSt nv, string sdt, string email, string username, string password)
        {
            var maNV = HttpContext.Session.GetString("MaNv");
            var currentSt = _context.NhanVienSts
                .Include(st => st.MaStNavigation)
                .Where(st => st.MaNv == maNV)
                .Select(st => st.MaStNavigation.MaSt)
                .FirstOrDefault();


            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var taikhoan = new TaiKhoan
                    {
                        TenDangNhap = username,
                        Matkhau = password,
                        Email = email,
                        Sdt = sdt,
                        AnhDaiDien = null,
                        IsActive = 1,
                        MaPhanQuyen = 2
                    };
                    _context.Add(taikhoan);
                    await _context.SaveChangesAsync();


                    var nhanvienst = new NhanVienSt
                    {
                        MaNv = nv.MaNv,
                        TenNv = nv.TenNv,
                        ChucVu = nv.ChucVu,
                        MaSt = currentSt,
                        MaTk = taikhoan.MaTk
                    };
                    _context.Add(nhanvienst);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return RedirectToAction("AddStaff", "Account");
                }
            }


            return RedirectToAction("StaffManagement", "Account");
        }

        public async Task<IActionResult> StaffDetail(string id)
        {
            var nhanvienst = _context.NhanVienSts
                .Include(n => n.MaTkNavigation)
                .Where(n => n.MaNv == id)
                .FirstOrDefault();

            return View(nhanvienst);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var nhanvienst = _context.NhanVienSts
                .Where(n => n.MaNv == id)
                .FirstOrDefault();

            if (nhanvienst != null)
            {
                _context.NhanVienSts.Remove(nhanvienst);
                await _context.SaveChangesAsync();
            }


            var taikhoannv = _context.TaiKhoans
                .Where(tk => tk.MaTk == nhanvienst.MaTk)
                .FirstOrDefault();

            if (taikhoannv != null)
            {
                _context.TaiKhoans.Remove(taikhoannv);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("StaffManagement", "Account");
        }
    }
}
