using Dapper;
using GestionDeTareas.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionDeTareas.Servicios
{
	public interface IRepositorioTareas
	{
		Task Actualizar(Tarea tarea);
		Task Crear(Tarea tarea);
		Task Eliminar(int id);
		Task<IEnumerable<Tarea>> Obtener(int usuarioId);
		Task<Tarea> ObtenerPorId(int id, int usuarioId);
	}

	public class RepositorioTareas:IRepositorioTareas
	{

		private readonly string connectionString;

		public RepositorioTareas(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task Crear(Tarea tarea) 
		{
			using var connection = new SqlConnection(connectionString);
			var parameters = new
			{
				tarea.Nombre,
				tarea.TipoTareaId,
				tarea.Descripcion,
				tarea.FechaCreacion,
				tarea.FechaLimite,
				tarea.Completada,
				tarea.UsuarioId // Asegúrate de que el nombre del parámetro coincida con el de la función almacenada
			};

			var id = await connection.QuerySingleAsync<int>("Tareas_Insertar",parameters, commandType: CommandType.StoredProcedure);


			tarea.Id = id;


		}


		public async Task<IEnumerable<Tarea>> Obtener(int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);


			return await connection.QueryAsync<Tarea>("Tareas_Obtener", new {usuarioId}, commandType: CommandType.StoredProcedure);
		}

		public async Task<Tarea> ObtenerPorId(int id,int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);

			return await connection.QueryFirstOrDefaultAsync<Tarea>("Tareas_ObtenerPorId", new {id,usuarioId}, commandType: CommandType.StoredProcedure);

		}


		public async Task Actualizar(Tarea tarea)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				tarea.Id, 
				tarea.Nombre, 
				tarea.TipoTareaId,
				tarea.Descripcion,
				tarea.FechaLimite
			};

			await connection.ExecuteAsync("Tareas_Actualizar",parameters, commandType: CommandType.StoredProcedure);

		}


		public async Task Eliminar(int id)
		{
			using var connection = new SqlConnection(connectionString);

			await connection.ExecuteAsync("Tareas_Eliminar", new {id}, commandType: CommandType.StoredProcedure); 

		}

	




	}
}
