using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetALLAsync();
            return View(allProducers);
        }
        //GET: producers/details/1
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
        
        //GET: producers/create
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicture,FullName,Bio")] Producer producer)
        {
            if(!ModelState.IsValid) return View(producer);
            
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
		}

        //GET: producers/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePicture,FullName,Bio")] Producer producer)
		{
			if (!ModelState.IsValid) return View(producer);

            if (id == producer.Id) 
            { 
                await _service.UpdateAsync(id, producer);
				return RedirectToAction(nameof(Index));
			}
			return View(producer);
		}

        //GET producer/delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");
			return View(producerDetails);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var producerDetails = await _service.GetByIdAsync(id);
			if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
		}
	}
}
