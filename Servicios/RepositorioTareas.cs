using Dapper;
using GestionDeTareas.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionDeTareas.Servicios
{
	public interface IRepositorioTareas
	{
		Task Crear(Tarea tarea);
		Task<IEnumerable<Tarea>> Obtener(int usuarioId);
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


			var id = await connection.QuerySingleAsync<int>("Tareas_Insertar", new {tarea.Nombre,tarea.TipoTareaId,tarea.Descripcion,tarea.FechaCreacion,tarea.FechaLimite,tarea.Completada}, commandType: CommandType.StoredProcedure);


			tarea.Id = id;


		}

		public async Task<IEnumerable<Tarea>> Obtener(int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);


			return await connection.QueryAsync<Tarea>("Tareas_Obtener", new {usuarioId}, commandType: CommandType.StoredProcedure);
		}

		//public async Task Actualizar()
		//{

		//}




	}
}
