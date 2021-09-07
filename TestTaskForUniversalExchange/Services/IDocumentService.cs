using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestTaskForUniversalExchange.Services
{
	public interface IDocumentService
	{
		Task<FileContentResult> GenerateDocument();
	}
}
