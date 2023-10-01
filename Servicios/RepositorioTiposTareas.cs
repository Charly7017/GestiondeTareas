using Dapper;
using GestionDeTareas.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionDeTareas.RepositorioTiposTareas
{
	public interface IRepositorioTiposTareas
	{
		Task Actualizar(TipoTarea tipoTarea);
		Task Crear(TipoTarea tipoTarea);
		Task Eliminar(int id);
		Task<bool> Existe(string nombre, int usuarioId);
		Task<IEnumerable<TipoTarea>> Obtener(int usuarioId);
		Task<TipoTarea> ObtenerPorId(int id, int usuarioId);
	}

	public class RepositorioTiposTareas:IRepositorioTiposTareas
	{
		private readonly string connectionString;

        public RepositorioTiposTareas(IConfiguration configuration)
        {
			connectionString = configuration.GetConnectionString("DefaultConnection");
        }


		public async Task Crear(TipoTarea tipoTarea)
		{
			using var connection = new SqlConnection(connectionString);


			//var parameters = new
			//{
			//	tipoTarea.Nombre,
			//	tipoTarea.UsuarioId
			//};

			var id = await connection.QuerySingleAsync<int>("TiposTareas_Insertar", new {tipoTarea.Nombre,tipoTarea.UsuarioId}, commandType: CommandType.StoredProcedure);



			tipoTarea.Id = id;

		}

		public async Task<bool> Existe(string nombre,int usuarioId)
		{
			using var connection = new SqlConnection(connectionString); ;

			var parameters = new
			{
				nombre,
				usuarioId
			};

			var existe = await connection.QueryFirstOrDefaultAsync<int>("TiposTareas_Existe", parameters, commandType: CommandType.StoredProcedure);

			return existe == 1;


		}


		public async Task<IEnumerable<TipoTarea>> Obtener(int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);


			return await connection.QueryAsync<TipoTarea>("TiposTareas_Obtener", new {usuarioId}, commandType: CommandType.StoredProcedure);


		}

		public async Task Actualizar(TipoTarea tipoTarea)
		{
			using var connection = new SqlConnection(connectionString);

			var parameters = new
			{
				tipoTarea.Nombre,
				tipoTarea.Id
			};
			await connection.ExecuteAsync("TiposTareas_Actualizar", parameters, commandType: CommandType.StoredProcedure);

		}

		public async Task Eliminar(int id)
		{
			using var connection = new SqlConnection(connectionString);


			await connection.ExecuteAsync("TiposTareas_Eliminar", new {id}, commandType: CommandType.StoredProcedure);
		}


		public async Task<TipoTarea> ObtenerPorId(int id,int usuarioId)
		{
			using var connection = new SqlConnection(connectionString);

			return await connection.QueryFirstOrDefaultAsync<TipoTarea>("TiposTareas_ObtenerPorId", new {id,usuarioId}, commandType: CommandType.StoredProcedure);
		}






		//private SqlConnection Conexion()
		//{
		//	using var connection = new SqlConnection(connectionString);

		//	return connection;

		//}



	}
}
