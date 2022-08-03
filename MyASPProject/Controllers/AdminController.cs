using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using MyASPProject.ViewModels;

namespace MyASPProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<CustomIdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET VIEW
        public IActionResult CreateRole()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole myRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(myRole);

                if (result.Succeeded)
                {
                    ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show' role='alert'>Berhasil menambahkan data {model.RoleName} <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // Menambilkan role
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["pesan"] = $"<div class='alert alert-warning alert-dismissible fade show' role='alert'>Role dengan id {role.Id} tidak ditemukan <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            //foreach (var user in _userManager.Users)
            //{
            //    if (await _userManager.IsInRoleAsync(user, user.FullName))
            //    {
            //        model.Users.Add(user.FullName);
            //    }
            //}

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show' role='alert'>Berhasil menambahkan data {model.RoleName} <button type='button' class='btn - close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            try
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
                return RedirectToAction("ListRoles");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }

            return View(model);
        }
    }
}