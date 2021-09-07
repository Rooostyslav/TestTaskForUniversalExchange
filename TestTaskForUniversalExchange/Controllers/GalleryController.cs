using Microsoft.AspNetCore.Mvc;

namespace TestTaskForUniversalExchange.Controllers
{
	public class GalleryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
