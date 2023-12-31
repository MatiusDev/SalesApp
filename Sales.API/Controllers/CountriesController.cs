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
		public async Task<ActionResult<IEnumerable<Country>>> GetAllAsync()
		{
			return Ok(await _context.Countries.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Country>> GetByIdAsync(int id)
		{
			return Ok(await _context.Countries.FindAsync(id));
		}

		[HttpPost]
		public async Task<ActionResult<Country>> PostAsync(Country country)
		{
			_context.Add(country);
			await _context.SaveChangesAsync();

			return Ok(country);
		}
	}
}
