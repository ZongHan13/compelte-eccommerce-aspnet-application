using eTicket.Data; using eTicket.Data.Services; using eTicket.Models; using Microsoft.AspNetCore.Mvc;  namespace eTicket.Controllers {     public class ActorsController : Controller     {         private readonly IActorService _service;          public ActorsController(IActorService service)         {             _service = service;          }          public async Task<IActionResult> Index()         {             var data = await _service.GetALL();             return View(data);         }         //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }         [HttpPost]         public async Task<IActionResult> Create([Bind("ProfilePicture,FullName,Bio")]Actor actor)

										 
		{
            if(!ModelState.IsValid)
            {
                return View(actor);
            }

            _service.Add(actor);
            return RedirectToAction(nameof(Index));
		}     } } 