using MyASPProject.Models;

namespace MyASPProject.Services.IRepository
{
    public interface ISamurai
    {
        Task<IEnumerable<Samurai>> GetAll();
        Task<Samurai> GetById(int id);
        Task<Samurai> Insert(Samurai obj);
    }
}
