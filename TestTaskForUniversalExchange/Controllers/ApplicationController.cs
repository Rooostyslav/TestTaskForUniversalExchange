using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestTaskForUniversalExchange.EF;
using TestTaskForUniversalExchange.Models;
using TestTaskForUniversalExchange.Services;

namespace TestTaskForUniversalExchange.Controllers
{
	public class ApplicationController : Controller
	{
        private readonly UniversalExchangeContext _context;
        private readonly IDocumentService _documentService;

        public ApplicationController(UniversalExchangeContext context, 
            IDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Application application)
        {
            if (ModelState.IsValid)
            {
                await _context.Applications.AddAsync(application);
                await _context.SaveChangesAsync();

                ViewBag.Success = true;

                return View();
            }
            else
			{
                ViewBag.Success = false;
                return View(application);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplicationsInDocument()
        {
            return await _documentService.GenerateDocument();
        }
    }
}
