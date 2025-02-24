using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Diplom.ViewModels;

namespace Diplom.Controllers;

public class AccountController : Controller
{
	private readonly test_CursachContext _context;

	public AccountController(test_CursachContext context)
	{
		_context = context;
	}

	public IActionResult Login() => View();

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginModel model)
	{
		if (ModelState.IsValid)
		{
			Tutor tutor = await _context.Tutors.FirstOrDefaultAsync(u => u.LogIn == model.Login &&
			                                                             u.PassWord == model.Password
			);
			Student student = await _context.Students
				.FirstOrDefaultAsync(
					u => u.LogIn == model.Login &&
					     u.PassWord == model.Password
				);
			if (tutor != null || student != null)
			{
				await Authenticate(model.Login); // аутентификация

				if (tutor != null)
				{
					if (tutor.TutorRole != "admin") return RedirectToAction("Index", "Tutor");
					else
					{
						return RedirectToAction("Index", "Admin");
					}
				}

				if (student != null) return RedirectToAction("Index", "Students");
			}

			ModelState.AddModelError("", "Некорректные логин и(или) пароль");
		}

		return View(model);
	}

	private async Task Authenticate(string userName)
	{
		// создаем один claim
		var claims = new List<Claim>
		{
			new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
		};
		// создаем объект ClaimsIdentity
		ClaimsIdentity id = new(
			claims,
			"ApplicationCookie",
			ClaimsIdentity.DefaultNameClaimType,
			ClaimsIdentity.DefaultRoleClaimType
		);
		// установка аутентификационных куки
		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(id));
	}

	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Index", "Home");
	}
}