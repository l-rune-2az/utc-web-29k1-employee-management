using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Models.Entities;
using EmployeeManagement.Services;

namespace EmployeeManagement.Controllers;

public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: /Account/Login — Hiển thị form đăng nhập
    [HttpGet]
    public IActionResult Login()
    {
        // Nếu đã đăng nhập rồi → chuyển thẳng vào trang chủ
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    // POST: /Account/Login — Xử lý khi người dùng bấm nút Đăng nhập
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        // Kiểm tra dữ liệu form hợp lệ không (Required, MaxLength, v.v.)
        if (!ModelState.IsValid) return View(model);

        // Gọi Service kiểm tra username + password
        Users? user = await _userService.AuthenticateAsync(model.Username, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa");
            return View(model);
        }

        // Claims — thông tin được mã hóa vào cookie, dùng xuyên suốt phiên đăng nhập
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            // Lưu EmployeeId để sau này EMPLOYEE chỉ xem được dữ liệu của mình
            new Claim("EmployeeId", user.EmployeeId?.ToString() ?? "")
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

        // ASP.NET mã hóa claims thành cookie và gửi về trình duyệt
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    // POST: /Account/Logout — Đăng xuất
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        // Xóa cookie → người dùng bị đăng xuất
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    // GET: /Account/Forbidden — Trang không có quyền truy cập
    [HttpGet]
    public IActionResult Forbidden()
    {
        return View();
    }
}
