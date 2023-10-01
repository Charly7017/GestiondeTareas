namespace GestionDeTareas.Servicios
{
	public interface IServicioUsuarios
	{
		int ObtenerIdUsuario();
	}
	public class ServicioUsuarios: IServicioUsuarios
	{

		public int ObtenerIdUsuario()
		{
			return 3;
		}

	}
}
