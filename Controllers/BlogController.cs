using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_FirstTry.App.Controllers
{
    public class BlogController : Controller
    {
        public string Article(string slug)
        {
            return $"Article: {slug}";
        }
    }
}
