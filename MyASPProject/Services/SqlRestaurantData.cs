using MyASPProject.Data;
using MyASPProject.Models;
using MyASPProject.Services.IRepository;

namespace MyASPProject.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private RestaurantDbContext _context;

        // Inject DB COntextnya
        public SqlRestaurantData(RestaurantDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            try
            {
                _context.Restaurants.Add(restaurant);
                _context.SaveChanges();
                return restaurant;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public IEnumerable<Restaurant> GetAll()
        {
            var results = _context.Restaurants.OrderBy(r => r.Name);
            return results;
        }

        public Restaurant GetById(int id)
        {
            var result = _context.Restaurants.FirstOrDefault(r => r.Id == id);
            if (result == null) throw new Exception($"Data resto dengan id {id} tidak ditemukan");
            return result;
        }
    }
}
