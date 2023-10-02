using GestionDeTareas.Models;
using GestionDeTareas.RepositorioTiposTareas;
using GestionDeTareas.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionDeTareas.Controllers
{
    public class TareasController : Controller
    {
		private readonly IRepositorioTiposTareas repositorioTiposTareas;
		private readonly IServicioUsuarios servicioUsuarios;
		private readonly IRepositorioTareas repositorioTareas;

		public TareasController(IRepositorioTiposTareas repositorioTiposTareas, IServicioUsuarios servicioUsuarios,IRepositorioTareas repositorioTareas)
		{
			this.repositorioTiposTareas = repositorioTiposTareas;
			this.servicioUsuarios = servicioUsuarios;
			this.repositorioTareas = repositorioTareas;
		}

		public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
		public async Task<IActionResult> Crear()
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();

			var modelo = new TareaCreacionViewModel();

			modelo.TiposTareas = await ObtenerTiposTareas(usuarioId);

			return View(modelo);
		}

		[HttpPost]
		public async Task<IActionResult> Crear(TareaCreacionViewModel tarea)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var tipoTarea = await repositorioTiposTareas.ObtenerPorId(tarea.TipoTareaId,usuarioId);

			if(tipoTarea is null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			if (!ModelState.IsValid)
			{
				tarea.TiposTareas = await ObtenerTiposTareas(usuarioId);
				return View(tarea);
			}

			await repositorioTareas.Crear(tarea);

			return RedirectToAction("Index");

		}

		//Se obtienen los tipos de tareas pero en forma de SelectListItem
		private async Task<IEnumerable<SelectListItem>> ObtenerTiposTareas(int usuarioId)
		{
			var tiposTareas = await repositorioTiposTareas.Obtener(usuarioId);

			return tiposTareas.Select(p => new SelectListItem(p.Nombre, p.Id.ToString()));


		}




	}
}
