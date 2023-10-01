using Microsoft.AspNetCore.Mvc;

namespace GestionDeTareas.Controllers
{
    public class TareasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
