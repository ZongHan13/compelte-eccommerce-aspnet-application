using eTicket.Data.Base;
using eTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>,IActorService
    {
        private readonly AppDbContext _context;
        public ActorService(AppDbContext context) : base(context) { }
       

        
    }
}
