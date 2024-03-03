using eTicket.Models;

namespace eTicket.Data.Services
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetALL();   
        Actor GetById(int id);
        void Add(Actor actor);
        Actor Update(int id, Actor newActor);
        void Delete(int id);

    }
}
