using Microsoft.AspNetCore.Mvc;
using QLVatTuPhongThiNghiem.Models.ViewModels;
using QLVatTuPhongThiNghiem.Services.Interfaces;

namespace QLVatTuPhongThiNghiem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (success, maNguoiDung, message) = await _authService.LoginAsync(model);

            if (success)
            {
                HttpContext.Session.SetInt32("MaNguoiDung", maNguoiDung);
                HttpContext.Session.SetString("TenDangNhap", model.TenDangNhap);

                TempData["SuccessMessage"] = message;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (maNguoiDung.HasValue)
            {
                await _authService.LogoutAsync(maNguoiDung.Value);
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
