using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

		//GET: cinemas/create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
		{
			if (!ModelState.IsValid) return View(cinema);

			await _service.AddAsync(cinema);
			return RedirectToAction(nameof(Index));
		}

		//GET: Cinemas/Details/1
		public async Task<IActionResult> Details(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		//GET: Cinemas/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var cinemasDetails = await _service.GetByIdAsync(id);
			if (cinemasDetails == null) return View("NotFound");
			return View(cinemasDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Logo,Name,Description")] Cinema cinema)
		{
			if (!ModelState.IsValid) return View(cinema);

				await _service.UpdateAsync(id, cinema);
				return RedirectToAction(nameof(Index));
			
			
		}

		//GET: Cinemas/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var cinemasDetails = await _service.GetByIdAsync(id);
			if (cinemasDetails == null) return View("NotFound");
			return View(cinemasDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id, Cinema cinema)
		{
			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
