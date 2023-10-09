using Dapper;
using GestionDeTareas.Models;
using GestionDeTareas.RepositorioTiposTareas;
using GestionDeTareas.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GestionDeTareas.Controllers
{
    public class TiposTareasController : Controller
    {
		private readonly IRepositorioTiposTareas repositorioTiposTareas;
		private readonly IServicioUsuarios servicioUsuarios;

		public TiposTareasController(IRepositorioTiposTareas repositorioTiposTareas,IServicioUsuarios servicioUsuarios)
		{
			this.repositorioTiposTareas = repositorioTiposTareas;
			this.servicioUsuarios = servicioUsuarios;
		}

        public async Task<IActionResult> Index()
        {
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var tiposTareas = await repositorioTiposTareas.Obtener(usuarioId);


            return View(tiposTareas);
        }

		[HttpGet]
        public IActionResult Crear()
        {
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Crear(TipoTarea tipoTarea)
		{

			if (!ModelState.IsValid)
			{
				return View(tipoTarea);
			}

			tipoTarea.UsuarioId = servicioUsuarios.ObtenerIdUsuario();

			var yaExisteTipoTarea =await repositorioTiposTareas.Existe(tipoTarea.Nombre,tipoTarea.UsuarioId);

			if (yaExisteTipoTarea)
			{
				ModelState.AddModelError(nameof(tipoTarea.Nombre),$"El nombre {tipoTarea.Nombre} ya existe.");
				return View(tipoTarea);
			}


			await repositorioTiposTareas.Crear(tipoTarea);

			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> Actualizar(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var tipoTarea = await repositorioTiposTareas.ObtenerPorId(id,usuarioId);

			if(tipoTarea is null)
			{
				return RedirectToAction("NoEncontrado","Home");
			}

			return View(tipoTarea);

		}

		[HttpPost]
		public async Task<IActionResult> Actualizar(TipoTarea tipoTarea)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var yaExisteTipoTarea = await repositorioTiposTareas.ObtenerPorId(tipoTarea.Id,usuarioId);


			if (yaExisteTipoTarea is null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			await repositorioTiposTareas.Actualizar(tipoTarea);


			return RedirectToAction("Index");

		}

		[HttpGet]
		public async Task<IActionResult> Eliminar(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var tipoTarea = await repositorioTiposTareas.ObtenerPorId(id, usuarioId);


			if (tipoTarea is null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			return View(tipoTarea);

		}



		[HttpPost]
		public async Task<IActionResult> EliminarTipoTarea(int id)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var tipoTarea = await repositorioTiposTareas.ObtenerPorId(id, usuarioId);
			if (tipoTarea is null)
			{
				return RedirectToAction("NoEncontrado", "Home");
			}

			await repositorioTiposTareas.Eliminar(id);

			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> VerificarExisteTipoTarea(string nombre)
		{
			var usuarioId = servicioUsuarios.ObtenerIdUsuario();
			var yaExisteTipoTarea = await repositorioTiposTareas.Existe(nombre,usuarioId);

			if (yaExisteTipoTarea)
			{
				return Json($"El nombre {nombre} ya existe");
			}

			return Json(true);

		}





	

	}
}
