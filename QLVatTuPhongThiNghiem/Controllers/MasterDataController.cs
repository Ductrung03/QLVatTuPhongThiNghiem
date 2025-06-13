using Microsoft.AspNetCore.Mvc;
using QLVatTuPhongThiNghiem.Models.Entities;
using QLVatTuPhongThiNghiem.Services.Interfaces;

namespace QLVatTuPhongThiNghiem.Controllers
{
    public class MasterDataController : Controller
    {
        private readonly IMasterDataService _masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        // Loại thiết bị
        public async Task<IActionResult> Loai()
        {
            // Check if user is logged in
            if (!HttpContext.Session.GetInt32("MaNguoiDung").HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }

            var loaiList = await _masterDataService.GetLoaiAsync();
            return View(loaiList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoai(Loai model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            var (success, message) = await _masterDataService.CreateLoaiAsync(model);
            return Json(new { success, message });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLoai(Loai model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            var (success, message) = await _masterDataService.UpdateLoaiAsync(model);
            return Json(new { success, message });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLoai(int id)
        {
            var (success, message) = await _masterDataService.DeleteLoaiAsync(id);
            return Json(new { success, message });
        }

        // Thương hiệu
        public async Task<IActionResult> ThuongHieu()
        {
            // Check if user is logged in
            if (!HttpContext.Session.GetInt32("MaNguoiDung").HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }

            var thuongHieuList = await _masterDataService.GetThuongHieuAsync();
            return View(thuongHieuList);
        }

        // Phòng máy
        public async Task<IActionResult> PhongMay()
        {
            // Check if user is logged in
            if (!HttpContext.Session.GetInt32("MaNguoiDung").HasValue)
            {
                return RedirectToAction("Login", "Auth");
            }

            var phongMayList = await _masterDataService.GetPhongMayAsync();
            return View(phongMayList);
        }
    }
}