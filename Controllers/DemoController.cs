using Demo_ASP_FirstTry.App.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP_FirstTry.App.Controllers
{
    public class DemoController : Controller
    {
        
        public string Example01()
        {
            return $"Demo-Exemple01";
        }
        public string Example02()
        {
            return $"Demo-Exemple02";
        }
        public string Example03([FromQuery] PersonRequest person)
        {
            return $"Demo-Exemple03 -> {person.FirstName} {person.LastName}";
        }

    }
}
