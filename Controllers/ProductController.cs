using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_FirstTry.App.Controllers
{
    public class ProductController : Controller
    {
        public string Index()
        {
            return "default Index";
        }
        public string Detail(string id)
        {
            return $"Details {id}";
        }
        public string Add()
        {
            return "Add";
        }
        [HttpGet]
        public IActionResult TestAction()
        {
            return View();
        }
        [HttpPost]
        public IActionResult TestAction(int id)
        {
            return View("Post");
        }
    }
}
