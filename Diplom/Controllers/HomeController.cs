using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers;

public class HomeController : Controller
{
	public IActionResult Index() => View();

	public IActionResult toHome()
	{
		var userName = User.Identity.Name;

		return View();
	}
}