using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Share.Entities;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("/api/countries")]
	public class CountriesController : ControllerBase
	{
		private readonly DataContext _context;

		public CountriesController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllAsync()
		{
			return Ok(await _context.Countries.ToListAsync());
		}

		/*[HttpGet]
		public async Task<ActionResult> GetByIdAsync(int id)
		{
			return Ok(await _context.Countries.FindAsync(id));
		}*/

		[HttpPost]
		public async Task<ActionResult> PostAsync(Country country)
		{
			_context.Add(country);
			try
			{
				var res = await _context.SaveChangesAsync();
				Console.Out.WriteLine("Prueba de consola: " + res);
			}
			catch (Exception ex)
			{
				throw new Exception($"Ocurrió un error: {ex.Message}");
			}
			return Ok(country);
		}
	}
}
