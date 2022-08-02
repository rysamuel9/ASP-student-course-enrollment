using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services.IRepository;
using MyASPProject.ViewModels;

namespace MyASPProject.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData _resto;
        public RestaurantsController(IRestaurantData resto)
        {
            _resto = resto;
        }

        public IActionResult Index()
        {
            /*
            var model = new Restaurant
            {
                Id = 1,
                Name = "Steak ABC"
            };

            // Digunakan untuk mengirim data ke controller
            ViewData["username"] = "samueltamba";
            ViewBag.Role = "Administrator";

            return View(model);
            */

            /* 
            var models = _resto.GetAll();
            return View(models);
            */

            // menggunakan view model
            var models = new RestaurantViewModel();
            models.Restaurants = _resto.GetAll();
            models.Username = "samueltamba";

            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            return View(models);
        }

        public IActionResult Details(int id)
        {
            var model = _resto.GetById(id);

            if (model == null)
            {
                // ViewData["kesalahan"] = "Data Restaurant tidak ditemukan";
                TempData["pesan"] = $"<span class='alert alert-danger alert-dismis'>Data Resto dengan id {id} tidak ditemukan</span>";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET VIEW CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE RESTO
        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newResto = new Restaurant()
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine
                };

                _resto.Add(newResto);

                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show' role='alert'>Berhasil menambahkan data {model.Name} <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
