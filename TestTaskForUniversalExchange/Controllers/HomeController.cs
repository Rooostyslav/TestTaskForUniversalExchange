using Microsoft.AspNetCore.Mvc;
namespace TestTaskForUniversalExchange.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
