﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website_QLCC_RauSach.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Website_QLCC_RauSach.Controllers
{
    public class MatHangsController : Controller
    {
        private readonly QuanLyRauSachContext _context;

        public MatHangsController(QuanLyRauSachContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string keywords, int maDm)
        {
            ViewBag.MaxGia = _context.MatHangs.Max(sp => sp.Dongia);
            ViewBag.MinGia = _context.MatHangs.Min(sp => sp.Dongia);
            ViewData["DanhMuc"] = _context.DanhMucs.ToList();
            ViewData["MatHangLastest"] = _context.HinhAnhMatHangs.Include(m => m.MaMhNavigation).OrderByDescending(m => m.MaMh).Skip(1).Take(3).ToList();

            var quanLyRauSachContext = _context.HinhAnhMatHangs
                .Include(m => m.MaMhNavigation)
                .Include(m => m.MaMhNavigation.MaDmNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keywords))
            {
                quanLyRauSachContext = quanLyRauSachContext
                    .Where(m => m.MaMhNavigation.TenMh.Contains(keywords));
            }

            if (maDm > 0)
            {
                quanLyRauSachContext = quanLyRauSachContext
                    .Where(m => m.MaMhNavigation.MaDm == maDm);
            }

            ViewBag.Count = quanLyRauSachContext.ToList().Count();

            var maNV = HttpContext.Session.GetString("MaNv");
            ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();

            return View(await quanLyRauSachContext.ToListAsync());
        }

        // GET: MatHangs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var maNV = HttpContext.Session.GetString("MaNv");
            ViewBag.CartCount = _context.GioHangs.Where(gh => gh.MaNvst.Equals(maNV)).Count();

            if (id == null)
            {
                return NotFound();
            }

            var matHang = await _context.HinhAnhMatHangs
                .Include(m => m.MaMhNavigation)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (matHang == null)
            {
                return NotFound();
            }

            ViewData["MatHangRelated"] = _context.HinhAnhMatHangs.Include(m => m.MaMhNavigation).OrderByDescending(m => m.MaMh).Take(4).ToList();

            var query = from mh in _context.MatHangs
                        join ctu in _context.ChiTietCungUngs on mh.MaMh equals ctu.MaMh
                        join nv in _context.NhanVienNccs on ctu.MaNvncc equals nv.MaNv
                        join ncc in _context.NhaCungCaps on nv.MaNcc equals ncc.MaNcc
                        where mh.MaMh == id
                        select new
                        {
                            MaNcc = ncc.MaNcc,
                            TenNcc = ncc.TenNcc
                        };
            var ttNcc = query.FirstOrDefault();
            ViewData["TTNcc"] = ttNcc;

            return View(matHang);
        }

        // GET: MatHangs/Create
        public IActionResult Create()
        {
            ViewData["MaDm"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc");
            return View();
        }

        // POST: MatHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMh,MaDm,TenMh,MoTa,DonViTinh,Dongia,SoLuong,TgbaoQuan,TinhTrang")] MatHang matHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", matHang.MaDm);
            return View(matHang);
        }

        // GET: MatHangs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matHang = await _context.MatHangs.FindAsync(id);
            if (matHang == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", matHang.MaDm);
            return View(matHang);
        }

        // POST: MatHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaMh,MaDm,TenMh,MoTa,DonViTinh,Dongia,SoLuong,TgbaoQuan,TinhTrang")] MatHang matHang)
        {
            if (id != matHang.MaMh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatHangExists(matHang.MaMh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDm"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", matHang.MaDm);
            return View(matHang);
        }

        // GET: MatHangs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matHang = await _context.MatHangs
                .Include(m => m.MaDmNavigation)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (matHang == null)
            {
                return NotFound();
            }

            return View(matHang);
        }

        // POST: MatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var matHang = await _context.MatHangs.FindAsync(id);
            if (matHang != null)
            {
                _context.MatHangs.Remove(matHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatHangExists(string id)
        {
            return _context.MatHangs.Any(e => e.MaMh == id);
        }
    }
}
