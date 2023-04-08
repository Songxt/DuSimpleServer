using Microsoft.AspNetCore.Mvc;

namespace Du.Core.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
