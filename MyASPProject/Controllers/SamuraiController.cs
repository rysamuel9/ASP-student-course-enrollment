using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.Services.IRepository;

namespace MyASPProject.Controllers
{
    public class SamuraiController : Controller
    {
        private readonly ISamurai _samurai;

        public SamuraiController(ISamurai samurai)
        {
            _samurai = samurai;
        }


        public async Task<IActionResult> Index()
        {
            // Ambil data samurai

            var model = await _samurai.GetAll();
            //string strResult = string.Empty;

            //foreach (var result in results)
            //{
            //    strResult += result.Name + "\n";
            //}

            //return Content(strResult);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _samurai.GetById(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Samurai samurai)
        {
            try
            {
                var model = await _samurai.Insert(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show' role='alert'>Berhasil menambahkan data samurai {model.Name} <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show' role='alert'>Gagal menambahkan data, Error: {ex.Message} <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
            }
        }
    }
}
