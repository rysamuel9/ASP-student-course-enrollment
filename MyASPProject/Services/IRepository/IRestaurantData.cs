using MyASPProject.Models;

namespace MyASPProject.Services.IRepository
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant GetById(int id);
        Restaurant Add(Restaurant restaurant);
    }
}
