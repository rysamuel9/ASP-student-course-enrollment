using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MyASPProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityUser> _roleManager;

        public AdminController(RoleManager<IdentityUser> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
