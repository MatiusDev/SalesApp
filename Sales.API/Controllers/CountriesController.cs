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
		public async Task<ActionResult<IEnumerable<Country>>> Get()
		{
			return Ok(await _context.Countries.ToListAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Country>> GetById(int id)
		{
			var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
			return country == null ? NotFound() : Ok(country);
		}

		[HttpPost]
		public async Task<ActionResult<Country>> Add(Country country)
		{
			_context.Add(country);
			await _context.SaveChangesAsync();

			return Ok(country);
		}

		[HttpPut]
		public async Task<ActionResult<Country>> Update(Country country)
		{
			_context.Update(country);
			await _context.SaveChangesAsync();

			return Ok(country);
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
			
			if (country == null) return NotFound();

			_context.Remove(country);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
