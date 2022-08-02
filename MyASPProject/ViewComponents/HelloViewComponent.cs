using Microsoft.AspNetCore.Mvc;

namespace MyASPProject.ViewComponents
{
    public class HelloViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = "Hello From View Component";
            return View("Default", model);
        }
    }
}