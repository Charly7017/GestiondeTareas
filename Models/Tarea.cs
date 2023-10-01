namespace GestionDeTareas.Models
{
	public class Tarea
	{
		public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoTareaId { get; set; }
        public int Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
		public DateTime FechaLimite { get; set; }
        public bool Completada { get; set; }
    }
}
